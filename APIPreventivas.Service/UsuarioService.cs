using APIPreventivas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace APIPreventivas.Service
{
    public interface IUsuarioService
    {
        List<Usuario> GetUsuarios();
        List<Usuario> GetSupervisores();
        List<Usuario> GetTecnicos();
        Usuario GetUsuario(int idUsuario);
        List<Usuario> GetUsuarioNome(string nome);
        Usuario PostUsuario(Usuario usuario);
        void PutUsuario(Usuario usuario);
        Usuario DeleteUsuario(int idUsuario);
        bool UsuarioExists(int idUsuario);
    }
    public class UsuarioService : IUsuarioService
    {
        private readonly APIPreventivaContext db;
        public UsuarioService(APIPreventivaContext context)
        {
            db = context;
        }

        public List<Usuario> GetUsuarios()
        {
            return db.Usuarios.OrderBy(u => u.PrimeiroNome).ToList();
        }

        public List<Usuario> GetSupervisores()
        {
            return db.Usuarios.Where(u => u.Permissao == Domain.Enum.TipoUsuarioEnum.TipoUsuario.supervisor).ToList();
        }

        public List<Usuario> GetTecnicos()
        {
            return db.Usuarios.Where(u => u.Permissao == Domain.Enum.TipoUsuarioEnum.TipoUsuario.tecnico).ToList();
        }

        public Usuario GetUsuario(int idUsuario)
        {
            var usuario = db.Usuarios.Find(idUsuario);

            return usuario;
        }

        public List<Usuario> GetUsuarioNome(string nome)
        {
            var usuarios = db.Usuarios.Where(u => u.PrimeiroNome.Contains(nome)).ToList();

            return usuarios;
        }

        public Usuario PostUsuario(Usuario usuario)
        {
            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return usuario;
        }

        public void PutUsuario(Usuario usuario)
        {
            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Usuario DeleteUsuario(int idUsuario)
        {
            var usuario = db.Usuarios.Find(idUsuario);
            db.Usuarios.Remove(usuario);

            return usuario;
        }

        public bool UsuarioExists(int idUsuario)
        {
            return db.Usuarios.Any(e => e.IdUsuario == idUsuario);
        }
    }
}
