namespace EShopTI.Product.Common.Events;

public abstract class BaseEvent
{
    protected BaseEvent(string type)
    {
        Type = type;
    }

    public string Id { get; set; }
    public string Type { get; private set; }
    public int Version { get; set; }
}