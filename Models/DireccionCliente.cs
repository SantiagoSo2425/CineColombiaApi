using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class DireccionCliente
{
    public int IdDireccionCli { get; set; }

    public int IdCliente { get; set; }

    public int IdCiudad { get; set; }

    public string Direccion { get; set; } = null!;

    public bool Activo { get; set; }
}
