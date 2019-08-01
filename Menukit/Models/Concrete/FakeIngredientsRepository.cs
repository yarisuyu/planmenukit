using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Menukit.Models.Abstract;
using Menukit.Models.Entities;


namespace Menukit.Models.Concrete
{
    public class FakeIngredientsRepository : IIngredientsRepository
    {
        private static IQueryable<Ingredient> fakeIngredients = new List<Ingredient> {
            new Ingredient {IngredientID = 0, Name = "Гречка", Category = "Крупы"},
            new Ingredient {IngredientID = 1, Name = "Рис", Category = "Крупы"},
            new Ingredient {IngredientID = 2, Name = "Курица", Category = "Крупы"},
            new Ingredient {IngredientID = 3, Name = "Говядина", Category = "Мясо"},
            new Ingredient {IngredientID = 4, Name = "Творог", Category = "Молочка"},
            new Ingredient {IngredientID = 5, Name = "Яйцо", Category = "Яйца"}
        }.AsQueryable();

        public IQueryable<Ingredient> Ingredients
        {
            get { return fakeIngredients; }
        }
    }

}