using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Product.Variant;

public record ProductVariantRemoveVM(string Id, string ProductId);

public class ProductVariantRemoveVMValidator : AbstractValidator<ProductVariantRemoveVM>
{
    public ProductVariantRemoveVMValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ProductId).NotEmpty();
    }
}