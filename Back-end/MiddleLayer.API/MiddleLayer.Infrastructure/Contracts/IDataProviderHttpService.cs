using MiddleLayer.Infrastructure.Models;

namespace MiddleLayer.Infrastructure.Contracts
{
    public interface IDataProviderHttpService
    {
        Task<Character> GetData();
    }
}
