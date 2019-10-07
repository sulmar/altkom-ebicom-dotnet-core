namespace Altkom.DotnetCore.Models
{
    public abstract class BaseEntity<TKey>
    {   
        public TKey Id { get; set; }
    }
}
