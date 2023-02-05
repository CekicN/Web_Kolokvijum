

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Septembar_2022
{   
    [ApiController]
    [Route("Controller")]    
    public class BiljkeController:ControllerBase
    {
        public Context context;

        public BiljkeController(Context c)
        {
            context = c;
        }

        [HttpPost]
        [Route("PreuzmiBiljku/{idP}/{idC}/{idL}/{idS}")]
        public async Task<ActionResult> PreuzmiBiljku(int idP, int idC, int idL, int idS)
        {


            var podrucje = await context.Podrucja.Where(p => p.ID == idP).FirstOrDefaultAsync();
            if(podrucje == null)
                return BadRequest("Ne postoji podrucje");


            var list = await context.Listovi.Where(p => p.ID == idL).FirstOrDefaultAsync();
            if(list == null)
                return BadRequest("Ne postoji list");

            var stablo = await context.Stabla.Where(p => p.ID == idS).FirstOrDefaultAsync();
            if(stablo == null)
                return BadRequest("Ne postoji stablo");
            var cvet = await context.Cvece.Where(p => p.ID == idC).FirstOrDefaultAsync();
            if(cvet == null)
                return BadRequest("Ne postoji cvet");

            var biljka = await context.Biljke.Where(p => p.Podrucje == podrucje && p.Cvet == cvet && p.List == list && p.Stablo == stablo).ToListAsync();
            
            try
            {
                if(biljka == null)
                {
                    var novabiljka = new NoveBiljke();

                    novabiljka.Podrucje = podrucje;
                    novabiljka.Cvet = cvet;
                    novabiljka.Stablo = stablo;
                    novabiljka.List = list;
                    
                    context.NoveBiljke.Add(novabiljka);
                    await context.SaveChangesAsync();

                    return Ok(novabiljka);
                }
                return Ok(biljka);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [Route("VratiPodrucje")]
        [HttpGet]
        public async Task<ActionResult> VratiPodrucje()
        {   
            
            var podrucje = await context.Podrucja.ToListAsync();

            if(podrucje == null)
                return BadRequest("Ne postoji podrucje");
            try
            {
                return Ok(podrucje);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiCvet")]
        [HttpGet]
        public async Task<ActionResult> VratiCvet()
        {   
            
            var podrucje = await context.Cvece.ToListAsync();

            if(podrucje == null)
                return BadRequest("Ne postoji podrucje");
            try
            {
                return Ok(podrucje);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiList")]
        [HttpGet]
        public async Task<ActionResult> VratiList()
        {   
            
            var podrucje = await context.Listovi.ToListAsync();

            if(podrucje == null)
                return BadRequest("Ne postoji podrucje");
            try
            {
                return Ok(podrucje);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiStablo")]
        [HttpGet]
        public async Task<ActionResult> VratiStablo()
        {   
            
            var podrucje = await context.Stabla.ToListAsync();

            if(podrucje == null)
                return BadRequest("Ne postoji podrucje");
            try
            {
                return Ok(podrucje);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromeniKolicinu/{id}")]
        [HttpPut]
        public async Task<ActionResult> PromeniKolicinu(int id)
        {
            try
            {
                var biljka = await context.Biljke.Where(p => p.ID == id).FirstOrDefaultAsync();
                if(biljka == null)
                    return BadRequest("Nije pronadjena biljka");
                biljka.kolicina += 1;

                context.Biljke.Update(biljka);
                await context.SaveChangesAsync();

                return Ok(biljka);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        } 
    }
}