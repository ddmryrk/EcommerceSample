using System.Collections.Generic;
using EcommerceSample.Interfaces;

namespace EcommerceSample.Entities
{
    public class Product : PrivateObjectBase
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public Product(string title, decimal price, Category category)
        {
            Title = title;
            Price = price;
            CategoryID = category.ID;
            Category = category;
        }

        public List<int> GetCategoryIDTree()
        {
            var result = new List<int>();

            var currentCategory = this.Category;
            while (currentCategory != null)
            {
                result.Add(currentCategory.ID);
                currentCategory = currentCategory.Parent;
            }

            return result;
        }
    }
}
