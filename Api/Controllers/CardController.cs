using Api.Business.Card_Business;
using Classes.Commands.Card;
using Classes.DTO;
using Classes.DTO.Card;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("api/card")]
        public async Task<ActionResult<BaseResult>> CreatCard([FromBody] CardPost command)
        {
            return await _mediator.Send(new CreateCard.CreateCardCommand
            {
                Ccv = command.Ccv,
                Type = command.Type,
                CardHolder = command.CardHolder,
                CardNumber = command.CardNumber,
                CardExpMonth = command.CardExpMonth,
                CardExpYear = command.CardExpYear
            });
        }
        
        [HttpGet]
        [Route("api/card")]
        public async Task<ActionResult<CardsDTO>> GetCards()
        {
            return await _mediator.Send(new GetCards.GetCardsCommand());
        }
        
        [HttpGet]
        [Route("api/cardbyid")]
        public async Task<ActionResult<CardsDTO>> GetCardById([FromHeader] string cardId)
        {
            return await _mediator.Send(new GetCard.GetCardCommand
            {
                Guid = cardId
            });
        }
   
    }
}