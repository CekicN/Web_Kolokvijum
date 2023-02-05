using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Cors; 
namespace Oktobar_2022.Controllers;

[ApiController]
[Route("[controller]")]
public class FotografijaController : ControllerBase
{
    FotoContext Context;
    public FotografijaController(FotoContext f)
    {
        Context = f;
    }

    [Route("UzmiRamPoVelicini/{idD}")]
    [HttpGet]
    public async Task<ActionResult> UzmiRamV(int idD)
    {
        var ram = await Context.Ramovi
                        .Include(p => p.Dimenzija)
                        .Where(p => p.Dimenzija.ID == idD).ToListAsync();


        if(ram == null)
            return BadRequest("Nije pronadjen ram sa tom dimenzijom");
        
        return Ok(
            ram.Select( p =>
            new
            {
                id = p.ID,
                Materijal = p.Materijal
            }
            )
        );
    }

    //[EnableCors("CORS")]
    [Route("UzmiDimenziju")]
    [HttpGet]
    public async Task<ActionResult> UzmiDimenziju()
    {
        var dim = await Context.Dimenzije.ToListAsync();

        if(dim == null)
            return BadRequest("Nije pronadjen ram sa tom dimenzijom");
        
        return Ok(dim);
    }

     [Route("UzmiPapir")]
    [HttpGet]
    public async Task<ActionResult> Uzmipapir()
    {
        var dim = await Context.Papiri.ToListAsync();

        if(dim == null)
            return BadRequest("Nije pronadjen ram sa tom dimenzijom");
        
        return Ok(dim);
    }

    [Route("UzmiFotografiju")]
    [HttpGet]
    public async Task<ActionResult> UzmiFotografiju([FromQuery] int[] IDs)
    {
        if(IDs.Length == 0)
        {
            var foto = await Context.Fotografije.ToListAsync();
            return Ok(foto);
        }
        else if(IDs.Length == 1)
        {
            var foto = await Context.Fotografije.Where(p => p.Dimenzija.ID == IDs[0]).ToListAsync();
            if(foto == null)
                return BadRequest("Nije pronadjena fotografija sa tom dimenzijom");
            return Ok(foto);
        }
        else if(IDs.Length == 2)
        {
            var foto = await Context.Fotografije.Where(p => p.Dimenzija.ID == IDs[0] && p.Papir.ID == IDs[1]).ToListAsync();
            if(foto == null)
                return BadRequest("Nije pronadjena fotografija sa tom dimenzijom i papirom");
            return Ok(foto);
        }
        else
        {
            var foto = await Context.Fotografije.Where(p => p.Dimenzija.ID == IDs[0] && p.Papir.ID == IDs[1] && p.Ram.ID == IDs[2]).ToListAsync();
            if(foto == null)
                return BadRequest("Nije pronadjena fotografija sa tom dimenzijom i papirom");
            return Ok(foto);
        }
    }

    [HttpDelete]
    [Route("Kupi/{idF}/{idR}")]
    public async Task<ActionResult> Kupi(int idF, int idR)
    {
        var foto = await Context.Fotografije.Where(p => p.ID == idF && p.Ram.ID == idR).FirstOrDefaultAsync();
        
        if(foto == null)
            return BadRequest("Nije pronadjena fotografija");

        Context.Fotografije.Remove(foto);
        await Context.SaveChangesAsync();
        return Ok(foto);
    }
}
