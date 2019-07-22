using Menukit.Controllers;
using Menukit.Models.Abstract;
using Menukit.Models.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tests
{
    [TestFixture]
    class NavControllerTests
    {
        [Test]
        public void Takes_IIngredientsRepository_As_Constructor_Param()
        {
            new NavController((IIngredientsRepository)null);
        }

        [Test]
        public void Produces_Home_Plus_NavLink_Object_For_Each_Distinct_Category()
        {
            IQueryable<Ingredient> ingredients = new[]
            {
                new Ingredient { Name = "A", Category = "Fruit" },
                new Ingredient { Name = "B", Category = "Vegetable" },
                new Ingredient { Name = "C", Category = "Meat" },
                new Ingredient { Name = "D", Category = "Vegetable" },
                new Ingredient { Name = "E", Category = "Fruit" }
            }.AsQueryable();

            var mockIngredientsRepos = new Moq.Mock<IIngredientsRepository>();
            mockIngredientsRepos.Setup(x => x.Ingredients).Returns(ingredients);
            var controller = new NavController(mockIngredientsRepos.Object);

            PartialViewResult result = controller.Menu(null);

            // Проверить визуализацию по одной NavLink на категорию
            // (в алфавитном порядке)
            var links = ((IEnumerable<NavLink>)result.ViewData.Model).ToList();
            Assert.IsEmpty(result.ViewName); // Должно визуализировать
                                             //представление по умолчанию
            Assert.AreEqual(4, links.Count);
            Assert.AreEqual("Home", links[0].Text);
            Assert.AreEqual("Fruit", links[1].Text);
            Assert.AreEqual("Meat", links[2].Text);
            Assert.AreEqual("Vegetable", links[3].Text);

            foreach (var link in links)
            {
                Assert.AreEqual("Ingredients", link.RouteValues["controller"]);
                Assert.AreEqual("List", link.RouteValues["action"]);
                Assert.AreEqual(1, link.RouteValues["page"]);
                if (links.IndexOf(link) == 0)
                    Assert.IsNull(link.RouteValues["category"]);
                else
                    Assert.AreEqual(link.Text, link.RouteValues["category"]);
            }
        }

        [Test]
        public void Highlights_Current_Category()
        {
            IQueryable<Ingredient> ingredients = new[] {
                new Ingredient { Name = "A", Category = "Meat"},
                new Ingredient { Name = "B", Category = "Vegetable" }
            }.AsQueryable();

            var mockIngredientsRepos = new Moq.Mock<IIngredientsRepository>();
            mockIngredientsRepos.Setup(x => x.Ingredients).Returns(ingredients);
            var controller = new NavController(mockIngredientsRepos.Object);

            var result = controller.Menu("Vegetable");

            var highlightedLinks = ((IEnumerable<NavLink>)result.ViewData.Model)
                            .Where(x => x.IsSelected).ToList();

            Assert.AreEqual(1, highlightedLinks.Count);
            Assert.AreEqual("Vegetable", highlightedLinks[0].Text);

        }
    }
}
