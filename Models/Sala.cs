using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class Sala
{
    public int IdSala { get; set; }

    public int IdTeatro { get; set; }

    public int IdTipoSala { get; set; }

    public string NombreSala { get; set; } = null!;

    public int CapacidadTotal { get; set; }

    public bool Activo { get; set; }

   // public virtual ICollection<Funcion> Funcions { get; set; } = new List<Funcion>();

    //public virtual Teatro IdTeatroNavigation { get; set; } = null!;

    //public virtual TipoSala IdTipoSalaNavigation { get; set; } = null!;

//    public virtual ICollection<Silla> Sillas { get; set; } = new List<Silla>();
}
