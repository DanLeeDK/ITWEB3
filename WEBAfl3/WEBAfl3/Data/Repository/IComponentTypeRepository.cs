using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WEBAfl3.Models;

namespace WEBAfl3.Data.Repository
{
    public interface IComponentTypeRepository
    {
        IEnumerable<ComponentType> GetAll();
        IEnumerable<ComponentType> GetAll(Expression<Func<ComponentType,bool>> predicate);
        ComponentType GetById(long id);
        void Add(ComponentType componentType);
        void Remove(ComponentType component);
        void Edit(ComponentType component);
    }
}