namespace EShopTI.Product.Query.Infrastructure.EventProcessing;

public interface IEventProcessor
{
    void ProcessEvent(string message);
}