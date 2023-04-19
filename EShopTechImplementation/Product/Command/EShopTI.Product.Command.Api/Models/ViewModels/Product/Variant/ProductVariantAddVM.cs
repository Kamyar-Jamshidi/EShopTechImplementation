using EShopTI.Product.Common.Domain;
using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Product.Variant;

public record ProductVariantAddVM(Colors Color, Sizes Size, string ProductId, int Quantity, string PriceCurrency, decimal PriceAmount);

public class ProductVariantAddVMValidator : AbstractValidator<ProductVariantAddVM>
{
    public ProductVariantAddVMValidator()
    {
        RuleFor(m => m.Color).IsInEnum();
        RuleFor(m => m.Size).IsInEnum();
        RuleFor(m => m.ProductId).NotEmpty();
        RuleFor(m => m.Quantity).GreaterThanOrEqualTo(0);
        RuleFor(m => m.PriceCurrency).NotEmpty();
        RuleFor(m => m.PriceAmount).GreaterThan(0);
    }
}