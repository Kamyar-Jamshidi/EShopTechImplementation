namespace EShopTI.Product.Common.Events.Category;

public class CategoryEditEvent : BaseEvent
{
    public CategoryEditEvent() : base(nameof(CategoryEditEvent))
    {
    }

    public string Title { get; set; }
}
