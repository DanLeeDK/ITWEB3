using Microsoft.AspNetCore.Hosting.Internal;
using WEBAfl3.Data.UnitOfWork;

namespace WEBAfl3.Data.Factory
{
    public class Factory : IFactory
    {
        public static string ConnectionString { get; set; }
        
        public IUnityOfWork GetUOF()
        {
            return new UnitOfWork.UnitOfWork(new ApplicationDbContext(ConnectionString));
        }
    }
}
