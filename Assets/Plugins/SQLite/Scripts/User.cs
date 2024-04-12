using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Description("Логин")]
        public string Login { get; set; } = null!;

        [Description("Пароль")]
        public string Password { get; set; } = null!;

        [Description("Администратор")]
        public bool IsAdmin { get; set; }
        public int? RoleId { get; set; }

        [Description("Роль")]
        public virtual Role? Role { get; set; }
        public virtual List<Scripts>? ScriptsTrainee { get; set; }
        public virtual List<Scripts>? ScriptsInstructor { get; set; }

        //[NotMapped]
        [Description("Роль")]
        public string RoleAlias { get; set; } = null!;

        public override string ToString() {
            return string.Format("[Users: Id={0}, Login={1},  Password={2}, IsAdmin={3},  RoleId={4}]",
                Id, Login, Password, IsAdmin, RoleId);
        }
    }
}
