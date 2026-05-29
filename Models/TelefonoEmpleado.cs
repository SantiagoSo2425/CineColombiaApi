using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TelefonoEmpleado
{
    public int IdTelefonoEmp { get; set; }

    public int IdEmpleado { get; set; }

    public int IdTipoTelefono { get; set; }

    public string Numero { get; set; } = null!;
}
