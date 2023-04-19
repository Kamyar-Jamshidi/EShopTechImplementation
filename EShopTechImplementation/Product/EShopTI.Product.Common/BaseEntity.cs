using MediatR;
using System.Text.Json.Serialization;

namespace EShopTI.Product.Common
{
    public abstract class BaseEntity
    {
        public string Id { get; protected set; }
        public bool IsDeleted { get; protected set; } = false;

        public virtual void Delete()
        {
            IsDeleted = true;
        }
    }
}
