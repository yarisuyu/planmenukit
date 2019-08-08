using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Menukit.Models.Entities;

namespace Menukit.Models.Abstract
{
    public interface IIngredientsRepository
    {
        IQueryable<Ingredient> Ingredients { get; }
        void SaveIngredient(Ingredient ingredient);
    }
}