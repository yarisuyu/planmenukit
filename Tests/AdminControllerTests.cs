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
    }
}
