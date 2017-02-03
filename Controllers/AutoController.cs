using Microsoft.AspNetCore.Mvc;

[Route("api/auto/quotes")]
public class AutoController : Controller {

//POST api/auto/quotes

[HttpPost]

public IActionResult Post([FromBody]string value) {
    return Created("",value);
}

//GET api/auto/quotes/5

[HttpGet("{id}")]
public IActionResult Get (int id)
{
    return Ok("The id is: " + id);
}
//PUT api/auto/quotes/id

[HttpPutAttribute("{id}")]

public IActionResult Put(int id, [FromBody]string value) 
{
    return NoContent();
}
//DELETE api/auto/quotes/id

[HttpDelete("{id}")]

public IActionResult Delete(int id) 
{
    return Delete(id);
}
}