using Microsoft.AspNetCore.Mvc;

[Route("api/claims")]
public class ClaimsController : Controller {

//POST api/claims/

[HttpPost]

public IActionResult Post([FromBody]string value) {
    return Created("",value);
}

//GET api/claims/claim

[HttpGet("{id}")]
public IActionResult Get (string id)
{
    return Ok("The id is: " + id);
}
//PUT api/claims/id

[HttpPutAttribute("{id}")]

public IActionResult Put(string id, [FromBody]string value) 
{
    return NoContent();
}

}