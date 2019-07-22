using Menukit.Controllers;
using Menukit.Models.Abstract;
using Menukit.Models.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class IngredientsControllerTests
    {
        [Test]
        public void ListPresentsCorrectPageOfIngredients()
        {
            IIngredientsRepository repository = MockIngredientsRepository(
                new Ingredient { Name = "I1" }, new Ingredient { Name = "I2" },
                new Ingredient { Name = "I3" }, new Ingredient { Name = "I4" },
                new Ingredient { Name = "I5" }
            );
            IngredientsController controller = new IngredientsController(repository);
            controller.PageSize = 3;

            var result = controller.List(null, 2);

            Assert.IsNotNull(result, "Didn't render view");
            var ingredients = result.ViewData.Model as IList<Ingredient>;
            Assert.AreEqual(2, ingredients.Count, "Got wrong number of ingredients");
            Assert.AreEqual(2, (int)result.ViewData["CurrentPage"]);
            Assert.AreEqual(2, (int)result.ViewData["TotalPages"]);
            Assert.AreEqual("I4", ingredients[0].Name);
            Assert.AreEqual("I5", ingredients[1].Name);
        }

        [Test]
        public void List_Includes_All_Ingredients_When_Category_Is_Null()
        {
            IIngredientsRepository repository = MockIngredientsRepository(
                new Ingredient { Name = "Огурец", Category = "Овощи" },
                new Ingredient { Name = "Сыр", Category = "Молочные продукты" }
            );

            IngredientsController controller = new IngredientsController(repository);
            controller.PageSize = 10;

            var result = controller.List(null, 1);

            Assert.IsNotNull(result, "Didn't render view");

            var ingredients = (IList<Ingredient>)result.ViewData.Model;
            Assert.AreEqual(2, ingredients.Count, "Got wrong number of items");
            Assert.AreEqual("Огурец", ingredients[0].Name);
            Assert.AreEqual("Сыр", ingredients[1].Name);
        }

        [Test]
        public void List_Filters_By_Category_When_Requested()
        {
            IIngredientsRepository repository = MockIngredientsRepository(
                new Ingredient { Name = "Огурец", Category = "Овощи" },
                new Ingredient { Name = "Сыр", Category = "Молочные продукты" },
                new Ingredient { Name = "Свекла", Category = "Овощи" },
                new Ingredient { Name = "Творог", Category = "Молочные продукты" },
                new Ingredient { Name = "Морковь", Category = "Овощи" }
            );

            IngredientsController controller = new IngredientsController(repository);
            controller.PageSize = 10;

            var result = controller.List("Овощи", 1);

            Assert.IsNotNull(result, "Didn't render view");

            var ingredients = (IList<Ingredient>)result.ViewData.Model;
            Assert.AreEqual(3, ingredients.Count, "Got wrong number of items");
            Assert.AreEqual("Огурец", ingredients[0].Name);
            Assert.AreEqual("Свекла", ingredients[1].Name);
            Assert.AreEqual("Морковь", ingredients[2].Name);

            Assert.AreEqual("Овощи", result.ViewData["CurrentCategory"]);
        }

        static IIngredientsRepository MockIngredientsRepository(params Ingredient[] ingredients)
        {
            var mockIngredientsRepos = new Moq.Mock<IIngredientsRepository>();
            mockIngredientsRepos.Setup(x => x.Ingredients).Returns(ingredients.AsQueryable());
            return mockIngredientsRepos.Object;
        }
    }
}
