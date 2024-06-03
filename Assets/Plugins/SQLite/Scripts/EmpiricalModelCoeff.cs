namespace Entity.Models
{
    public class EmpiricalModelCoeffs
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public double Value { get; set; }
        public int? EmpiricalModelId { get; set; }

        public virtual EmpiricalModels? EmpiricalModel { get; set; }
    }
}
