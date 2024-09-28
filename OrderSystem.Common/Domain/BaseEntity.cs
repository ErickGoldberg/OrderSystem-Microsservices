using OrderSystem.Common.Domain.Enums;

namespace OrderSystem.Common.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime UpdatedAt { get; protected set; } = DateTime.Now;
        public EntityStatusEnum EntityStatus { get; protected set; } = EntityStatusEnum.Actived;

        public virtual void Activate()
            => EntityStatus = EntityStatusEnum.Actived;

        public virtual void Deactivate()
            => EntityStatus = EntityStatusEnum.Deactivated;

        public virtual void Delete()
            => EntityStatus = EntityStatusEnum.Deleted;

        public virtual void SetUpdatedAtDate(DateTime updatedAtDate)
            => UpdatedAt = updatedAtDate;
    }
}
