using System.Collections.Generic;
using System.Linq;
using apiSIA.Models;
using CineColombiaApi.Models;

namespace apiCine.Clases;

public class clsOpeUsuarioSistema
{
    private readonly CineColombiaContext oCine;
    public UsuarioSistema tblUsuarioSistema { get; set; }

    public clsOpeUsuarioSistema(CineColombiaContext oCine)
    {
        this.oCine = oCine;
        tblUsuarioSistema = new UsuarioSistema();
    }

    public List<UsuarioSistema> ListarUsuariosSistema()
    {
        return oCine.UsuarioSistemas.ToList();
    }

    public IQueryable ConsultarUsuarioSistema(int idUsuario)
    {
        return from x in oCine.UsuarioSistemas
               where x.IdUsuario == idUsuario
               select x;
    }

    public IQueryable ConsultarLogin(string username, string passwordHash)
    {
        return from x in oCine.UsuarioSistemas
               where x.Username == username
               && x.PasswordHash == passwordHash
               select x;
    }

    public int Agregar()
    {
        var existe = (from x in oCine.UsuarioSistemas
                      where x.Username == tblUsuarioSistema.Username
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        var existeEmpleado = (from x in oCine.UsuarioSistemas
                              where x.IdEmpleado == tblUsuarioSistema.IdEmpleado
                              select x).Any();

        if (existeEmpleado)
        {
            return -1;
        }

        oCine.UsuarioSistemas.Add(tblUsuarioSistema);
        return oCine.SaveChanges() > 0 ? 1 : 0;
    }

    public int Modificar()
    {
        var usuario = (from x in oCine.UsuarioSistemas
                       where x.IdUsuario == tblUsuarioSistema.IdUsuario
                       select x).FirstOrDefault();

        if (usuario == null)
        {
            return -2;
        }

        var existe = (from x in oCine.UsuarioSistemas
                      where x.IdUsuario != tblUsuarioSistema.IdUsuario
                      && x.Username == tblUsuarioSistema.Username
                      select x).Any();

        if (existe)
        {
            return -1;
        }

        var existeEmpleado = (from x in oCine.UsuarioSistemas
                              where x.IdUsuario != tblUsuarioSistema.IdUsuario
                              && x.IdEmpleado == tblUsuarioSistema.IdEmpleado
                              select x).Any();

        if (existeEmpleado)
        {
            return -1;
        }

        usuario.IdEmpleado = tblUsuarioSistema.IdEmpleado;
        usuario.IdRol = tblUsuarioSistema.IdRol;
        usuario.Username = tblUsuarioSistema.Username;
        usuario.PasswordHash = tblUsuarioSistema.PasswordHash;
        usuario.Activo = tblUsuarioSistema.Activo;
        usuario.UltimoLogin = tblUsuarioSistema.UltimoLogin;

        return oCine.SaveChanges() > 0 ? 1 : 0;
    }
}
