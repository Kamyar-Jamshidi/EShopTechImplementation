using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Category
{
    public record CategoryRemoveVM(string Id);

    public class CategoryRemoveVMValidator : AbstractValidator<CategoryRemoveVM>
    {
        public CategoryRemoveVMValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }
}
