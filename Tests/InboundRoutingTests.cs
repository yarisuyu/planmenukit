using Menukit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Tests
{
    [TestFixture]
    public class InboundRoutingTests
    {
        [Test]
        public void Slash_Goes_To_All_Ingredients_Page()
        {
            TestRoute("~/", new { controller = "Ingredients", action = "List",
                category = (string)null, page = 1 });
        }

        [Test]
        public void Page2_Goes_To_All_Ingredients_Page_2()
        {
            TestRoute("~/Page2", new
            {
                controller = "Ingredients",
                action = "List",
                category = (string)null,
                page = 2
            });
        }

        [Test]
        public void Fruits_Goes_To_Fruits_Page_1()
        {
            TestRoute("~/Fruits", new
            {
                controller = "Ingredients",
                action = "List",
                category = "Fruits",
                page = 1
            });
        }

        [Test]
        public void Fruits_Slash_Page43_Goes_To_Fruits_Page_43()
        {
            TestRoute("~/Fruits/Page43", new
            {
                controller = "Ingredients",
                action = "List",
                category = "Fruits",
                page = 43
            });
        }

        [Test]
        public void Anything_Slash_Else_Goes_To_Else_On_AnythingController()
        {
            TestRoute("~/Anything/Else", new
            {
                controller = "Anything",
                action = "Else"
            });
        }

        private void TestRoute (string url, object expectedValues)
        {
            // Подготовка: подгтовить коллекцию маршрутов и макет контекста запроса
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            var mockHttpContext = new Moq.Mock<HttpContextBase>();
            var mockRequest = new Moq.Mock<HttpRequestBase>();
            mockHttpContext.Setup(x => x.Request).Returns(mockRequest.Object);
            mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(url);

            // Действие: получить отображенный маршрут
            RouteData routeData = routes.GetRouteData(mockHttpContext.Object);

            // Утверждение: проверить значения маршрута на соответствие ожидаемым значениям
            Assert.IsNotNull(routeData);
            var expectedDict = new RouteValueDictionary(expectedValues);
            foreach (var expectedVal in expectedDict)
            {
                if (expectedVal.Value == null)
                    Assert.IsNull(routeData.Values[expectedVal.Key]);
                else
                    Assert.AreEqual(expectedVal.Value.ToString(),
                        routeData.Values[expectedVal.Key].ToString());
            }
        }
    }
}
