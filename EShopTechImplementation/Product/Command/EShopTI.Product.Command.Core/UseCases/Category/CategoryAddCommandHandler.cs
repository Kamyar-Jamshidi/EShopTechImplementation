using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Core.MessageBus;
using EShopTI.Product.Common.Events.Category;
using MediatR;
using CategoryDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Category.Category;

namespace EShopTI.Product.Command.Core.UseCases.Category;

public class CategoryAddCommandHandler : IRequestHandler<CategoryAddCommand, CategoryDomain>
{
    private readonly ICommandRepository<CategoryDomain> _commandRepository;
    private readonly IMessageBusClient _messageBusClient;

    public CategoryAddCommandHandler(ICommandRepository<CategoryDomain> commandRepository, IMessageBusClient messageBusClient)
    {
        _commandRepository = commandRepository;
        _messageBusClient = messageBusClient;
    }

    public async Task<CategoryDomain> Handle(CategoryAddCommand request, CancellationToken cancellationToken)
    {
        var categoryId = Guid.NewGuid().ToString();
        var category = new CategoryDomain(categoryId, request.Title);
        await _commandRepository.Insert(category, cancellationToken);

        _messageBusClient.Publish(new CategoryCreateEvent
        {
            Title = request.Title,
            Id = categoryId
        });

        return category;
    }
}
