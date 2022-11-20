using System.Net;
using Api.Models;
using Api.Persistence;
using Classes.DTO;
using FluentValidation;
using MediatR;

namespace Api.Business.Card_Business{
    public class CreateCard
    {
        public class CreateCardCommand : IRequest<BaseResult>
        {
            public string CardHolder { get; set; }
            public string CardNumber { get; set; }
            public int CardExpMonth { get; set; }
            public int CardExpYear { get; set; }
            public string Ccv { get; set; }
            public string Type { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<CreateCardCommand>
        {
            public ExecuteValidation()
            {
                RuleFor(x => x.CardHolder).NotEmpty().NotNull();
                RuleFor(x => x.CardNumber).NotEmpty().NotNull();
                RuleFor(x => x.CardExpMonth).NotEmpty().NotNull().GreaterThan(0).LessThanOrEqualTo(12).WithMessage("Card expiration month should be two positive digits");
                RuleFor(x => x.CardExpYear).NotEmpty().NotNull().GreaterThan(0).LessThanOrEqualTo(99).WithMessage("Card expiration year should be two positive digits");
                RuleFor(x => x.Ccv).NotEmpty().MinimumLength(3).MaximumLength(4).Must(ccv => int.TryParse(ccv, out var val) && val > 0).WithMessage("Credit card security code should be a number between 3 and 4 digits");
                RuleFor(x => x.Type).NotEmpty();
                
                RuleFor(m => new {m.CardExpMonth, m.CardExpYear}).Must(x => ExpirationDateValidator(x.CardExpMonth, x.CardExpYear))
                    .WithMessage("Credit card is expired");
            }

            public bool ExpirationDateValidator(int month, int year)
            {
                if (month > (DateTime.Now.Month + 1))
                    return false;
                return year <= (DateTime.Now.Year % 100);
            }
            
            // public bool MasterCard
        }

        public class Mediator : IRequestHandler<CreateCardCommand, BaseResult>
        {
            private readonly ContextDB _context;
            
            public Mediator(ContextDB context)
            {
                _context = context;
            }

            public async Task<BaseResult> Handle(CreateCardCommand request, CancellationToken cancellationToken)
            {
                var result = new BaseResult();
                try
                {
                    var card = new Card
                    {
                        Ccv = request.Ccv,
                        Id = Guid.NewGuid(),
                        Type = request.Type,
                        CardHolder = request.CardHolder,
                        CardNumber = request.CardNumber,
                        CreationDate = DateTime.Now,
                        CardExpMonth = request.CardExpMonth,
                        CardExpYear = request.CardExpYear
                    };

                    await _context.AddAsync(card);
                    await _context.SaveChangesAsync();

                    result.StatusCode = HttpStatusCode.Created;
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