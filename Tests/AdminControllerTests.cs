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
    public class AdminControllerTests
    {
        private Moq.Mock<IIngredientsRepository> mockRepos;

        [SetUp]
        public void SetUp()
        {
            List<Ingredient> allIngredients = new List<Ingredient>();
            for (int i = 1; i <= 50; i++)
            {
                allIngredients.Add(new Ingredient { IngredientID = i, Name = "Ingredient " + i});
            }
            mockRepos = new Moq.Mock<IIngredientsRepository>();
            mockRepos.Setup(x => x.Ingredients)
                .Returns(allIngredients.AsQueryable());
        }

        [Test]
        public void Index_Action_Lists_All_Ingredients()
        {
            AdminController controller = new AdminController(mockRepos.Object);

            ViewResult results = controller.Index();

            Assert.IsEmpty(results.ViewName);

            var ingredRendered = (List<Ingredient>)results.ViewData.Model;
            Assert.AreEqual(50, ingredRendered.Count);

            for (int i = 0; i < 50; i++)
            {
                Assert.AreEqual("Ingredient " + (i + 1), ingredRendered[i].Name);
            }
        }

        [Test]
        public void Edit_Renders_Correct_Ingredient()
        {
            AdminController controller = new AdminController(mockRepos.Object);

            ViewResult result = controller.Edit(17);

            Assert.IsEmpty(result.ViewName);

            var renderedIngredient = (Ingredient)result.ViewData.Model;
            Assert.AreEqual(17, renderedIngredient.IngredientID);
            Assert.AreEqual("Ingredient " + 17, renderedIngredient.Name);
        }

        [Test]
        public void Edit_Action_Saves_Ingredient_To_Repository_And_Redirects_To_Index()
        {
            AdminController controller = new AdminController(mockRepos.Object);
            Ingredient newIngredient = new Ingredient();

            var result = (RedirectToRouteResult)controller.Edit(newIngredient);

            mockRepos.Verify(x => x.SaveIngredient(newIngredient));
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_Action_For_Invalid_Data_Redirects_To_Edit()
        {
            AdminController controller = new AdminController(mockRepos.Object);
            controller.ModelState.AddModelError("SomeProperty", "Got invalid data");
            Ingredient newIngredient = new Ingredient();

            var result = (RedirectToRouteResult)controller.Edit(newIngredient);

            Assert.AreEqual("Edit", result.RouteValues["action"]);
        }

        [Test]
        public void Create_Action_Redirects_To_Edit_For_New_Ingredient()
        {
            AdminController controller = new AdminController(mockRepos.Object);

            var result = controller.Create();

            Assert.AreEqual("Edit", result.ViewName);
            Assert.AreEqual("", ((Ingredient)result.Model).Name);
            Assert.AreEqual("", ((Ingredient)result.Model).Category);
        }
    }
}
