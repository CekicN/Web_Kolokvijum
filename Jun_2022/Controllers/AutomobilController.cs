using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.AspNetCore.Cors;
namespace Jun_2022.Controllers;

[ApiController]
[Route("[controller]")]
public class AutomobilController : ControllerBase
{
   AutomobilContext Context;
   public AutomobilController(AutomobilContext c)
   {
        Context = c;
   }

   [Route("DodajMarku")]
   [HttpPost]
   public async Task<ActionResult> DodajMarku([FromBody] Marka marka)
   {
        try
        {
            Context.Marke.Add(marka);
            await Context.SaveChangesAsync();
            return Ok(marka);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
   }

   [Route("DodajModel/{idMarke}/{model}")]
   [HttpPost]
   public async Task<ActionResult> DodajModel(int idMarke,string model)
   {
        var marka = Context.Marke.Where(p => p.ID == idMarke).FirstOrDefault();
        if(marka == null)
            return BadRequest("Nije pronadjena marka");
        try
        {
            Model m = new Model
            {
                Naziv = model,
                Marka = marka
            };

            await Context.Modeli.AddAsync(m);
            await Context.SaveChangesAsync();

            return Ok(m);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
   }

    [Route("DodajAutomobil")]
     [HttpPost]
    public async Task<ActionResult> DodajAutomobil([FromBody] Automobili auto)
    {
            try
            {

                await Context.Automobili.AddAsync(auto);
                await Context.SaveChangesAsync();

                return Ok(auto);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    }
    
    [Route("DodajBoju/{idModela}/{Boja}")]
    [HttpPost]
    public async Task<ActionResult> DodajBoju(int idModela,string Boja)
    {
            var model = Context.Modeli.Where(p => p.ID == idModela).FirstOrDefault();
            if(model == null)
                return BadRequest("Nije pronadjena marka");
            try
            {
                Boja b = new Boja
                {
                    Naziv = Boja,
                    Model = model
                };

                await Context.Boje.AddAsync(b);
                await Context.SaveChangesAsync();

                return Ok(b);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
    }
   // [EnableCors("CORS")]
    [Route("PreuzmiMarku")]
    [HttpGet]
    public async Task<ActionResult> PreuzmiMarku()
    {
        var marke = await Context.Marke.ToListAsync();

        return Ok(marke);
    }

        [Route("PreuzmiModel/{idMarke}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiModel(int idMarke)
        {
            var modeli = await Context.Modeli
                                .Include(p => p.Marka)
                                .Where(p => p.Marka.ID == idMarke)
                                .ToListAsync();
            return Ok(modeli);
        }

        [Route("PreuzmiBoju/{idModela}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiBoju(int idModela)
        {
            var boje = await Context.Boje
                                .Include(p => p.Model)
                                .Where(p => p.Model.ID == idModela)
                                .ToListAsync();
            return Ok(boje);
        }


        [Route("PronadjiAutomobil")]
        [HttpGet]
        public async Task<ActionResult> PronadjiAutomobil([FromQuery] int[] IDs)
        {
            if(IDs.Length == 1)
            {   
                var marka = Context.Marke.Where(p => p.ID == IDs[0]).FirstOrDefault();
                if(marka == null)
                    return BadRequest("Marka nije pronadjena");
                var automobil = await Context.Automobili.Where(p => p.Marka == marka.Naziv)
                                                .Select(p => 
                                                new
                                                {
                                                    Marka = p.Marka,
                                                    Model = p.Model,
                                                    Slika = p.URLSlike,
                                                    Kolicina = p.Kolicina,
                                                    Datum = p.DatumProdaje,
                                                    Cena = p.Cena
                                                })
                                                .ToListAsync();
                if(automobil == null)
                    return BadRequest("Ne postoji automobil sa tom markom");
                return Ok(automobil);
        }
        else if(IDs.Length == 2)
        {
            var marka = Context.Marke.Where(p => p.ID == IDs[0]).FirstOrDefault();
                if(marka == null)
                    return BadRequest("Marka nije pronadjena");
                var model = Context.Modeli.Where(p => p.ID == IDs[1]).FirstOrDefault();
                if(model == null)
                    return BadRequest("Model nije pronadjen");
                var automobil = await Context.Automobili.Where(p => p.Marka == marka.Naziv && p.Model == model.Naziv)
                                                .Select(p => 
                                                new
                                                {
                                                    Marka = p.Marka,
                                                    Model = p.Model,
                                                    Slika = p.URLSlike,
                                                    Kolicina = p.Kolicina,
                                                    Datum = p.DatumProdaje,
                                                    Cena = p.Cena
                                                })
                                                .ToListAsync();
                if(automobil == null)
                    return BadRequest("Ne postoji automobil sa tom markom");
                return Ok(automobil);
        }
        else if(IDs.Length == 3)
        {
                var marka = Context.Marke.Where(p => p.ID == IDs[0]).FirstOrDefault();
                if(marka == null)
                    return BadRequest("Marka nije pronadjena");
                var model = Context.Modeli.Where(p => p.ID == IDs[1]).FirstOrDefault();
                if(model == null)
                    return BadRequest("Model nije pronadjen");
                var boja = Context.Boje.Where(p => p.ID == IDs[2]).FirstOrDefault();
                if(boja == null)
                    return BadRequest("Boja nije pronadjena");
                var automobil = await Context.Automobili.Where(p => p.Marka == marka.Naziv && p.Model == model.Naziv && p.Boja == boja.Naziv)
                                                .Select(p => 
                                                new
                                                {
                                                    Marka = p.Marka,
                                                    Model = p.Model,
                                                    Slika = p.URLSlike,
                                                    Kolicina = p.Kolicina,
                                                    Datum = p.DatumProdaje,
                                                    Cena = p.Cena
                                                })
                                                .ToListAsync();
                if(automobil == null)
                    return BadRequest("Ne postoji automobil sa tom markom");
                return Ok(automobil);
        }
        else
            return BadRequest("Barem Marka mora biti popunjena!");
    }

    [Route("NaruciAutomobil/{marka}/{model}")]
    [HttpPut]
    public async Task<ActionResult> NaruciAutomobil(string marka, string model)
    {
        var ma = Context.Marke.Where(p => p.Naziv == marka).FirstOrDefault();
        if(ma == null)
        {
            return BadRequest("Ne postoji takva marka");
        }

        var mo = Context.Modeli.Where(p => p.Naziv == model).FirstOrDefault();
        if(mo == null)
        {
            return BadRequest("Ne postoji takav model");
        }

        try
        {
            var auto = await Context.Automobili.Where(p => p.Marka == marka && p.Model == model).FirstOrDefaultAsync();
            auto.Kolicina--;

            if(auto.Kolicina < 1)
            {
                Context.Automobili.Remove(auto);
            }
            else
            {
                Context.Automobili.Update(auto);
            }
            await Context.SaveChangesAsync();
            
            return Ok(auto);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}
