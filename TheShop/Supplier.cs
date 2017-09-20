namespace TheShop
{
    public class Supplier
    {
        private Article _article;

        public Supplier(int id, string name, int price)
        {
            _article = new Article()
            {
                ID = id,
                NameOfArticle = name,
                ArticlePrice = price
            };
        }

        public bool ArticleInInventory(int id)
        {
            return _article.ID == id;
        }

        public Article GetArticle(int id)
        {
            return _article.ID == id ? _article : null;
        }
    }
}
