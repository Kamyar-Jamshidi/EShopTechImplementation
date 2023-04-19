using EShopTI.Product.Command.Core.Data.Repositories;
using EShopTI.Product.Command.Core.MessageBus;
using EShopTI.Product.Common.Events.Category;
using MediatR;
using CategoryDomain = EShopTI.Product.Command.Core.Domain.Aggregates.Category.Category;

namespace EShopTI.Product.Command.Core.UseCases.Category;

public class CategoryEditCommandHandler : IRequestHandler<CategoryEditCommand, CategoryDomain>
{
    private readonly ICommandRepository<CategoryDomain> _commandRepository;
    private readonly IMessageBusClient _messageBusClient;

    public CategoryEditCommandHandler(ICommandRepository<CategoryDomain> commandRepository, IMessageBusClient messageBusClient)
    {
        _commandRepository = commandRepository;
        _messageBusClient = messageBusClient;
    }

    public async Task<CategoryDomain> Handle(CategoryEditCommand request, CancellationToken cancellationToken)
    {
        var category = await _commandRepository.GetByIdAsync(request.Id, cancellationToken);
        category.Edit(request.Title);
        await _commandRepository.Update(category, cancellationToken);

        _messageBusClient.Publish(new CategoryEditEvent
        {
            Title = request.Title,
            Id = request.Id
        });

        return category;
    }
}
