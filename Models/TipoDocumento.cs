using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TipoDocumento
{
    public int IdTipoDoc { get; set; }

    public string Codigo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

   // public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

   // public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
