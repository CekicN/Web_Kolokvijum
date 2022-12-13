using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _2021_jun2.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public FakultetContext Context { get; set; }
    public IspitController(FakultetContext context)
    {
        Context = context;
    }

    [Route("IspitniRokovi")]
    [HttpGet]
    public async Task<ActionResult> Rokovi()
    {
        return Ok(await Context.Rokovi.Select(p =>
        new
        {
            ID = p.ID,
            Naziv = p.Naziv
        }).ToListAsync());
    }

    [Route("DodajRok")]
    [HttpPost]
    public async Task<ActionResult> DodajRok([FromBody] IspitniRok rok)
    {
        if(rok.Naziv == null)
            return BadRequest("Prazan je rok");

        try
        {
            Context.Rokovi.Add(rok);
            await Context.SaveChangesAsync();
            return Ok($"Rok sa ID-jem {rok.ID} je dodat");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("DodajPolozeniIspit/{indeks}/{idPredmeta}/{idRoka}/{ocena}")]
    [HttpPost]
    public async Task<ActionResult> DodajIspit(int indeks, int idPredmeta, int idRoka, int ocena)
    {
        if(indeks < 10000 || indeks > 20000)
            return BadRequest("Pogresan Broj indeksa");
        //...

        try
        {
            var student = await Context.Studenti.Where(p => p.Indeks == indeks).FirstOrDefaultAsync();
            var predmet = await Context.Predmeti.Where(p => p.ID == idPredmeta).FirstOrDefaultAsync();
            var ispitniRok = await Context.Rokovi.FindAsync(idRoka);

            Spoj s = new Spoj{
                Student = student,
                Predmet = predmet,
                IspitniRok = ispitniRok,
                Ocena = ocena
            };

            Context.StudentiPredmeti.Add(s);
            await Context.SaveChangesAsync();

            var podaciOStudentu = await Context.StudentiPredmeti
                    .Include(p => p.Student)
                    .Include(p => p.Predmet)
                    .Include(p => p.IspitniRok)
                    .Where(p => p.Student.Indeks == indeks)
                    .Select(p => 
                    new
                    {
                        Indeks = p.Student.Indeks,
                        Ime = p.Student.Ime,
                        Prezime = p.Student.Prezime,
                        Predmet = p.Predmet.Naziv,
                        Ocena = p.Ocena
                    }).ToListAsync();
            return Ok(podaciOStudentu);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
