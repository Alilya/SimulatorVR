using System.Collections.Generic;

namespace Entity.Models
{
    public class EmpiricalModelType
    {
        public int Id { get; set; }
        public string Alias { get; set; } = null!;
        public string UnitAlias { get; set; } = null!;

        public virtual List<EmpiricalModels>? EmpiricalModels { get; set;}
    }
}
