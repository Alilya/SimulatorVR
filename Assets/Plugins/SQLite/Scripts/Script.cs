using SQLite4Unity3d;

namespace Entity.Models
{
    public class Scripts
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public string Protocol { get; set; } = null!;
        public int? TaskId { get; set; }
        public int InstructorId { get; set; }
        public int TraineeId { get; set; }

        public virtual Users? Trainee { get; set; }
        public virtual Users? Instructor { get; set; }
        public virtual Tasks? Task { get; set; }

        public override string ToString() {
            return string.Format("[Scripts: Id={0}, Status={1},  Protocol={2}, TaskId={3},  InstructorId={4}, TraineeId={5} ]",
                Id, Status, Protocol, TaskId, InstructorId, TraineeId);
        }
    }
}
