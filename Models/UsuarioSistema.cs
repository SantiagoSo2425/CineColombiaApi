using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class UsuarioSistema
{
    public int IdUsuario { get; set; }

    public int IdEmpleado { get; set; }

    public int IdRol { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime? UltimoLogin { get; set; }

   // public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    //public virtual Rol IdRolNavigation { get; set; } = null!;
}
