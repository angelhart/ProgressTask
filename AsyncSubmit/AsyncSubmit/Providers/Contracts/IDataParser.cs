using System.Net.Http;

namespace AsyncSubmit.Providers.Contracts
{
    public interface IDataParser
    {
        StringContent Serialize<T>(T model);
    }
}