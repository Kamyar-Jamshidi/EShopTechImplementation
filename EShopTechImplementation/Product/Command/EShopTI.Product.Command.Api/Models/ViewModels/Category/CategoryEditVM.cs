using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Category
{
    public record CategoryEditVM(string Id, string Title);

    public class CategoryEditVMValidator : AbstractValidator<CategoryEditVM>
    {
        public CategoryEditVMValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
            RuleFor(m => m.Title).NotEmpty();
        }
    }
}
