namespace EShopTI.Product.Common.Domain.ValueObjects;

public class Money : IEquatable<Money>
{
    public string Currency { get; private set; }
    public decimal Amount { get; private set; }

    public Money(string currency, decimal amount)
    {
        Currency = currency;
        Amount = amount;
    }

    public bool Equals(Money other)
    {
        if (ReferenceEquals(other, null)) return false;
        if (ReferenceEquals(other, this)) return true;
        return Currency.Equals(other.Currency) && Amount.Equals(other.Amount);
    }

    public override bool Equals(object obj)
    {
        return Equals(obj as Money);
    }

    public override int GetHashCode()
    {
        return Currency.GetHashCode() ^ Amount.GetHashCode();
    }
}
