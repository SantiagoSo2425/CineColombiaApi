using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Ciudad
{
    public int IdCiudad { get; set; }

    public int IdDepartamento { get; set; }

    public string Nombre { get; set; } = null!;
}
