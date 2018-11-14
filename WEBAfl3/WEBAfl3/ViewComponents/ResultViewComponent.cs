using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WEBAfl3.Data.Repository;
using WEBAfl3.Models;

namespace WEBAfl3.ViewComponents
{
    public class ResultViewComponent : ViewComponent
    {
        private readonly IComponentRepository _componentRepository;
        private readonly IComponentTypeRepository _componentTypeRepository;
       
        public ResultViewComponent(IComponentRepository componentRepository, IComponentTypeRepository componentTypeRepository)
        {
            _componentRepository = componentRepository;
            _componentTypeRepository = componentTypeRepository;
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
                var componentType = _componentTypeRepository.GetById(typeId);


                var co = componentType.Components.ToAsyncEnumerable().ToList(); //_componentRepository.GetAll().Where(c => c.ComponentType.ComponentTypeId == typeId).ToAsyncEnumerable().ToList();
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
