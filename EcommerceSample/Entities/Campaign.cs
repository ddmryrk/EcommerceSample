using EcommerceSample.Interfaces;

namespace EcommerceSample.Entities
{
    public class Campaign : PrivateObjectBase
    {
        public string Description { get; set; }
        public Category Category { get; set; }
        public DiscountOption DiscountOption { get; set; }
        public int MinimumCartItem { get; set; }

        public Campaign(string description, Category category, int minimumCartItem, DiscountOption discountOption)
        {
            Description = description;
            Category = category;
            MinimumCartItem = minimumCartItem;
            DiscountOption = discountOption;
        }
    }
}
