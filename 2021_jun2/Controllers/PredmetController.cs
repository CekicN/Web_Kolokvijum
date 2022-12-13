using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _2021_jun2.Controllers;

[ApiController]
[Route("[controller]")]
public class PredmetController : ControllerBase
{
    public FakultetContext Context { get; set; }
    public PredmetController(FakultetContext context)
    {
        Context = context;
    }

    [Route("UnesiPredmet")]
    [HttpPost]
    public async Task<ActionResult> DodajPredmet([FromBody] Predmet predmet)
    {
        if(predmet.Naziv == null)
            return BadRequest("Nema Predmeta");

        try
        {
            Context.Predmeti.Add(predmet);
            await Context.SaveChangesAsync();
            return Ok("Predmet je dodat");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Route("PreuzmiPredmet")]
    [HttpGet]
    public async Task<ActionResult> PreuzmiPredmet()
    {
        return Ok(
            await Context.Predmeti.Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv
                }
            ).ToListAsync()
        );
    }
}
