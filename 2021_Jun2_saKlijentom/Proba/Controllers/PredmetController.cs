using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Proba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredmetController : ControllerBase
    {
        public FakultetContext Context { get; set; }

        public PredmetController(FakultetContext context)
        {
            Context = context;
        }

        [Route("PreuzmiPredmete")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                return Ok(await Context.Predmeti.Select(p => new { p.ID, p.Naziv }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodatiPredmet")]
        [HttpPost]
        public async Task<ActionResult> DodatiPredmet([FromBody] Predmet predmet)
        {
            if (predmet.Godina < 1 && predmet.Godina > 5)
            {
                return BadRequest("Pogrešna Godina!");
            }

            // ... Ostale provere, Naziv

            try
            {
                Context.Predmeti.Add(predmet);
                await Context.SaveChangesAsync();
                return Ok($"Predmet je dodat! ID je: {predmet.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenitiPremet")]
        [HttpPut]
        public async Task<ActionResult> PromenitiPredmet([FromBody] Predmet predmet)
        {
            if (predmet.ID <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            if (predmet.Godina < 1 && predmet.Godina > 5)
            {
                return BadRequest("Pogrešna Godina!");
            }

            // ... Ostale provere, Naziv

            try
            {
                Context.Predmeti.Update(predmet);

                await Context.SaveChangesAsync();
                return Ok($"Uspešno izmenjen predmet! ID je: {predmet.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiPredmet")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisatiPredmet(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            try
            {
                var predmet = await Context.Predmeti.FindAsync(id);
                Context.Predmeti.Remove(predmet);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan predmet: {predmet.Naziv}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
