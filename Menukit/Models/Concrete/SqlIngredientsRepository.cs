using Menukit.Models.Entities;
using Menukit.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}