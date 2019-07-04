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
        
        public ViewResult List(int page)
        {
            int numProducts = ingredientsRepository.Ingredients.Count();
            ViewData["TotalPages"] = (int)Math.Ceiling((double)numProducts / PageSize);
            ViewData["CurrentPage"] = page;

            return View(ingredientsRepository.Ingredients
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList());
        }
    }
}