using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Jun2_2022.Controllers;

[ApiController]
[Route("[controller]")]
public class KompanijaController : ControllerBase
{
    Context context;
    public KompanijaController(Context c)
    {
        context = c;
    }

    [Route("dodajRobu")]
    [HttpPost]
    public async Task<ActionResult> DodajRobu([FromBody] Roba roba)
    {
        if(roba == null)
            return BadRequest("Nemostojeca roba");
        
        try
        {
            await context.Roba.AddAsync(roba);
            await context.SaveChangesAsync();

            return Ok($"Dodata je roba sa ID:{roba.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("dodajKompaniju")]
    [HttpPost]
    public async Task<ActionResult> DodajKompaniju([FromBody] Kompanija kom)
    {
        if(kom == null)
            return BadRequest("Nemostojeca Kompanija");
        
        try
        {
            await context.Kompanija.AddAsync(kom);
            await context.SaveChangesAsync();

            return Ok($"Dodata je roba sa ID:{kom.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("dodajVozilo")]
    [HttpPost]
    public async Task<ActionResult> DodajVozilo([FromBody] Vozilo vozilo)
    {
        if(vozilo == null)
            return BadRequest("Nemostojece vozilo");
        
        try
        {
            await context.Vozilo.AddAsync(vozilo);
            await context.SaveChangesAsync();

            return Ok($"Dodata je roba sa ID:{vozilo.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [Route("Pronadji/{idR}")]
    [HttpGet]
    public async Task<ActionResult> Pronadji(int idR)
    {
        var roba = await context.Roba.Where(p => p.ID == idR).FirstOrDefaultAsync();
        if(roba == null)
            return BadRequest("Nema robe");
        
        var kom = await context.Kompanija.Where(p => p.Cena > roba.CenaOd &&
         p.Cena < roba.CenaDo &&
        (((roba.datumIsporuke.Subtract(roba.datumPrijema)).Days) > p.BrojDanaZaIsporuku))
        .Select(p => 
        new
        {
            Naziv = p.Naziv,
            Cena = p.Cena,
            //Vozilo = p.Vozila,
            ProsecnaZarad = p.ProsecnaZarada
        }
        ).FirstOrDefaultAsync();
        if(kom == null)
            return BadRequest("Ne postoji kompanija koja moze da ispuni ove uslove");
        return Ok(kom);
    }

    [Route("Isporuci/{idK}")]
    [HttpPut]
    public async Task<ActionResult> Isporuci(int idK)
    {
        var kom = await context.Kompanija.Where(p => p.ID == idK).FirstOrDefaultAsync();
        if(kom == null)
            return BadRequest("Nema kompanije");
        
        try
        {
            kom.ProsecnaZarada += kom.Cena;
            await context.SaveChangesAsync();
            return Ok(kom);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
