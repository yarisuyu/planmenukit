using Menukit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Tests
{
    [TestFixture]
    public class OutboundRoutingTests
    {
        [Test]
        public void All_Ingredients_Page_1_Is_At_Slash()
        {
            Assert.AreEqual("/", GetOutboundUrl(new {
                controller = "Ingredients", action = "List",
                category = (string)null, page = 1
            }));
        }

        [Test]
        public void Fruits_Page1_Is_At_Slash_Fruits()
        {
            Assert.AreEqual("/Fruits", GetOutboundUrl(new
            {
                controller = "Ingredients",
                action = "List",
                category = "Fruits",
                page = 1
            }));
        }

        [Test]
        public void Fruits_Page101_Is_At_Slash_Fruits_Slash_Page101()
        {
            Assert.AreEqual("/Fruits/Page101", GetOutboundUrl(new
            {
                controller = "Ingredients",
                action = "List",
                category = "Fruits",
                page = 101
            }));
        }

        [Test]
        public void AnythingController_Else_Action_Is_At_Anything_Slash_Else()
        {
            Assert.AreEqual("/Anything/Else", GetOutboundUrl(new
            {
                controller = "Anything",
                action = "Else"
            }));
        }

        private string GetOutboundUrl(object routeValues)
        {
            // Получить конфигурацию маршрута и имитацию контекста запроса
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var mockHttpContext = new Moq.Mock<HttpContextBase>();
            var mockRequest = new Moq.Mock<HttpRequestBase>();
            var fakeResponse = new FakeResponse();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockHttpContext.Setup(x => x.Response).Returns(fakeResponse);
            mockRequest.Setup(x => x.ApplicationPath).Returns("/");

            // Генерация исходящего URL
            var ctx = new RequestContext(mockHttpContext.Object, new RouteData());

            return routes.GetVirtualPath(ctx, new RouteValueDictionary(routeValues))
                .VirtualPath;
        }

        private class FakeResponse : HttpResponseBase
        {
            // Маршрутизация вызовов для сеансов, не использующих cookie-наборы
            // На тест это не влияет, поэтому возвратить путь неизменным
            public override string ApplyAppPathModifier(string virtualPath)
            {
                return virtualPath;
            }
        }
    }
}
