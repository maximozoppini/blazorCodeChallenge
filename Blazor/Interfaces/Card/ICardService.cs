using System.Threading.Tasks;
using Classes.Commands.Card;
using Classes.DTO;
using Classes.DTO.Card;

namespace hq_blazor_code_challenge.Interfaces.Card;

public interface ICardService
{
    Task<BaseResult> CreateCard(CardPost command);
    Task<CardsDTO> GetCards();
    Task<CardsDTO> GetCardById(string idCard);
}