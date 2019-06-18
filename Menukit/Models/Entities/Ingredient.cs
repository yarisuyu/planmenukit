using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Menukit.Models.Entities
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}