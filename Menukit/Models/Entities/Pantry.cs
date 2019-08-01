using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Menukit.Models.Entities
{
    public class Pantry
    {
        private List<PantryLine> lines = new List<PantryLine>();
        public IList<PantryLine> Lines { get { return lines.AsReadOnly(); } }
        public void AddItem(Ingredient ingredient, int quantity)
        {
            var line = lines
                .FirstOrDefault(l => l.Ingredient.IngredientID == ingredient.IngredientID);
            if (line == null)
                lines.Add(new PantryLine { Ingredient = ingredient, Quantity = quantity });
            else
                line.Quantity += quantity;
        }
        public void RemoveLine(Ingredient ingredient)
        {
            lines.RemoveAll(l => l.Ingredient.IngredientID == ingredient.IngredientID);
        }
        public void Clear()
        {
            lines.Clear();
        }
    }

    public class PantryLine
    {
        public Ingredient Ingredient { get; set; }
        public int Quantity { get; set; }
    }
}