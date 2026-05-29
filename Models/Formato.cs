using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Formato
{
    public int IdFormato { get; set; }

    public string Nombre { get; set; } = null!;

   // public virtual ICollection<Funcion> Funcions { get; set; } = new List<Funcion>();
}
