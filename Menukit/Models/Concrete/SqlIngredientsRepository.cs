using Menukit.Models.Entities;
using Menukit.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Menukit.Models.Concrete
{
    class IngredientContext : DbContext
    {
        public IngredientContext()
            : base(nameOrConnectionString: "DbConnection")
        { }

        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
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