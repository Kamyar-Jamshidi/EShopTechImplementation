namespace EShopTI.Product.Common.Events.Category;

public class CategoryCreateEvent : BaseEvent
{
    public CategoryCreateEvent() : base(nameof(CategoryCreateEvent))
    {
    }

    public string Title { get; set; }
}