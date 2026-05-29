using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class TipoSilla
{
    public int IdTipoSilla { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal PrecioBase { get; set; }

   // public virtual ICollection<Silla> Sillas { get; set; } = new List<Silla>();
}
