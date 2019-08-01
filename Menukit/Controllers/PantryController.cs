using Menukit.Models.Abstract;
using Menukit.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Menukit.Controllers
{
    public class PantryController : Controller
    {
        private IIngredientsRepository ingredientsRepository;
        public PantryController(IIngredientsRepository ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public RedirectToRouteResult AddToPantry(Pantry pantry,
            int ingredientId, string returnUrl)
        {
            Ingredient ingredient = ingredientsRepository.Ingredients
                .FirstOrDefault(i => i.IngredientID == ingredientId);
            pantry.AddItem(ingredient, 1);
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromPantry(Pantry pantry,
            int ingredientId, string returnUrl)
        {
            Ingredient ingredient = ingredientsRepository.Ingredients
                .FirstOrDefault(i => i.IngredientID == ingredientId);
            pantry.RemoveLine(ingredient);
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Pantry pantry, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            ViewData["CurrentCategory"] = "Pantry";
            return View(pantry);
        }

        public ViewResult Summary(Pantry pantry)
        {
            return View(pantry);
        }
    }
}