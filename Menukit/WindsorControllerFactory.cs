using Castle.Core;
using Castle.Core.Resource;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Menukit
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        WindsorContainer container;

        public WindsorControllerFactory()
        {
            container = new WindsorContainer(
                new XmlInterpreter(new ConfigResource("castle")));

            // создать экземпляр контейнера, взяв конфигурацию из Web.config
            var controllerTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                  where typeof(IController).IsAssignableFrom(t)
                                  select t;
            
            // зарегистрировать все типы контроллеров как Transient
            foreach (Type t in controllerTypes)
                container.Register(Component.For(t).Named(t.FullName).LifeStyle.Is(LifestyleType.Transient));
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                return (IController)container.Resolve(controllerType);
            }
            else
            {
                return base.GetControllerInstance(requestContext, controllerType);
            }
        }
    }
}