using Domain.EntitiesLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validations
{
    public class VideojuegoValidator: AbstractValidator<VideoJuego>
    {
        public VideojuegoValidator()
        {
            RuleFor(x => x.Titulo).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Compania).NotEmpty().WithMessage("La compañía es obligatoria.");
            RuleFor(x => x.AnioLanzamiento).GreaterThan(0).WithMessage("El año de lanzamiento debe ser mayor que 0.");
            RuleFor(x => x.Precio).GreaterThan(0).WithMessage("El precio debe ser mayor que 0.");
            RuleFor(x => x.PuntajePromedio).InclusiveBetween(0, 5).WithMessage("El puntaje debe estar entre 0 y 5.");
        }
    }
}
