using EShopTI.Product.Common.Events.Category;
using EShopTI.Product.Common.Events.Product;
using EShopTI.Product.Query.Core.UseCases.Category;
using EShopTI.Product.Query.Core.UseCases.Product;
using EShopTI.Product.Query.Core.UseCases.Product.ProductVariant;
using MediatR;

namespace EShopTI.Product.Query.Infrastructure.EventHandler
{
    public class EventHandler : IEventHandler
    {
        private readonly IMediator _mediatr;

        public EventHandler(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task On(ProductCreateEvent @event)
        {
            var request = new ProductAddCommand(@event.Id, @event.Name, @event.CategoryId, @event.Quantity, @event.Price);
            await _mediatr.Send(request);
        }

        public async Task On(ProductEditEvent @event)
        {
            var request = new ProductEditCommand(@event.Id, @event.Name, @event.CategoryId, @event.Quantity, @event.Price);
            await _mediatr.Send(request);
        }

        public async Task On(ProductRemoveEvent @event)
        {
            var request = new ProductRemoveCommand(@event.Id);
            await _mediatr.Send(request);
        }

        public async Task On(ProductVariantCreateEvent @event)
        {
            var request = new ProductVariantAddCommand(@event.Id, @event.Color, @event.Size, @event.ProductId, @event.Quantity, @event.Price);
            await _mediatr.Send(request);
        }

        public async Task On(ProductVariantEditEvent @event)
        {
            var request = new ProductVariantEditCommand(@event.Id, @event.Color, @event.Size, @event.ProductId, @event.Quantity, @event.Price);
            await _mediatr.Send(request);
        }

        public async Task On(ProductVariantRemoveEvent @event)
        {
            var request = new ProductVariantRemoveCommand(@event.Id, @event.ProductId);
            await _mediatr.Send(request);
        }

        public async Task On(CategoryCreateEvent @event)
        {
            var request = new CategoryAddCommand(@event.Id, @event.Title);
            await _mediatr.Send(request);
        }

        public async Task On(CategoryEditEvent @event)
        {
            var request = new CategoryEditCommand(@event.Id, @event.Title);
            await _mediatr.Send(request);
        }

        public async Task On(CategoryRemoveEvent @event)
        {
            var request = new CategoryRemoveCommand(@event.Id);
            await _mediatr.Send(request);
        }
    }
}