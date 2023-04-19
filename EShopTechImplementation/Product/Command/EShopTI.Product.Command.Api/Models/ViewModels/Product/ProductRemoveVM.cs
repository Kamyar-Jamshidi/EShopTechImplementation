using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Product
{
    public record ProductRemoveVM(string Id);

    public class ProductRemoveVMValidator : AbstractValidator<ProductRemoveVM>
    {
        public ProductRemoveVMValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
