using Menukit.Models.Abstract;
using Menukit.Models.Entities;
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

        [AcceptVerbs(HttpVerbs.Get)]
        public ViewResult Edit(int ingredientID)
        {
            Ingredient ingredient = (from i in ingredientsRepository.Ingredients
                                     where i.IngredientID == ingredientID
                                     select i).First();
            return View(ingredient);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                ingredientsRepository.SaveIngredient(ingredient);
                TempData["message"] = ingredient.Name + ": обновление прошло успешно";
                return RedirectToAction("Index");
            }
            else
            {
                // Ошибка проверки достоверности
                return View(ingredient);
            }
    

            return View(ingredient);
        }

        public ViewResult Create()
        {
            return View("Edit", new Ingredient());
        }
    }
}
