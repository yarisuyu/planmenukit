using Menukit.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Menukit.Controllers
{
    public class NavController : Controller
    {
        private IIngredientsRepository ingredientsRepository;
        public NavController(IIngredientsRepository ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public PartialViewResult Menu(string highlightCategory)
        {
            // Поместить в начало ссылку Home
            List<NavLink> navLinks = new List<NavLink>();
            navLinks.Add(new CategoryLink(null) {
                IsSelected = (highlightCategory == null)
            });

            // Добавить ссылку для каждой отличающейся категории
            var categories =
                ingredientsRepository.Ingredients.Select(x => x.Category);
            foreach (string category in categories.Distinct().OrderBy(x => x))
                navLinks.Add(new CategoryLink(category) {
                    IsSelected = (category == highlightCategory)
                });

            return PartialView(navLinks);
        }
    }

    public class NavLink // Ссылка на произвольный элемент маршрута
    {
        public string Text { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
        public bool IsSelected { get; set; }
    }

    public class CategoryLink: NavLink // Специфическая ссылка на категорию ингредиентов
    {
        public CategoryLink(string category)
        {
            Text = category ?? "Home";
            RouteValues = new RouteValueDictionary(new
            {
                controller = "Ingredients",
                action = "List",
                category = category,
                page = 1
            });
        }
    }

}