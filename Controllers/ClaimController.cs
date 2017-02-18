using Microsoft.AspNetCore.Mvc;
using FisherInsuranceApi.Data;
using FisherInsuranceApi.Models;

[Route("api/claim")]
public class ClaimController : Controller {

//POST api/claims/

private IMemoryStore db;

public ClaimController(IMemoryStore repo)
{
    db = repo;
}

[HttpGet]

public IActionResult GetQuotes()
{

    return Ok(db.RetrieveAllQuotes);
}


[HttpPost]

public IActionResult Post([FromBody]Claim claim) {
   
    return Ok(db.CreateClaim(claim));
}

//GET api/claims/claim

[HttpGet("{id}")]
public IActionResult Get (int id)
{
    return Ok(db.RetrieveQuote(id));
}
//PUT api/claims/id

[HttpPutAttribute("{id}")]

public IActionResult Put(int id, [FromBody]Claim claim) 
{
    return Ok(db.UpdateClaim(claim));
}
public IActionResult Delete([FromBody]int id) 
{
    db.DeleteClaim(id);
    return Ok();
}
}