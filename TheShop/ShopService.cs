using System;
using System.Collections.Generic;
using System.Linq;

namespace TheShop
{
    public class ShopService
    {
        private DatabaseDriver DatabaseDriver;
        private Logger logger;

        private Supplier Supplier1;
        private Supplier Supplier2;
        private Supplier Supplier3;
        private List<Supplier> _suppliers;

        public ShopService()
        {
            DatabaseDriver = new DatabaseDriver();
            logger = new Logger();
            _suppliers = new List<Supplier>();
            Supplier1 = new Supplier(1, "Article from supplier1", 458);
            _suppliers.Add(Supplier1);
            Supplier2 = new Supplier(1, "Article from supplier2", 459);
            _suppliers.Add(Supplier2);
            Supplier3 = new Supplier(1, "Article from supplier3", 460);
            _suppliers.Add(Supplier3);
        }

        public ShopService(List<Supplier> suppliers)
        {
            DatabaseDriver = new DatabaseDriver();
            logger = new Logger();
            _suppliers = suppliers;
        }

        public void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId)
        {
            #region ordering article

            Article article = null;
            Article tempArticle = null;

            foreach (var item in _suppliers)
            {
                var articleExists = item.ArticleInInventory(id);
                if (articleExists)
                {
                    tempArticle = item.GetArticle(id);
                    if (maxExpectedPrice > tempArticle.ArticlePrice)
                    {
                        article = tempArticle;
                    }
                }
            }

            #endregion

            #region selling article

            if (article == null)
            {
                throw new Exception("Could not order article");
            }

            logger.Debug("Trying to sell article with id=" + id);

            article.IsSold = true;
            article.SoldDate = DateTime.Now;
            article.BuyerUserId = buyerId;

            try
            {
                DatabaseDriver.Save(article);
                logger.Info("Article with id=" + id + " is sold.");
            }
            catch (ArgumentNullException ex)
            {
                logger.Error("Could not save article with id=" + id);
                throw new Exception("Could not save article with id");
            }
            catch (Exception e)
            {
                throw;
            }

            #endregion
        }

        public Article GetById(int id)
        {
            return DatabaseDriver.GetById(id);
        }
    }
}
