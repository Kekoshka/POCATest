using FluentValidation;
using System.Text.RegularExpressions;
using РОСАTest.Common.DTO;

namespace РОСАTest.Common.Validators
{
    public class CreateResponseDTORequestValidator : AbstractValidator<CreateResponseDTORequest>
    {
        public CreateResponseDTORequestValidator()
        {
            RuleFor(x => x.CertificateRequestId)
                .NotEmpty().WithMessage("Id запроса сертификата обязателен");

            RuleFor(x => x.IsPhysical)
                .NotNull().WithMessage("Необходимо указать тип справки");

            When(x => !x.IsPhysical, () =>
            {
                RuleFor(x => x.File)
                    .NotNull().WithMessage("Файл обязателен для электронной справки")
                    .Must(f => f!.Length > 0).WithMessage("Файл не может быть пустым")
                    .Must(f => f!.Length <= 10 * 1024 * 1024)
                    .WithMessage("Размер файла не может превышать 10 МБ")
                    .Must(f => Regex.IsMatch(
                        f!.FileName,
                        @"^[\w\-. ]+\.(doc|docx|xlsx|png|jpe?g|zip|7z|pdf)$"))
                    .WithMessage("Недопустимое расширение");
            });

            When(x => x.IsPhysical, () =>
            {
                RuleFor(x => x.File)
                    .Null().WithMessage("Бумажная справка не должна содержать файл");
            });
        }
    }

    public class CreateResponseListValidator : AbstractValidator<List<CreateResponseDTORequest>>
    {
        public CreateResponseListValidator()
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Список ответов не может быть пустым")
                .Must(list => list.Count <= 10)
                .WithMessage("Нельзя загрузить более 10 справок за раз");

            RuleFor(x => x)
                .Must(list =>
                    list.Select(d => d.CertificateRequestId).Distinct().Count() == list.Count)
                .WithMessage("В списке есть дублирующиеся запросы сертификатов");

            RuleForEach(x => x)
                .SetValidator(new CreateResponseDTORequestValidator());
        }
    }
}
