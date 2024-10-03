using FluentValidation;

namespace Application.Validations
{
    public class TopRankingValidator : AbstractValidator<int?>
    {
        public TopRankingValidator()
        {
            RuleFor(x => x)
                .Must(x => !x.HasValue || x.Value > 0)
                .WithMessage("El valor ingresado para el Top debe ser mayor que 0.");
        }
    }

}
