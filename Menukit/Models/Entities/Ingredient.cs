using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Menukit.Models.Entities
{
    public class Ingredient : IDataErrorInfo
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public string this[string propName]
        {
            get
            {
                if ((propName == "Name") && string.IsNullOrEmpty(Name))
                {
                    return "Введите непустое название.";
                }
                if ((propName == "Category") && string.IsNullOrEmpty(Category))
                {
                    return "Введите непустую категорию.";
                }
                return null;
            }
        }

        public string Error { get { return null; } }


    }
}