using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Classes.CustomExceptions;
using hq_blazor_code_challenge.Interfaces;
using Microsoft.AspNetCore.Components;

namespace hq_blazor_code_challenge.Services;

public class ApiCalls : IApiCalls
{
    private readonly HttpClient _httpClient;
   
    private NavigationManager _navManager { get; set; }

    public ApiCalls(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> CallApi(string url, string method, Dictionary<string, string>? headers, object? comando)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Clear();
            var urlApi = url;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(entry.Key, entry.Value);
                }
            }

            var response = new HttpResponseMessage();

            switch (method)
            {
                case "GET":
                    response = await _httpClient.GetAsync(urlApi);
                    break;
                case "POST":
                    var comandoPOSTJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(comando),
                        Encoding.UTF8, "application/json");
                    response = await _httpClient.PostAsync(urlApi, comandoPOSTJson);
                    break;
                case "PUT":
                    var comandoPUTJson = new StringContent(System.Text.Json.JsonSerializer.Serialize(comando),
                        Encoding.UTF8, "application/json");
                    response = await _httpClient.PutAsync(urlApi, comandoPUTJson);
                    break;
                case "DELETE":

                    break;
            }

            var contenido = await response.Content.ReadAsStringAsync();

            return contenido;
        }
        catch (Exception ex)
        {
            throw new ApiCallException(((int) HttpStatusCode.InternalServerError).ToString());
        }
    }
}