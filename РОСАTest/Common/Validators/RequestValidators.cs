using FluentValidation;
using РОСАTest.Common.DTO;

namespace РОСАTest.Common.Validators
{
    public class CertificateRequestDTOValidator : AbstractValidator<CertificateRequestDTO>
    {
        public CertificateRequestDTOValidator()
        {
            RuleFor(x => x.RequestReason)
                .NotEmpty().WithMessage("Причина запроса не может быть пустой")
                .MaximumLength(500).WithMessage("Причина запроса не может превышать 500 символов");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Количество должно быть больше 0")
                .LessThanOrEqualTo(100).WithMessage("Количество не может превышать 100");

            RuleFor(x => x.CertificateTypeId)
                .NotEmpty().WithMessage("Тип сертификата обязателен");
        }
    }
    public class CreateRequestDTORequestValidator : AbstractValidator<CreateRequestDTORequest>
    {
        public CreateRequestDTORequestValidator()
        {
            RuleFor(x => x.Note)
                .MaximumLength(1000).WithMessage("Примечание не может превышать 1000 символов");

            RuleFor(x => x.CertificateRequests)
                .NotEmpty().WithMessage("Список сертификатов не может быть пустым")
                .Must(list => list.Count <= 10).WithMessage("Нельзя запросить более 10 сертификатов за раз");

            RuleForEach(x => x.CertificateRequests)
                .SetValidator(new CertificateRequestDTOValidator());
        }
    }

}
