using System.Collections.Generic;
using System.Threading.Tasks;

namespace hq_blazor_code_challenge.Interfaces;

public interface IApiCalls
{
    Task<object> CallApi(string url, string method, Dictionary<string, string>? headers, object? comando);
}