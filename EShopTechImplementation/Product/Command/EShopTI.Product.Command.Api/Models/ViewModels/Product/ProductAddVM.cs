using FluentValidation;

namespace EShopTI.Product.Command.Api.Models.ViewModels.Product
{
    public record ProductAddVM(string Name, string Categoryid, int Quantity, string PriceCurrency, decimal PriceAmount);

    public class ProductAddVMValidator : AbstractValidator<ProductAddVM>
    {
        public ProductAddVMValidator()
        {
            RuleFor(m => m.Name).NotEmpty();
            RuleFor(m => m.Categoryid).NotEmpty();
            RuleFor(m => m.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(m => m.PriceCurrency).NotEmpty();
            RuleFor(m => m.PriceAmount).GreaterThan(0);
        }
    }
}
