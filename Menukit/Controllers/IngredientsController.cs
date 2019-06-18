using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Menukit.Models.Abstract;
using Menukit.Models.Concrete;

namespace Menukit.Controllers
{
    public class IngredientsController : Controller
    {
        private IIngredientsRepository ingredientsRepository;

        public IngredientsController()
        {
            this.ingredientsRepository = new FakeIngredientsRepository();
        }

        public ViewResult List()
        {
            return View(ingredientsRepository.Ingredients.ToList());
        }
    }
}