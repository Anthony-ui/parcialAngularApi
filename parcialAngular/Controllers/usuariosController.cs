using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos;
using parcialAngular.Models;

namespace parcialAngular.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly BaseDatos _context;

        public usuariosController(BaseDatos context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public IEnumerable<usuarios> Getusuarios()
        {
            return _context.usuarios;
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Getusuarios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarios = await _context.usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return Ok(usuarios);
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putusuarios([FromRoute] int id, [FromBody] usuarios usuarios)
        {
            usuarios.idUsuario = id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuarios.idUsuario)
            {
                return BadRequest();
            }

            _context.Entry(usuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usuariosExists(id))
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

        // POST: api/usuarios
        [HttpPost]
        public async Task<IActionResult> Postusuarios([FromBody] usuarios usuarios)
        {

            usuarios.fecha = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusuarios", new { id = usuarios.idUsuario }, usuarios);
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteusuarios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarios = await _context.usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return Ok(usuarios);
        }

        private bool usuariosExists(int id)
        {
            return _context.usuarios.Any(e => e.idUsuario == id);
        }



        [HttpGet]
        [Route("inicio")]

        public object login(string usuario, string clave)
        {
            var compr = _context.usuarios.Where(x => x.usuario == usuario && x.clave == clave);
            if (compr.Count() > 0)
            {
                return compr;
            }
            else
            {
                return Ok(false);
            }



        }



        [HttpPost, DisableRequestSizeLimit]
        [Route("guardar")]
        public string subir()
        {
            try
            {
                var foto = Request.Form.Files[0];
                var directorio = Path.Combine("Imagenes", "Contenido");
                var f = Path.Combine(Directory.GetCurrentDirectory(), directorio);



                if (foto.Length > 0)
                {
                   
                    var vec = foto.FileName.Split(".");
                    var len = foto.FileName.Split(".").Length;
                    var ext = "." + vec[len - 1];
                    var nombreArchivo = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ext;
                    var rutaFinal = Path.Combine(f, nombreArchivo);
                    var rutaBaseDatos = Path.Combine(directorio, nombreArchivo);
                    using (var stream = new FileStream(rutaFinal, FileMode.Create))
                    {
                        foto.CopyTo(stream);
                    }
                    return rutaBaseDatos;
                }
                else
                {
                    return "error";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "error";
            }
        }

    }
}