using Microsoft.AspNetCore.Mvc;
using FisherInsuranceApi.Data;
using FisherInsuranceApi.Models;

[Route("api/claim")]
public class ClaimController : Controller {

//POST api/claims/

private readonly FisherContext db;


public ClaimController(FisherContext context)
{
 db = context;
}

[HttpGet]

public IActionResult GetClaims()
{

    return Ok(db.Claim);
}


[HttpPost]
 public IActionResult Post([FromBody] Claim claim)
 {
 var newClaim = db.Claim.Add(claim);
 db.SaveChanges();
 return CreatedAtRoute("GetClaim", new { id = claim.Id }, claim);
 }


//GET api/claims/claim

 [HttpGet("{id}", Name = "GetClaim")]
public IActionResult Get(int id)
 {
 return Ok(db.Claim.Find(id));
 }
//PUT api/claims/id

[HttpPut("{id}")]
 public IActionResult Put(int id, [FromBody] Claim claim)
 {
 var newClaim = db.Claim.Find(id);
 if (newClaim == null)
 {
 return NotFound();
 }
 newClaim = claim;
 db.SaveChanges();
 return Ok(newClaim);
 }
[HttpDelete("{id}")]

 public IActionResult Delete(int id)
 {
 var claimToDelete = db.Claim.Find(id);
 if (claimToDelete == null)
 {
 return NotFound();
 }
 db.Claim.Remove(claimToDelete);
 db.SaveChangesAsync();
 return NoContent();

}
}