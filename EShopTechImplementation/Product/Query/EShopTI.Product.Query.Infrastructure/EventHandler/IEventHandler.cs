using EShopTI.Product.Common.Events.Category;
using EShopTI.Product.Common.Events.Product;

namespace EShopTI.Product.Query.Infrastructure.EventHandler;

public interface IEventHandler
{
    Task On(CategoryCreateEvent @event);
    Task On(CategoryEditEvent @event);
    Task On(CategoryRemoveEvent @event);
    Task On(ProductCreateEvent @event);
    Task On(ProductEditEvent @event);
    Task On(ProductRemoveEvent @event);
    Task On(ProductVariantCreateEvent @event);
    Task On(ProductVariantEditEvent @event);
    Task On(ProductVariantRemoveEvent @event);
}