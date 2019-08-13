using Menukit.Models.Entities;
using Menukit.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace Menukit.Models.Concrete
{
    public class SqlIngredientsRepository : IIngredientsRepository
    {
        IngredientContext ctx = new IngredientContext();

        public SqlIngredientsRepository()
        { }

        public IQueryable<Ingredient> Ingredients
        {
            get { return ctx.Ingredients; }
        }

        public void SaveIngredient(Ingredient ingredient)
        {
            EnsureValid(ingredient, "Name", "Category");

            Ingredient existingIngredient = ctx.Ingredients.FirstOrDefault();
            //Если это новый ингредиент, просто добавить его к DataContext
            if (existingIngredient == null)
            {
                ctx.Ingredients.Add(ingredient);
            }
            else
            {
                // Если обновляется существующий ингредиент,
                // поручить IngredientContext сохранение этого экземпляра
                ctx.Ingredients.Attach(ingredient);
                ctx.Entry(ingredient).State = System.Data.Entity.EntityState.Modified;
            }
            ctx.SaveChanges();
        }

        public void DeleteIngredient(Ingredient ingredient)
        {
            ctx.Ingredients.Remove(ingredient);
            ctx.SaveChanges();
        }

        private void EnsureValid(IDataErrorInfo validatable, params string[] properties)
        {
            if (properties.Any(x => validatable[x] != null))
                throw new InvalidOperationException("Недопустимый объект.");
        }
    }
}