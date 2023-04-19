namespace EShopTI.Product.Common.Domain.Aggregates.Category;

public abstract class BaseCategory : BaseAggregateRoot
{
    public string Title { get; protected set; }

    public BaseCategory()
    {
    }

    public BaseCategory(string id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidOperationException($"The value of {nameof(title)} cannot be null or empty. Please provide a valid {nameof(title)}!");
        }

        Id = id;
        Title = title;
    }

    public void Edit(string title)
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException($"This category is deletet and you cannot update it!");
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidOperationException($"The value of {nameof(title)} cannot be null or empty. Please provide a valid {nameof(title)}!");
        }

        Title = title;
    }
}
