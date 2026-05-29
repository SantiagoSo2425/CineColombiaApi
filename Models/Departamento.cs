using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Departamento
{
    public int IdDepartamento { get; set; }

    public int IdPais { get; set; }

    public string Nombre { get; set; } = null!;
}
