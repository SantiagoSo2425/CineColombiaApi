using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class MetodoPago
{
    public int IdMetodoPago { get; set; }

    public string Nombre { get; set; } = null!;

   // public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
