using System.Threading.Tasks;

namespace TDDxUnitCore.Domain._Base
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
