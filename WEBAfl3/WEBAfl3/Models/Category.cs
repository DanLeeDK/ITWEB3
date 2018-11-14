using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WEBAfl3.Models
{
    public class Category
    {
        public Category()
        {
            ComponentTypeCategories = new List<ComponentTypeCategory>();
        }
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<ComponentTypeCategory> ComponentTypeCategories
        {
            get; protected set;
        }
    }
}
