using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace _2021_jun2.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    public FakultetContext Context { get; set; }
    public StudentController(FakultetContext context)
    {
        Context = context;
    }


    
    [Route("Studenti")]
    [HttpGet]
    public async Task<ActionResult> Preuzmi([FromQuery] int[] rokIDs)
    {
        //return Ok(podatak);
        //Lazy loading -> kad se povucu studenti imace informaciju samo o studentu dok,
        //ne  zatrebaju i podaci iz StudentPredmet(Spoj) tad ce da se povucu i ti podaci

        // var student = Context.Studenti.FirstOrDefault();
        // student.StudentPredmet.Where(p => p.Ocena > 8);//tek ovde ce da se povucu podaci iz baze za StudentPredmet
        /******LOSE ZBOG SERIJALIZACIJE****/
        
        
        //Explicit Loading --> -> ako se povuku studenti iz baze i kad se uradi ToList povlaci sve iz baze sto je lose

        // var studenti = Context.Studenti;

        // var student = studenti.Where(p => p.Indeks == 18477).FirstOrDefault();
        // await Context.Entry(student).Collection(p => p.StudentPredmet).LoadAsync();

        // foreach(var s in student.StudentPredmet)
        // {
        //     await Context.Entry(s).Reference(p => p.Predmet).LoadAsync();
        //     await Context.Entry(s).Reference(p => p.IspitniRok).LoadAsync();
        // }
        
        // var studenti = Context.Studenti
        //     .Include(p => p.StudentPredmet)//Ukljucuje vezu student-predmet u studenti ce se naci studenti sa dodatom studentpredmet
        //     .ThenInclude(p => p.Predmet)//ThenInclude radi sa onim sto je prethodno ukljuceno a to je studentpredmet
        //     .Include(p => p.StudentPredmet)
        //     .ThenInclude(p => p.IspitniRok);//bez ToList jer ce da vrati celu bazu
        //PRAVI SE PETLJA jer se poziva studentpredmet u koji se opet poziva student i tako u krug zato se dodaje [JsonIgnore] atribut u Spoj kod studenta

        //Eager loading --> Najbolji metod
        var studenti = Context.Studenti
                        .Include(p => p.StudentPredmet)
                        .ThenInclude(p => p.IspitniRok)
                        .Include(p => p.StudentPredmet)
                        .ThenInclude(p => p.Predmet);

        var student =  await studenti.ToListAsync();

        return Ok
        (
            student.Select(p =>
            new
            {
                ID = p.ID,
                Index = p.Indeks,
                Ime = p.Ime,
                Prezime = p.Prezime,
                Predmeti = p.StudentPredmet
                    .Where(q => rokIDs.Contains(q.IspitniRok.ID))
                    .Select(q =>
                    new
                    {
                        Predmet = q.Predmet.Naziv,
                        IspitniRok = q.IspitniRok.Naziv,
                        Ocena = q.Ocena
                    })
            }).ToList()
        );

    }

    [Route("DodatuStudenta")]
    [HttpPost]
    public async Task<ActionResult> DodajStudenta([FromBody] Student student)
    {
        if(student.Indeks < 10000 || student.Indeks > 20000)
        {
            return BadRequest("Pogresan Indeks");
        }

        if(string.IsNullOrEmpty(student.Ime) || student.Ime.Length > 50)
            return BadRequest("Pogresno ime");
        
        if(string.IsNullOrEmpty(student.Prezime) || student.Prezime.Length > 50)
            return BadRequest("Pogresno prezime");
        try
        {
            Context.Studenti.Add(student);
            await Context.SaveChangesAsync();//AWAIT -> sluzi da se ova funkcija odvija u pozadinskoj niti tj u pozadini,
                                            //neblokirajuca je tj moze se izvrsavati sa izvrsenjem dok ona radi
            return Ok($"Student je dodat! ID je: {student.ID}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PromenitiStudenta/{indeks}/{ime}/{prezime}")]
    [HttpPut]
    public async Task<ActionResult> Promeni(int indeks, string ime, string prezime)
    {   
         if(indeks < 10000 || indeks > 20000)
        {
            return BadRequest("Pogresan Indeks");
        }

        try
        {
            var student = Context.Studenti.Where(p => p.Indeks == indeks).FirstOrDefault();
            if(student != null)
            {
                student.Ime = ime;
                student.Prezime = prezime;

                await Context.SaveChangesAsync();
                return Ok($"Uspesno promenjen student! ID: {student.ID}");
            }
            else
                return BadRequest("Student nije pronadjen");
            
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("PromeniFromBody")]
    [HttpPut]
    public async Task<ActionResult> PromeniBody([FromBody] Student student)
    {
        if(student.ID <= 0)
        {
            return BadRequest("Pogresan ID");
        }

        try
        {
            // var studentZapromenu = await Context.Studenti.FindAsync(student.ID);

            // studentZapromenu.Indeks = student.Indeks;
            // studentZapromenu.Ime = student.Ime;
            // studentZapromenu.Prezime = student.Prezime;

            Context.Studenti.Update(student);

            await Context.SaveChangesAsync();
            return Ok($"Student sa ID-jem {student.ID} je izmenjen");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Route("IzbrisiStudenta/{id}")]
    [HttpDelete]
    public async Task<ActionResult> Izbrisi(int id)
    {
        if(id <= 0)
            return BadRequest("Pogresan ID");
        try
        {
            var student = await Context.Studenti.FindAsync(id);
            int indeks = student.Indeks;
            Context.Studenti.Remove(student);
            await Context.SaveChangesAsync();//Nakon ove metode se sve brise sem parametra funkcije id
            return Ok($"Uspesno obrisan student! Indeks: {indeks}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
