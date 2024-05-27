namespace Entity.Models
{
    public class ReferenceValues
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int QualityId { get; set; }
        public double Error { get; set; }
        public double Value { get; set; }

        public virtual Tasks? Task { get; set; }
        public virtual Qualities Quality { get; set; } = null!;
    }
}
