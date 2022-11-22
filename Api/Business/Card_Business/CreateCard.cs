using System.Net;
using System.Text.RegularExpressions;
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
                RuleFor(x => x.CardHolder).NotEmpty().NotNull().WithMessage("Card holder is required");
                RuleFor(x => x.CardNumber).NotEmpty().NotNull().WithMessage("Card number is required").Must(cardNumber => ValidateCardNumber(cardNumber)).WithMessage("Card number not correspond to VISA, MASTERCARD or AMEX");
                RuleFor(x => x.CardExpMonth).NotEmpty().NotNull().WithMessage("Card expiration month is required").GreaterThan(0).LessThanOrEqualTo(12).WithMessage("Card expiration month should be two positive digits");
                RuleFor(x => x.CardExpYear).NotEmpty().NotNull().WithMessage("Card expiration year is required").GreaterThan(0).LessThanOrEqualTo(99).WithMessage("Card expiration year should be two positive digits");
                RuleFor(x => x.Ccv).NotEmpty().MinimumLength(3).WithMessage("Card CVV is required").MaximumLength(4).Must(ccv => int.TryParse(ccv, out var val) && val > 0).WithMessage("Credit card security code should be a number between 3 and 4 digits");
                RuleFor(x => x.Type).NotEmpty();
                
                RuleFor(m => new {m.CardExpMonth, m.CardExpYear}).Must(x => ExpirationDateValidator(x.CardExpMonth, x.CardExpYear))
                    .WithMessage("Credit card is expired");
            }

            public bool ExpirationDateValidator(int month, int year)
            {
                if (year < (DateTime.Now.Year % 100))
                {
                    return false;
                }
                return year != (DateTime.Now.Year % 100) || month >= (DateTime.Now.Month);
            }

            public bool ValidateCardNumber(string cardNumber)
            {
                string visaPattern = @"^4[0-9]{12}(?:[0-9]{3})?$";
                string masterCardPattern = @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$";
                string amexPattern = @"^3[47][0-9]{13}$";
                Match validationRegex = Regex.Match(cardNumber, visaPattern, RegexOptions.IgnoreCase);
                if (validationRegex.Success)
                    return true;
                validationRegex = Regex.Match(cardNumber, masterCardPattern, RegexOptions.IgnoreCase);
                if (validationRegex.Success)
                    return true;
                validationRegex = Regex.Match(cardNumber, amexPattern, RegexOptions.IgnoreCase);
                if (validationRegex.Success)
                    return true;
                
                return false;
            }
        }

        public class Mediator : IRequestHandler<CreateCardCommand, BaseResult>
        {
            private readonly ContextDB _context;
            private readonly IValidator<CreateCardCommand> _validator;
            
            public Mediator(ContextDB context, IValidator<CreateCardCommand> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<BaseResult> Handle(CreateCardCommand request, CancellationToken cancellationToken)
            {
                var result = new BaseResult();

                var validation = await _validator.ValidateAsync(request);
                if (!validation.IsValid)
                {
                    var errors = string.Join(Environment.NewLine, validation.Errors);
                    result.SetError(errors, HttpStatusCode.InternalServerError);
                    return result;
                }
                
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