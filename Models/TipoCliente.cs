using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TipoCliente
{
    public int IdTipoCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

   // public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
