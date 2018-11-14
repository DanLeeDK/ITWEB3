using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WEBAfl3.Data.Repository;

namespace WEBAfl3.Data.UnitOfWork
{
    public class UnitOfWork : IUnityOfWork
    {
        private readonly ApplicationDbContext _context;
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Components = new ComponentRepository(_context);
            Categories = new CategoryRepository(_context);
            ComponentTypes = new ComponentTypeRepository(_context);
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public IComponentRepository Components { get; }
        public ICategoryRepository Categories { get; }
        public IComponentTypeRepository ComponentTypes { get; }

        public int Complete()
        {
            var result = 0;
            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    saveFailed = true;

                    e.Entries.Single().Reload();
                    result = 1;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            } while (saveFailed);

            return result;
        }
    }
}
