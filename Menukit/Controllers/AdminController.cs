using Menukit.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Menukit.Controllers
{
    public class AdminController : Controller
    {
        private IIngredientsRepository ingredientsRepository;
        public AdminController(IIngredientsRepository ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public ViewResult Index()
        {
            return View(ingredientsRepository.Ingredients.ToList());
        }

        
    }
}
