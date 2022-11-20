using System.Net;
using Api.Models;
using Api.Persistence;
using AutoMapper;
using Classes.DTO;
using Classes.DTO.Card;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Business.Card_Business{
    public class GetCard
    {
        public class GetCardCommand : IRequest<CardsDTO>
        {
            public string Guid { get; set; }
        }
        
        public class ExecuteValidation : AbstractValidator<GetCardCommand>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.Guid).NotEmpty();
            }
        }


        public class Mediator : IRequestHandler<GetCardCommand, CardsDTO>
        {
            private readonly ContextDB _context;
            private readonly IMapper _mapper;
            
            public Mediator(ContextDB context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CardsDTO> Handle(GetCardCommand request, CancellationToken cancellationToken)
            {
                var result = new CardsDTO();
                try
                {
                    var guidCard = new Guid(request.Guid);
                    var card = await _context.Card.FirstOrDefaultAsync(c => c.Id.Equals(guidCard));

                    if (card != null)
                    {
                        var aux = _mapper.Map<Card, CardItem>(card);
                        result.Cards.Add(aux);
                        return result;    
                    }
                    
                    result.SetError("Card not found");
                    return result;
                }
                catch (Exception ex)
                {
                    result.SetError(ex.Message, HttpStatusCode.InternalServerError);
                    return result;
                }
            }
        }
    }
}