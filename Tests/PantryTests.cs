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
    public class PantryTests
    {
        [Test]
        public void Pantry_Starts_Empty()
        {
            Pantry pantry = new Pantry();
            Assert.AreEqual(0, pantry.Lines.Count);
        }

        [Test]
        public void Can_Add_Items_To_Pantry()
        {
            Ingredient i1 = new Ingredient { IngredientID = 1 };
            Ingredient i2 = new Ingredient { IngredientID = 2 };

            //Добавить 3 ингредиента, из них два одинаковы
            Pantry pantry = new Pantry();
            pantry.AddItem(i1, 1);
            pantry.AddItem(i1, 2);
            pantry.AddItem(i2, 10);

            Assert.AreEqual(2, pantry.Lines.Count,
                "Wrong number of lines in the pantry");

            var i1Line = pantry.Lines.Where(l => l.Ingredient.IngredientID == 1).First();
            var i2Line = pantry.Lines.Where(l => l.Ingredient.IngredientID == 2).First();

            Assert.AreEqual(3, i1Line.Quantity);
            Assert.AreEqual(10, i2Line.Quantity);
        }

        [Test]
        public void Can_Remove_Items_From_Pantry()
        {
            Ingredient i1 = new Ingredient { IngredientID = 1 };
            Ingredient i2 = new Ingredient { IngredientID = 2 };

            Pantry pantry = new Pantry();
            pantry.AddItem(i1, 2);
            pantry.AddItem(i2, 10);

            Assert.AreEqual(2, pantry.Lines.Count);

            pantry.RemoveLine(i1);

            Assert.AreEqual(1, pantry.Lines.Count,
                "Wrong number of lines in the pantry");
            Assert.AreEqual(i2.IngredientID,
                pantry.Lines.FirstOrDefault().Ingredient.IngredientID);
        }

        [Test]
        public void Can_Be_Cleared()
        {
            Pantry pantry = new Pantry();
            pantry.AddItem(new Ingredient(), 1);
            Assert.AreEqual(1, pantry.Lines.Count);
            pantry.Clear();
            Assert.AreEqual(0, pantry.Lines.Count);
        }
    }
}
