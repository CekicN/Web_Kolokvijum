using Microsoft.AspNetCore.Mvc;
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

    [Route("Preuzmi")]
    [HttpGet]
    public ActionResult Preuzmi()
    {
        return Ok(Context.Studenti);
    }
}
