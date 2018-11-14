namespace WEBAfl3.Models
{
    public class ComponentTypeCategory
    {
        public int ComponentTypeId { get; set; }
        public ComponentType ComponentType { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
