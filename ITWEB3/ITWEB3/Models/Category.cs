using System.Collections.Generic;

namespace ITWEB3.Models
{
    public class Category
    {
        public Category()
        {
            ComponentTypeCategories = new List<ComponentTypeCategory>();
        }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ComponentTypeCategory> ComponentTypeCategories
        {
            get; protected set;
        }
    }
}
