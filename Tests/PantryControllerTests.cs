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
    public class PantryControllerTests
    {
        [Test]
        public void Can_Add_Ingredient_To_Pantry()
        {
            var mockIngredientsRepos = new Moq.Mock<IIngredientsRepository>();
            var ingredients = new System.Collections.Generic.List<Ingredient>
            {
                new Ingredient { IngredientID = 14, Name = "Milk" },
                new Ingredient { IngredientID = 27, Name = "Eggs" }
            };

            mockIngredientsRepos.Setup(x => x.Ingredients)
                .Returns(ingredients.AsQueryable());

            var pantry = new Pantry();
            var controller = new PantryController(mockIngredientsRepos.Object);


            RedirectToRouteResult result =
                controller.AddToPantry(pantry, 27, "someReturnUrl");

            Assert.AreEqual(1, pantry.Lines.Count);
            Assert.AreEqual("Eggs", pantry.Lines[0].Ingredient.Name);
            Assert.AreEqual(1, pantry.Lines[0].Quantity);

            // Проверить, что посетитель перенаправлен на экран отображения кладовой
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("someReturnUrl", result.RouteValues["returnUrl"]);
        }

        [Test]
        public void Can_Remove_Ingredient_From_Pantry()
        {
            var mockIngredientsRepos = new Moq.Mock<IIngredientsRepository>();
            var ingredients = new System.Collections.Generic.List<Ingredient>
            {
                new Ingredient { IngredientID = 14, Name = "Milk" },
                new Ingredient { IngredientID = 27, Name = "Eggs" }
            };

            mockIngredientsRepos.Setup(x => x.Ingredients)
                .Returns(ingredients.AsQueryable());

            var pantry = new Pantry();
            pantry.AddItem(new Ingredient { IngredientID = 14, Name = "Milk" }, 3);
            pantry.AddItem(new Ingredient { IngredientID = 27, Name = "Eggs" }, 10);

            Assert.AreEqual(2, pantry.Lines.Count);
            Assert.AreEqual("Milk", pantry.Lines[0].Ingredient.Name);
            Assert.AreEqual(3, pantry.Lines[0].Quantity);
            Assert.AreEqual("Eggs", pantry.Lines[1].Ingredient.Name);
            Assert.AreEqual(10, pantry.Lines[1].Quantity);

            var controller = new PantryController(mockIngredientsRepos.Object);
            
            RedirectToRouteResult result =
                controller.RemoveFromPantry(pantry, 14, "someReturnUrl");

            Assert.AreEqual(1, pantry.Lines.Count);
            Assert.AreEqual("Eggs", pantry.Lines[0].Ingredient.Name);
            Assert.AreEqual(10, pantry.Lines[0].Quantity);

            // Проверить, что посетитель перенаправлен на экран отображения кладовой
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("someReturnUrl", result.RouteValues["returnUrl"]);
        }

        [Test]
        public void Index_Action_Renders_Default_View_With_Pantry_And_Return_Url()
        {
            Pantry pantry = new Pantry();
            PantryController controller = new PantryController(null);

            ViewResult result = controller.Index(pantry, "myReturnUrl");

            Assert.IsEmpty(result.ViewName); // Визуализировать представление по умолчанию
            Assert.AreSame(pantry, result.ViewData.Model);
            Assert.AreEqual("myReturnUrl", result.ViewData["returnUrl"]);
            Assert.AreEqual("Pantry", result.ViewData["CurrentCategory"]);
        }

        public void Summary_Action_Renders_Default_View_With_Pantry()
        {
            Pantry pantry = new Pantry();
            PantryController controller = new PantryController(null);

            ViewResult result = controller.Summary(pantry);

            Assert.IsEmpty(result.ViewName);
            Assert.AreSame(pantry, result.ViewData.Model);
        }
    }
}
