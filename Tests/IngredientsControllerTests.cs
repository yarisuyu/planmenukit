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

            var result = controller.List(2);

            Assert.IsNotNull(result, "Didn't render view");
            var ingredients = result.ViewData.Model as IList<Ingredient>;
            Assert.AreEqual(2, ingredients.Count, "Got wrong number of ingredients");
            Assert.AreEqual("I4", ingredients[0].Name);
            Assert.AreEqual("I5", ingredients[1].Name);
        }

        static IIngredientsRepository MockIngredientsRepository(params Ingredient[] ingredients)
        {
            var mockIngredientsRepos = new Moq.Mock<IIngredientsRepository>();
            mockIngredientsRepos.Setup(x => x.Ingredients).Returns(ingredients.AsQueryable());
            return mockIngredientsRepos.Object;
        }
    }
}
