using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Core.MessageBus;
using EShopTI.Product.Common.Events.Category;
using MediatR;
using CategoryDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Category.Category;

namespace EShopTI.Product.Command.Core.UseCases.Category;

public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand>
{
    private readonly ICommandRepository<CategoryDomain> _commandRepository;
    private readonly IMessageBusClient _messageBusClient;

    public CategoryRemoveCommandHandler(ICommandRepository<CategoryDomain> commandRepository, IMessageBusClient messageBusClient)
    {
        _commandRepository = commandRepository;
        _messageBusClient = messageBusClient;
    }

    public async Task Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
    {
        var category = await _commandRepository.GetByIdAsync(request.Id, cancellationToken);
        category.Delete();
        await _commandRepository.Update(category, cancellationToken);

        _messageBusClient.Publish(new CategoryRemoveEvent
        {
            Id = request.Id
        });
    }
}
