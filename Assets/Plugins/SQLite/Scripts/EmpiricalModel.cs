//using System.ComponentModel.DataAnnotations.Schema;

using System.Collections.Generic;

namespace Entity.Models
{
    public partial class EmpiricalModels
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int MaterialId { get; set; }
        public string Formula { get; set; } = null!;
        public int EquipmentId { get; set; }

        public virtual Materials Material { get; set; } = null!;
        public virtual Equipments Equipment { get; set; } = null!;
        public virtual List<EmpiricalModelCoeffs> EmpiricalModelCoeffs { get; set; } = null!; 
        public virtual List<ParamRange> ParamsRanges { get; set; } = null!;
        public virtual EmpiricalModelType Type { get; set; } = null!;

        #region NotMapped
       // [NotMapped]
        public string EmpiricalModelTypeAlias { get; set; } = null!;
        //[NotMapped]
        public string MaterialAlias { get; set; } = null!;
        //[NotMapped]
        public string EquipmentAlias { get; set; } = null!;
       // [NotMapped]
        public string RangeParamsAlias { get; set; } = null!;
        #endregion
    }
}
