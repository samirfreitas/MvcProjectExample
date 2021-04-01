using Example.Business.Models.Validations.Docs;
using FluentValidation;

namespace Example.Business.Models.Validations
{
    public class VendorValidation : AbstractValidator<Vendor>
    {
        public VendorValidation()
        {
            RuleFor(f => f.Name)
                .NotNull().WithMessage("O Campo {PropertyName} precisa ser fornecido.")
                .NotEmpty().WithMessage("O Campo {PropertyName} precisa ser fornecido.")
                .Length(2, 100).WithMessage("O Campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            When(f => f.VendorType == VendorType.PhysicalPerson, () => {

                RuleFor(f => f.Document.Length).Equal(CpfValidation.LenghtCpf)
                .WithMessage("O campo documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(f => CpfValidation.Validation(f.Document)).Equal(true)
                .WithMessage("O documento fornecido e invalido.");

            });    

            When(f => f.VendorType == VendorType.LegalPearson, () => {

                RuleFor(f => f.Document.Length).Equal(CnpjValidation.LenghtCnpj)
                .WithMessage("O campo documento precisa ter {ComparisonValue} caracteres e foi fornecido {PropertyValue}.");

                RuleFor(f => CnpjValidation.Validation(f.Document)).Equal(true)
                .WithMessage("O documento fornecido e invalido.");
            });    
        }
    }
}
