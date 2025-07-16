namespace AdeptusMart01.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime RegisterTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
