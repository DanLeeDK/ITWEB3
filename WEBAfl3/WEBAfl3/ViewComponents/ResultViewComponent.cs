using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAfl3.Data.Repository;
using WEBAfl3.Models;

namespace ITWEBExercise5.ViewComponents
{
    public class ResultViewComponent : ViewComponent
    {
        private readonly IComponentRepository _componentDb;
        private readonly ICategoryRepository _categoryDb;
        private readonly IComponentTypeRepository _typeDb;

        public ResultViewComponent(IComponentRepository compDb, ICategoryRepository catDb, IComponentTypeRepository typeDb)
        {
            _componentDb = compDb;
            _categoryDb = catDb;
            _typeDb = typeDb;
        }

        public async Task<IViewComponentResult> InvokeAsync(int typeId)
        {
            var components = await GetComponentsAsync(typeId);
            if (components == null)
            {
                return View();
            }
            return View(components);
        }

        private Task<List<Component>> GetComponentsAsync(int typeId)
        {
            // If searching by type
            if (typeId != 0)
            {
                var co = _componentDb.GetAll().Where(c => c.ComponentTypeId == typeId).ToAsyncEnumerable().ToList();
                return co;
            }
            // If searching by category
            //if (categoryName != "asdf")
            //{
            //    //var category = _categoryDb.GetAll().Where(c => c.Name == categoryName);
            //    var allTypes = _typeDb.GetAll();
            //    var components = new List<Component>();
            //    foreach (var type in allTypes)
            //    {
            //        foreach (var category in type.Categories)
            //        {
            //            if (category.Name == categoryName)
            //            {
            //                components.Concat(type.Components);
            //            }
            //        }
            //    }
            //    return components.ToAsyncEnumerable().ToList();
            //}
            // No search - get all components
            //return _componentDb.GetAll().ToAsyncEnumerable().ToList();
            return new List<Component>().ToAsyncEnumerable().ToList();
        }
    }
}
