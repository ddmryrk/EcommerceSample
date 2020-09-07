using EcommerceSample.Interfaces;

namespace EcommerceSample.Entities
{
    public class Category : PrivateObjectBase
    {
        public string   Title       { get; set; }
        public int      ParentID    { get; set; }
        public Category Parent      { get; set; }

        public Category(string title)
        {
            Title = title;
        }

        public void SetParent(Category parentCategory)
        {
            Parent = parentCategory;
            ParentID = parentCategory.ID;
        }
    }
}
