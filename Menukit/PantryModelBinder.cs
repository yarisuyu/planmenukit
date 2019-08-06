using Menukit.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Menukit
{
    public class PantryModelBinder : IModelBinder
    {
        private const string pantrySessionKey = "_cart";
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            if (bindingContext.Model != null)
                throw new InvalidOperationException("Не удалось обновить экземпляры");

            // вернуть объект Pantry из Session[], при необходимости создать его.
            Pantry pantry = (Pantry)controllerContext.HttpContext.Session[pantrySessionKey];
            if (pantry == null)
            {
                pantry = new Pantry();
                controllerContext.HttpContext.Session[pantrySessionKey] = pantry;
            }
            return pantry;
        }
    }
}