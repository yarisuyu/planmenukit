using Menukit.HtmlHelpers;
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
    public class PageHelperTests
    {
        [Test]
        public void PageLinksMethodExtendsHtmlHelper()
        {
            HtmlHelper html = null;
            html.PageLinks(0, 0, null);
        }

        [Test]
        public void PageLinksProducesAnchorTags()
        {
            // Первый параметр - индекс текущей страницы
            // Второй параметр - общее количество страниц
            // Третий параметр - лямбда-функция для отображения 
            // номера страницы на ее URL
            string links = ((HtmlHelper)null).PageLinks(2, 3, i => "Page" + i);

            Assert.AreEqual(@"<a href=""Page1"">1</a>
<a class=""selected"" href=""Page2"">2</a>
<a href=""Page3"">3</a>
", links);
        }
    }
}
