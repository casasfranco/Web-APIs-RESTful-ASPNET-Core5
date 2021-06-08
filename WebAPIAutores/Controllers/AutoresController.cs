using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers
{

    //[HttpGet("/primer")] Para ignorar "Autores" en la url de la api

    [ApiController]
    [Route("api/[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        //api/Autores
        [HttpGet]
        public async Task<ActionResult<Autor>> Get()
        {
            return await context.Autores.FirstOrDefaultAsync();
        }

        //api/Autores/primer
        [HttpGet("primer")]
        public async Task<ActionResult<Autor>> GetPrimerAutor()
        {
            var autor = await context.Autores.FirstOrDefaultAsync();

            if(autor == null)
            {
                return NotFound();
            }

            return autor;
        }

        //api/Autores
        [HttpPost]
        public async Task<ActionResult> Post(Autor autor)
        {
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        //api/Autores/:id
        [HttpPut("{id:int}")]   // api/autores/:id
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if(autor.Id != id)
            {
                return BadRequest("El id del autor no coincide con el id de la URL");
            }

            //Comprueba existencia
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        //api/Autores/:id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        { 
            //Comprueba existencia
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
