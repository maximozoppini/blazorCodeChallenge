using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Classes.Commands.Card;
using Classes.CustomExceptions;
using Classes.DTO;
using Classes.DTO.Card;
using hq_blazor_code_challenge.Interfaces;
using hq_blazor_code_challenge.Interfaces.Card;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;

namespace hq_blazor_code_challenge.Services.Card;

public class CardService : ICardService
{
    private IConfiguration _config { get; set; }
    private IApiCalls _apiCalls { get; set; }

    public CardService(IConfiguration config, IApiCalls apiCalls)
    {
        _config = config;
        _apiCalls = apiCalls;
    }

    public async Task<BaseResult> CreateCard(CardPost command)
    {
        var resultado = new BaseResult();
        try
        {
            var urlApi = _config["ApiURL"] + "api/card";

            var contenido = await _apiCalls.CallApi(urlApi, "POST", null, command);
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            resultado = JsonSerializer.Deserialize<BaseResult>(contenido.ToString(), options);

            return resultado;
        }
        catch (ApiCallException ex)
        {
            resultado.SetError(ex.Message, ex._statusCode);
            return resultado;
        }
        catch (Exception ex)
        {
            resultado.SetError("Error while saving card", HttpStatusCode.InternalServerError);
            return resultado;
        }
    }

    public async Task<CardsDTO> GetCards()
    {
        var resultado = new CardsDTO();
        try
        {
            var urlApi = _config["ApiURL"] + "api/card";
            var contenido = await _apiCalls.CallApi(urlApi, "GET", null, null);
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            resultado = JsonSerializer.Deserialize<CardsDTO>(contenido.ToString(), options);

            return resultado;
        }
        catch (ApiCallException ex)
        {
            resultado.SetError(ex.Message, ex._statusCode);
            return resultado;
        }
        catch (Exception ex)
        {
            resultado.SetError("Error while getting cards", HttpStatusCode.InternalServerError);
            return resultado;
        }
    }

    public async Task<CardsDTO> GetCardById(string idCard)
    {
        var resultado = new CardsDTO();
        try
        {
            var headers = new Dictionary<string, string>();
            headers.Add("cardId", idCard);
            
            var urlApi = _config["ApiURL"] + "api/cardbyid";
            var contenido = await _apiCalls.CallApi(urlApi, "GET", headers, null);
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            resultado = JsonSerializer.Deserialize<CardsDTO>(contenido.ToString(), options);

            return resultado;
        }
        catch (ApiCallException ex)
        {
            resultado.SetError(ex.Message, ex._statusCode);
            return resultado;
        }
        catch (Exception ex)
        {
            resultado.SetError("Error while getting card by id", HttpStatusCode.InternalServerError);
            return resultado;
        }
    }
}