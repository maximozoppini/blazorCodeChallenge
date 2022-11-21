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
    public class GetCards
    {
        public class GetCardsCommand : IRequest<CardsDTO>
        {
         
        }


        public class Mediator : IRequestHandler<GetCardsCommand, CardsDTO>
        {
            private readonly ContextDB _context;
            private readonly IMapper _mapper;

            public Mediator(ContextDB context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CardsDTO> Handle(GetCardsCommand request, CancellationToken cancellationToken)
            {
                var result = new CardsDTO();
                try
                {
                    var cards = await _context.Card.ToListAsync();
                   
                    var aux = _mapper.Map<List<Card>, List<CardItem>>(cards);
                    result.Cards = aux;
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