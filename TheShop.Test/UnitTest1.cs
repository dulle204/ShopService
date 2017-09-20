using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TheShop.Test
{
    [TestClass]
    public class UnitTest1
    {
        private ShopService _service;

        [TestMethod]
        public void OrderAndSellArticle_ShouldNotThrowException()
        {
            //arrange
            List<Supplier> suppliers = new List<Supplier>();
            var Supplier1 = new Supplier(1, "Article from supplier1", 458);
            suppliers.Add(Supplier1);
            var Supplier2 = new Supplier(1, "Article from supplier2", 459);
            suppliers.Add(Supplier2);
            var Supplier3 = new Supplier(1, "Article from supplier3", 460);
            suppliers.Add(Supplier3);
            _service = new ShopService(suppliers);

            //act
            _service.OrderAndSellArticle(1, 459, 12);
            var article = _service.GetById(1);

            //assert
            Assert.AreEqual(458, article.ArticlePrice);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void OrderAndSellArticle_ShouldThrowException()
        {
            //arrange
            List<Supplier> suppliers = new List<Supplier>();
            var Supplier1 = new Supplier(1, "Article from supplier1", 458);
            suppliers.Add(Supplier1);
            var Supplier2 = new Supplier(1, "Article from supplier2", 459);
            suppliers.Add(Supplier2);
            var Supplier3 = new Supplier(1, "Article from supplier3", 460);
            suppliers.Add(Supplier3);
            _service = new ShopService(suppliers);

            //act
            _service.OrderAndSellArticle(1, 400, 12);
            var article = _service.GetById(1);
        }
    }
}
