namespace Boilerplate.Domain.Common;

public interface IAuditableEntity
{
    Guid CreatedBy { get; set; }

    DateTime CreatedAt { get; set; }

    Guid UpdatedBy { get; set; }

    DateTime? UpdatedAt { get; set; }
}