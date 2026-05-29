using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Teatro
{
    public int IdTeatro { get; set; }

    public string Nombre { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public bool Activo { get; set; }

   // public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    //public virtual ICollection<Sala> Salas { get; set; } = new List<Sala>();
}
