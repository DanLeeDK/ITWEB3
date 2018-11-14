using System;
using System.Collections.Generic;
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
            var component3 = new Component
            {
                ComponentNumber = 3,
                SerialNo = "JK69",
                Status = ComponentStatus.Loaned,
                AdminComment = "Lots of fun",
                UserComment = "Sure is"
            };

            context.Components.Add(component1);
            context.Components.Add(component2);
            context.Components.Add(component3);
            context.SaveChanges();

            var image = new Byte[0];//System.IO.File.ReadAllBytes("resistor.jpg");

            var esimage = new ESImage{
                ImageData = image,
                Thumbnail = image,
                ImageMimeType = "image/jpeg"
            };

            var componentType1 = new ComponentType
            {
                ComponentName = "Resistor",
                ComponentInfo = "Just a resistor",
                Location = "Shelf",
                Status = ComponentTypeStatus.Available,
                Datasheet = "data",
                ImageUrl = "url",
                Image = esimage,
                Manufacturer = "Hest og Co A/S",
                WikiLink = "wikilink",
                AdminComment = "comment",
            };

            var componentType2 = new ComponentType
            {
                ComponentName = "Pump",
                ComponentInfo = "Just a pump",
                Location = "Drawer 5",
                Status = ComponentTypeStatus.Available,
                Datasheet = "Push up 4 times",
                ImageUrl = "url",
                Image = esimage,
                Manufacturer = "Fitness World",
                WikiLink = "wikilink",
                AdminComment = "Be careful if you don't want to be big",
            };

            componentType1.Components.Add(component1);
            componentType1.Components.Add(component2);
            context.ComponentTypes.Add(componentType1);

            componentType2.Components.Add(component3);
            context.ComponentTypes.Add(componentType2);
            context.SaveChanges();

            var category = new Category
            {
                Name = "Test"
            };

            var categoryComponentType1 = new ComponentTypeCategory
            {
                Category = category,
                ComponentType = componentType1
            };

            var categoryComponentType2 = new ComponentTypeCategory
            {
                Category = category,
                ComponentType = componentType2
            };

            category.ComponentTypeCategories.Add(categoryComponentType1);
            category.ComponentTypeCategories.Add(categoryComponentType2);
            context.Categories.Add(category);
            //context.CategoryComponentTypes.Add(categoryComponentType);
            context.SaveChanges();
        }
    }
}