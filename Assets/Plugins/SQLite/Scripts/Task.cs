using System.Collections.Generic;
using System.ComponentModel;

namespace Entity.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public int ScriptId { get; set; }
        public int OvenTypeId { get; set; }
        public int MaterialId { get; set; }
        public string Reference { get; set; } = null!;
        public int? EmergencySituationId { get; set; }
        public int QualityId { get; set; }


        public virtual Equipments OvenType { get; set; } = null!;
        public virtual Materials Material { get; set; } = null!;
        public virtual Scripts Script { get; set; } = null!;
        public virtual List<ReferenceValues>? ReferenceValues { get; set; }
        public virtual EmergencySituations? EmergencySituation { get; set; }
        public virtual Qualities Quality { get; set; } = null!;
        public override string ToString() {
            return string.Format("[Tasks: Id={0}, ScriptId={1},  QualityId={2}," +
                " OvenTypeId={3},  MaterialId={4}, OvenType={5}," +
               " Quality={6},Material={7},  Script={8}]",
                Id, ScriptId, QualityId, OvenTypeId, MaterialId, OvenType, Quality, Material, Script);
        }
    }
}
