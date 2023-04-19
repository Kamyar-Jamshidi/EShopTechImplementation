using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Category
{
    public record CategoryAddVM(string Title);

    public class CategoryAddVMValidator : AbstractValidator<CategoryAddVM>
    {
        public CategoryAddVMValidator()
        {
            RuleFor(m => m.Title).NotEmpty();
        }
    }
}
