using System;
using System.Collections.Generic;
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
    public class eventosController : ControllerBase
    {
        private readonly BaseDatos _context;

        public eventosController(BaseDatos context)
        {
            _context = context;
        }

        // GET: api/eventos
        [HttpGet]
        public IEnumerable<object> Getusuarios()
        {
            //return _context.eventos;
            var actual = DateTime.Now;
            List<reportes> report = new List<reportes>();

            var listaReferencia = (from e in _context.eventos
                                   join u in _context.usuarios on e.idUsuario equals u.idUsuario
                                  
                                   select new
                                   {
                                       idEvento = e.idEvento,
                                       fecha = e.fecha,
                                       evento = e.evento,
                                       lugar = e.lugar,
                                       costo = e.costo,
                                       nombre = u.nombre + " " + u.apellido

                                   }).ToList();


            foreach (var item in listaReferencia)
            {

                var reportesobj = new reportes();

                reportesobj.idEvento = item.idEvento;
                reportesobj.fecha = item.fecha;
                reportesobj.evento = item.evento;
                reportesobj.lugar = item.lugar;
                reportesobj.costo = Convert.ToDecimal(item.costo);
                reportesobj.nombre = item.nombre;
                int dayDiff = (item.fecha - actual).Value.Days;
                reportesobj.dias = dayDiff;

                report.Add(reportesobj);




            }




            return report;

        }



        [HttpGet]
        [Route("reportes")]
        public IEnumerable<object> GetReporte(string fechaDesde, string fechaHasta) {

            DateTime desde = DateTime.Parse(fechaDesde);
            DateTime hasta  = DateTime.Parse(fechaHasta);
            var actual = DateTime.Now;
            List<reportes> report = new List<reportes>();





            var listaReferencia = (from e in _context.eventos
                                   join u in _context.usuarios on e.idUsuario equals u.idUsuario
                                   
                                   where e.fecha.Value.Date>=desde.Date && e.fecha.Value.Date <= hasta.Date  

                                   select  new
                                   {
                                       idEvento = e.idEvento,
                                       fecha = e.fecha,
                                       evento = e.evento,
                                       lugar = e.lugar,
                                       costo = e.costo,
                                       nombre = u.nombre + " " + u.apellido


                                   }).ToList();


            foreach (var item in listaReferencia)
            {

                var reportesobj = new reportes();

                reportesobj.idEvento = item.idEvento;
                reportesobj.fecha = item.fecha;
                reportesobj.evento = item.evento;
                reportesobj.lugar = item.lugar;
                reportesobj.costo = Convert.ToDecimal (item.costo);
                reportesobj.nombre = item.nombre;
                int dayDiff = ( item.fecha - actual).Value.Days;
                reportesobj.dias = dayDiff;

                report.Add(reportesobj);


             

            }


     

            return report;


        }





        // GET: api/eventos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Geteventos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventos = await _context.eventos.FindAsync(id);

            if (eventos == null)
            {
                return NotFound();
            }

            return Ok(eventos);
        }

        // PUT: api/eventos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Puteventos([FromRoute] int id, [FromBody] eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventos.idEvento)
            {
                return BadRequest();
            }

            _context.Entry(eventos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!eventosExists(id))
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

        // POST: api/eventos
        [HttpPost]
        public async Task<IActionResult> Posteventos([FromBody] eventos eventos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.eventos.Add(eventos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Geteventos", new { id = eventos.idEvento }, eventos);
        }

        // DELETE: api/eventos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteeventos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var eventos = await _context.eventos.FindAsync(id);
            if (eventos == null)
            {
                return NotFound();
            }

            _context.eventos.Remove(eventos);
            await _context.SaveChangesAsync();

            return Ok(eventos);
        }

        private bool eventosExists(int id)
        {
            return _context.eventos.Any(e => e.idEvento == id);
        }
    }
}