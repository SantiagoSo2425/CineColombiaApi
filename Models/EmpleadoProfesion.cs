using System;
using System.Collections.Generic;

namespace CineColombiaApi.Models;

public partial class EmpleadoProfesion
{
    public int IdEmpProfesion { get; set; }

    public int IdEmpleado { get; set; }

    public int IdProfesion { get; set; }
}
