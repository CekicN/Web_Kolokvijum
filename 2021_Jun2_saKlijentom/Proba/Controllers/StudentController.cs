using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Proba.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        public FakultetContext Context { get; set; }

        public StudentController(FakultetContext context)
        {
            Context = context;
        }

        // Cors policy može da se uključi i za pojedinačne metode, ovako
        [EnableCors("CORS")]
        // Ruta može da se razlikuje od naziva metode
        [Route("Studenti")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Preuzmi([FromQuery] int[] rokIDs)
        {
            try
            {
                // Eager loading
                // Include(p => p.StudentPredmet.Where...) omogućava uključivanje podatka samo za određene studente
                var studenti = Context.Studenti
                    .Include(p => p.StudentPredmet/*.Where(p => p.Student.Indeks == 12345)*/)
                    .ThenInclude(p => p.IspitniRok)
                    .Include(p => p.StudentPredmet)
                    .ThenInclude(p => p.Predmet);

                // Explicit loading!
                /*var student = studenti.Where(p => p.Indeks == 12345).FirstOrDefault();
                await Context.Entry(student).Collection(p => p.StudentPredmet).LoadAsync();

                foreach (var s in student.StudentPredmet)
                {
                    await Context.Entry(s).Reference(q => q.IspitniRok).LoadAsync();
                    await Context.Entry(s).Reference(q => q.Predmet).LoadAsync();
                }*/

                // Lazy loading se uključuje sam, kada se property koristi

                var student = await studenti.ToListAsync();

                return Ok
                (
                    student.Select(p =>
                    new
                    {
                        Indeks = p.Indeks,
                        Ime = p.Ime,
                        Prezime = p.Prezime,
                        Predmeti = p.StudentPredmet
                            .Where(q => rokIDs.Contains(q.IspitniRok.ID))
                            .Select(q =>
                            new
                            {
                                Predmet = q.Predmet.Naziv,
                                GodinaPredmeta = q.Predmet.Godina,
                                IspitniRok = q.IspitniRok.Naziv,
                                Ocena = q.Ocena
                            })
                    }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [EnableCors("CORS")]
        [Route("StudentiPretraga/{rokovi}/{predmetID}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> StudentiPretraga(string rokovi, int predmetID)
        {
            try
            {
                var rokIds = rokovi.Split('a')
                .Where(x=> int.TryParse(x, out _))
                .Select(int.Parse)
                .ToList();
               
                var studentipopredmetu = Context.StudentiPredmeti
                    .Include(p => p.Student)
                    .Include(p => p.IspitniRok)
                    .Include(p => p.Predmet)
                    .Where(p=>p.Predmet.ID==predmetID
                    && rokIds.Contains(p.IspitniRok.ID));

                
                var student = await studentipopredmetu.ToListAsync();

                return Ok
                (
                    student.Select(p =>
                    new
                    {
                        Index=p.Student.Indeks,
                        Ime = p.Student.Ime,
                        Prezime = p.Student.Prezime,
                        Predmet = p.Predmet.Naziv,
                        Rok = p.IspitniRok.Naziv,
                        Ocena = p.Ocena
                    }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [EnableCors("CORS")]
        [Route("StudentiPretragaFromBody/{predmetID}")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> StudentiPretragaFromBody([FromRoute]int predmetID, [FromBody]int[] rokIds)
        {
            try
            {
                var studentipopredmetu = Context.StudentiPredmeti
                    .Include(p => p.Student)
                    .Include(p => p.IspitniRok)
                    .Include(p => p.Predmet)
                    .Where(p=>p.Predmet.ID==predmetID
                    && rokIds.Contains(p.IspitniRok.ID));

                
                var student = await studentipopredmetu.ToListAsync();

                return Ok
                (
                    student.Select(p =>
                    new
                    {
                        Index=p.Student.Indeks,
                        Ime = p.Student.Ime,
                        Prezime = p.Student.Prezime,
                        Predmet = p.Predmet.Naziv,
                        Rok = p.IspitniRok.Naziv,
                        Ocena = p.Ocena
                    }).ToList()
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [EnableCors("CORS")]
        [Route("DodatiStudenta")]
        [HttpPost]
        public async Task<ActionResult> DodajStudenta([FromBody] Student student)
        {
            if (student.Indeks < 10000 || student.Indeks > 20000)
            {
                return BadRequest("Pogrešan Indeks!");
            }

            if (string.IsNullOrWhiteSpace(student.Ime) || student.Ime.Length > 50)
            {
                return BadRequest("Pogrešno ime!");
            }

            if (string.IsNullOrWhiteSpace(student.Prezime) || student.Prezime.Length > 50)
            {
                return BadRequest("Pogrešno prezime!");
            }

            try
            {
                Context.Studenti.Add(student);
                await Context.SaveChangesAsync();
                return Ok($"Student je dodat! ID je: {student.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenitiStudenta/{indeks}/{ime}/{prezime}")]
        [HttpPut]
        public async Task<ActionResult> Promeni(int indeks, string ime, string prezime)
        {
            if (indeks < 10000 || indeks > 20000)
            {
                return BadRequest("Pogrešan indeks!");
            }

            try
            {
                var student = Context.Studenti.Where(p => p.Indeks == indeks).FirstOrDefault();

                if (student != null)
                {
                    student.Ime = ime;
                    student.Prezime = prezime;

                    await Context.SaveChangesAsync();
                    return Ok($"Uspešno promenjen student! ID: {student.ID}");
                }
                else
                {
                    return BadRequest("Student nije pronađen!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenaFromBody")]
        [HttpPut]
        public async Task<ActionResult> PromeniBody([FromBody] Student student)
        {
            if (student.ID <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            // ... Ostale provere (Indeks, Ime, Prezime)

            try
            {
                //var studentZaPromenu = await Context.Studenti.FindAsync(student.ID);
                //studentZaPromenu.Indeks = student.Indeks;
                //studentZaPromenu.Ime = student.Ime;
                //studentZaPromenu.Prezime = student.Prezime;

                Context.Studenti.Update(student);

                await Context.SaveChangesAsync();
                return Ok($"Student sa ID: {student.ID} je uspešno izmenjen!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisatiStudenta/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            try
            {
                var student = await Context.Studenti.FindAsync(id);
                int indeks = student.Indeks;
                Context.Studenti.Remove(student);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan student sa Indeksom: {indeks}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
