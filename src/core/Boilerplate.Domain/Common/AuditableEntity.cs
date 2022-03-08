namespace Boilerplate.Domain.Common;

public class AuditableEntity : IAuditableEntity
{
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}