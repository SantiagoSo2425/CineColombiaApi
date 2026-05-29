using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TelefonoCliente
{
    public int IdTelefono { get; set; }

    public int IdCliente { get; set; }

    public int IdTipoTelefono { get; set; }

    public string Numero { get; set; } = null!;
}
