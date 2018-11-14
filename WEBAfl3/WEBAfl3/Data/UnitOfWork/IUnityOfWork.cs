using System;
using WEBAfl3.Data.Repository;

namespace WEBAfl3.Data.UnitOfWork
{
    public interface IUnityOfWork : IDisposable
    {
        IComponentRepository Components { get; }
        ICategoryRepository Categories { get; }
        IComponentTypeRepository ComponentTypes { get; }
        
        int Complete();
    }
}
