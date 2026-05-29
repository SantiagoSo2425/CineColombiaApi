using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class DireccionEmpleado
{
    public int IdDireccionEmp { get; set; }

    public int IdEmpleado { get; set; }

    public int IdCiudad { get; set; }

    public string Direccion { get; set; } = null!;

    public bool Activo { get; set; }
}
