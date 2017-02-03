using Microsoft.AspNetCore.Mvc;

[Route("api/home/quotes")]
public class HomeController : Controller {

//POST api/home/quotes

[HttpPost]

public IActionResult Post([FromBody]string value) {
    return Created("",value);
}

//GET api/home/quotes/5

[HttpGet("{id}")]
public IActionResult Get (int id)
{
    return Ok("The id is: " + id);
}
//PUT api/home/quotes/id

[HttpPutAttribute("{id}")]

public IActionResult Put(int id, [FromBody]string value) 
{
    return NoContent();
}

}