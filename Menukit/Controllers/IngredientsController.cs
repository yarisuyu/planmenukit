using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Menukit.Models.Abstract;
using Menukit.Models.Concrete;
using Menukit.Models.Entities;

namespace Menukit.Controllers
{
    public class IngredientsController : Controller
    {
        private IIngredientsRepository ingredientsRepository;

        public int PageSize = 4;

        public IngredientsController(IIngredientsRepository ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }
        
        public ViewResult List(string category, int page)
        {
            var ingredientsInCategory = (category == null)
                ? ingredientsRepository.Ingredients
                : ingredientsRepository.Ingredients.Where(x => x.Category == category);
            int numProducts = ingredientsInCategory.Count();
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["CurrentPage"] = page;
            ViewData["CurrentCategory"] = category;

            return View(ingredientsInCategory
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList());
        }
    }
}