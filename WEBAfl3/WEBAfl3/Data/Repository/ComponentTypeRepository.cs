using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WEBAfl3.Models;

namespace WEBAfl3.Data.Repository
{
    public class ComponentTypeRepository : IComponentTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ComponentTypeRepository(ApplicationDbContext context) => _context = context;

        public void Add(ComponentType componentType)
        {
            _context.ComponentTypes.Add(componentType);
            _context.SaveChanges();
        }

        public void Edit(ComponentType component)
        {
            _context.Entry(component).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }

        public IEnumerable<ComponentType> GetAll() => _context.ComponentTypes.AsEnumerable();

        public IEnumerable<ComponentType> GetAll(Expression<Func<ComponentType, bool>> predicate) => _context.ComponentTypes.Where(predicate).AsEnumerable();

        public ComponentType GetById(long id) => _context.ComponentTypes.FirstOrDefault(x => x.ComponentTypeId == id);

        public void Remove(ComponentType component)
        {
            _context.ComponentTypes.Remove(component);
            _context.SaveChanges();
        }
    }
}