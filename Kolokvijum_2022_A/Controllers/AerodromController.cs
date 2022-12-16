using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Nova_fascikla__4_.Controllers;

[ApiController]
[Route("[controller]")]
public class AerodromController : ControllerBase
{
    AerodromContext Context;

    public AerodromController(AerodromContext c)
    {
        Context = c;
    }

    [Route("DodajAerodrom")]
    [HttpPost]
    public async Task<ActionResult> DodajAerodrom([FromBody] Aerodrom aerodrom)
    {
        try
        {
            await Context.Aerodromi.AddAsync(aerodrom);
            await Context.SaveChangesAsync();
            return Ok($"Dodat je aerodrom sa ID:{aerodrom.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    } 

    [Route("DodajLetelicu")]
    [HttpPost]
    public async Task<ActionResult> DodajLetelicu([FromBody] Letelica letelica)
    {
        try
        {
            await Context.Letelice.AddAsync(letelica);
            await Context.SaveChangesAsync();
            return Ok($"Dodata je letelica sa ID:{letelica.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    } 
    
    [Route("DodajLet/{idA}/{idB}/{idL}")]
    [HttpPost]
    public async Task<ActionResult> DodajLet([FromBody] Letovi let, int idA, int idB, int idL)
    {
        try
        {
            var aerodromA = Context.Aerodromi.Where(p => p.ID == idA).FirstOrDefault();
        if(aerodromA == null)
            return BadRequest("Nema aerodroma");
        var aerodromB = Context.Aerodromi.Where(p => p.ID == idB).FirstOrDefault();
        if(aerodromB == null)
            return BadRequest("Nema aerodroma");
        var letelica = Context.Letelice.Where(p => p.ID == idL).FirstOrDefault();
        if(letelica == null)
            return BadRequest("Nema letelice");

        if(let.BrojPutnika + letelica.Posada > letelica.KapacitetPutnika)
            return BadRequest("Putnici ne mogu da stanu u letelicu");
        let.TackaA = aerodromA;
        let.TackaB = aerodromB;
        let.Letelica = letelica;
            await Context.Letovi.AddAsync(let);
            await Context.SaveChangesAsync();
            return Ok($"Dodata je letelica sa ID:{let.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    } 


    [Route("PronadjiLet/{idA}")]
    [HttpGet]
    public async Task<ActionResult> PronadjiLet(int idA)
    {
        try
        {
            var let = await Context.Letovi
                         .Include(p => p.TackaA)
                         .Include(p => p.TackaB)
                         .Include(p => p.Letelica)
                         .Where(p => p.TackaA.ID == idA)
                         .Select(p =>
                         new{
                            NazivPrvogAerodorma = p.TackaA.Naziv,
                            NazivDrugogAerodorma = p.TackaB.Naziv,
                            NazivLetelice = p.Letelica.Naziv
                         })
                         .ToListAsync();

            return Ok(let);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("ProsecnaDuzina/{IdA1}/{IdA2}")]
    [HttpGet]
    public async Task<ActionResult> ProsecnaDuzina(int IdA1, int IdA2)
    {
        var let = Context.Letovi
                         .Include(p => p.TackaA)
                         .Include(p => p.TackaB)
                         .Where(p => p.TackaA.ID == IdA1 && p.TackaB.ID == IdA2)
                         .FirstOrDefault();

        if(let == null)
            return BadRequest("Nije pronadjen let");

        var duzina = (let.VremeSletanja.Subtract(let.VremePoletanja)).Hours;

        return Ok(duzina);

    }
}
