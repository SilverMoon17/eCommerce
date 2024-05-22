namespace Domain.Common;

public class BaseEntity
{
    public Guid Id { get; set; } = new();

    public DateTime TimeStamp { get; set; } = DateTime.Now; 
}