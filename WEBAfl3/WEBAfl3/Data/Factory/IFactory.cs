using WEBAfl3.Data.UnitOfWork;

namespace WEBAfl3.Data.Factory
{
    public interface IFactory
    {
        IUnityOfWork GetUOF();
    }
}
