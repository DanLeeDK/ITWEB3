using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WEBAfl3.Models;

namespace WEBAfl3.Data.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        IEnumerable<Category> GetAll(Expression<Func<Category,bool>> predicate);
        Category GetById(int id);
        void Add(Category category);
        void Remove(Category category);
        void Edit(Category category);
    }
}