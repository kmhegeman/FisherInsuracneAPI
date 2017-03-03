using Microsoft.AspNetCore.Mvc;
using FisherInsuranceApi.Data;
using FisherInsuranceApi.Models;

[Route("api/quotes")]
public class QuoteController : Controller {

//POST api/auto/quotes

private readonly FisherContext db;


public QuoteController(FisherContext context)
{
 db = context;
}

[HttpGet]

public IActionResult GetQuotes()
{

    return Ok(db.Quote);
}


[HttpPost]
 public IActionResult Post([FromBody] Quote quote)
 {
 var newQuote = db.Quote.Add(quote);
 db.SaveChanges();
 return CreatedAtRoute("GetQuote", new { id = quote.Id }, quote);
 }


//GET api/quotes/quote

 [HttpGet("{id}", Name = "GetQuote")]
public IActionResult Get(int id)
 {
 return Ok(db.Quote.Find(id));
 }
//PUT api/quotes/id

[HttpPut("{id}")]
 public IActionResult Put(int id, [FromBody] Quote quote)
 {
 var newQuote = db.Quote.Find(id);
 if (newQuote == null)
 {
 return NotFound();
 }
 newQuote = quote;
 db.SaveChanges();
 return Ok(newQuote);
 }
[HttpDelete("{id}")]

 public IActionResult Delete(int id)
 {
 var quoteToDelete = db.Quote.Find(id);
 if (quoteToDelete == null)
 {
 return NotFound();
 }
 db.Quote.Remove(quoteToDelete);
 db.SaveChangesAsync();
 return NoContent();

}
}