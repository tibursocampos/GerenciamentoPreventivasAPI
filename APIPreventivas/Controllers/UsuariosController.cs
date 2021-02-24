using APIPreventivas.Models;
using APIPreventivas.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIPreventivas.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioService usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        //GET: api/Usuarios
       [HttpGet]
        public List<Usuario> GetUsuarios()
        {
            return usuarioService.GetUsuarios();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = usuarioService.GetUsuario(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpGet("busca")]
        public ActionResult<List<Usuario>> GetUsuarioNome(string nome)
        {
            var usuario = usuarioService.GetUsuarioNome(nome);
            if (usuario == null)
            {
                return NotFound(new { mensagem = "Usuário não encontrado !!! " });
            }
            return usuario;
        }

        [HttpGet("supervisores")]
        public ActionResult<List<Usuario>> GetSupervisores()
        {
            var usuario = usuarioService.GetSupervisores();
            if (usuario == null)
            {
                return NotFound(new { mensagem = "Supervisor não encontrado !!! " });
            }
            return usuario;
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public ActionResult PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return BadRequest();
            }

            try
            {
                usuarioService.PutUsuario(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        [HttpPost]
        public ActionResult<Usuario> PostUsuario(Usuario usuario)
        {
            var usuarioCriado = usuarioService.PostUsuario(usuario);

            return CreatedAtAction("GetUsuario", new { id = usuarioCriado.IdUsuario }, usuarioCriado);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public ActionResult<Usuario> DeleteUsuario(int id)
        {
            var usuario = usuarioService.DeleteUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        private bool UsuarioExists(int id)
        {
            return usuarioService.UsuarioExists(id);
        }
    }
}
