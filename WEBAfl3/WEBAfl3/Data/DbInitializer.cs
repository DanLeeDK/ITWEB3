using System;
using System.Linq;
using WEBAfl3.Models;

namespace WEBAfl3.Data
{
    public static class DbInitializer
    {
        public static void Initializer(ApplicationDbContext
            context)
        {
            context.Database.EnsureCreated();


            if (context.Components.Any())
            {
                return;
            }

            var component1 = new Component{
                ComponentNumber = 89,
                SerialNo = "KL89",
                Status = ComponentStatus.Available,
                AdminComment = "Working",
                UserComment = "A small flaw"
            };
            var component2 = new Component{
                ComponentNumber = 42,
                SerialNo = "ML42",
                Status = ComponentStatus.Loaned,
                AdminComment = "User au593874 has it",
                UserComment = "I'm keeping it"
            };
            var image = new Byte[0];//System.IO.File.ReadAllBytes("resistor.jpg");

            var esimage = new ESImage{
                ImageData = image,
                Thumbnail = image,
                ImageMimeType = "image/jpeg"
            };
            var componenttype = new ComponentType
            {
                ComponentName = "Resistor",
                ComponentInfo = "Just a resistor",
                Location = "in yer butt",
                Status = ComponentTypeStatus.Available,
                Datasheet = "data",
                ImageUrl = "url",
                Image = esimage,
                Manufacturer = "Poul Ejnar A/S",
                WikiLink = "wikilink",
                AdminComment = "comment"
            };
            componenttype.Components.Add(component1);
            componenttype.Components.Add(component2);
            var category = new Category
            {
                Name = "Test"
            };

            var categoryComponentType = new ComponentTypeCategory
            {
                Category = category,
                ComponentType = componenttype
            };

            context.CategoryComponentTypes.Add(categoryComponentType);
            context.SaveChanges();
        }
    }
}