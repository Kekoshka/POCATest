using FluentValidation;
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
                    .NotNull().WithMessage("Для электронной справки файл обязателей")
                    .Must(f => f!.Length > 0).WithMessage("Файл не может быть пустым")
                    .Must(f => f!.Length <= 50 * 1024 * 1024)
                    .WithMessage("Размер файла не может превышать 50 МБ");

                RuleFor(x => x.FileName)
                    .NotEmpty().WithMessage("Имя файла обязательно для электронной справки")
                    .MaximumLength(255).WithMessage("Имя файла не может превышать 255 символов")
                    .Matches(@"^[\w\-. ]+\.(doc|docx|xlsx|png|jpe?g|zip|7z|pdf)$")
                    .WithMessage("Недопустимое расширение (разрешены: doc, docx, xlsx, png, jpeg, zip, 7z, pdf)");
            });

            When(x => x.IsPhysical, () =>
            {
                RuleFor(x => x.File)
                    .Null().WithMessage("Бумажная справка не должна содержать файл");

                RuleFor(x => x.FileName)
                    .Null().WithMessage("Бумажная справка не должна содержать имя файла");
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
