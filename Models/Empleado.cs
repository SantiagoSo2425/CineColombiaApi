using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public int IdTeatro { get; set; }

    public int IdTipoDoc { get; set; }

    public string NumDocumento { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public DateOnly FechaIngreso { get; set; }

    public bool Activo { get; set; }

   // public virtual Teatro IdTeatroNavigation { get; set; } = null!;

    //public virtual TipoDocumento IdTipoDocNavigation { get; set; } = null!;

   // public virtual UsuarioSistema? UsuarioSistema { get; set; }

    //public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
