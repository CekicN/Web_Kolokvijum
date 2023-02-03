using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Proba.Controllers
{
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
            try
            {
                return Ok(await Context.Rokovi.Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv
                }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajPolozeniIspit/{indeks}/{idPredmeta}/{idRoka}/{ocena}")]
        [HttpPost]
        public async Task<ActionResult> DodajIspit(int indeks, int idPredmeta, int idRoka, int ocena)
        {
           // if (indeks < 10000 || indeks > 20000)
           // {
           //     return BadRequest("Pogrešan broj indeksa");
          //  }
            //...

            try
            {
                var student = Context.Studenti.Where(p => p.Indeks == indeks).FirstOrDefault();

                if(student==null)
                {
                    return StatusCode(202,"Ne postoji student sa unetim brojem indeksa");
                    //return BadRequest("Ne postoji student sa unetim brojem indeksa");
                }
                var predmet = await Context.Predmeti.Where(p => p.ID == idPredmeta).FirstOrDefaultAsync();
                var ispitniRok = await Context.Rokovi.FindAsync(idRoka);

                var polozioRok = Context.StudentiPredmeti
                .Include(x=>x.Student)
                .Include(x=>x.Predmet)
                .Where(x=>x.Student.ID==student.ID
                && x.Predmet.ID==predmet.ID)
                .FirstOrDefault();

                if(polozioRok !=null)
                {
                    return StatusCode(203,"Student je vec polozio taj ispit");
                }
                Spoj s = new Spoj
                {
                    Student = student,
                    Predmet = predmet,
                    IspitniRok = ispitniRok,
                    Ocena = ocena
                };

                Context.StudentiPredmeti.Add(s);
                await Context.SaveChangesAsync();

                var podaciOStudnetu = await Context.StudentiPredmeti
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
                            IspitniRok = p.IspitniRok.Naziv,
                            Ocena = p.Ocena
                        }).ToListAsync();
                return Ok(podaciOStudnetu);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajRok/{naziv}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DodajRok(string naziv)
        {
            if (string.IsNullOrWhiteSpace(naziv))
            {
                return BadRequest("Pogrešan naziv roka!");
            }

            try
            {
                IspitniRok rok = new IspitniRok
                {
                    Naziv = naziv
                };

                Context.Rokovi.Add(rok);
                await Context.SaveChangesAsync();
                return Ok("Uspešno upisan rok!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
