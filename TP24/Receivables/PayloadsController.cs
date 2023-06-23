using Microsoft.AspNetCore.Mvc;

namespace TP24.Receivables;

[ApiController]
[Route("[controller]")]

public class ReceivablesController : ControllerBase
{
    private readonly ReceivablesRepository repo;

    public ReceivablesController(ReceivablesRepository repo) {
        this.repo = repo;
    }

    [HttpGet(Name = "Customers")]
    public IEnumerable<Receivable> GetAll() {
        return repo.GetAll();
    }

    [HttpGet("{reference}")]
    public ActionResult<Receivable> Get([FromRoute] string reference) {
        var receivable = repo.GetByRef(reference);
        return receivable is not null
            ? Ok(receivable)
            : NotFound();
    }

    [HttpPost]
    public ActionResult Create(Receivable receivable) {
        var success = repo.Create(receivable);
        return success
            ? Created($"/receivables/{receivable.reference}", receivable)
            : BadRequest();
    }

    [HttpPut("{reference}")]
    public ActionResult Update([FromRoute] string reference, [FromBody] Receivable updatedReceivable) {
        var customer = repo.GetByRef(reference);
        if (customer is null) {
            return NotFound();
        }

        repo.Update(reference, updatedReceivable);
        return Ok(updatedReceivable);
    }

    [HttpDelete("{reference}")]
    public ActionResult<Receivable> Delete([FromRoute] string reference) {
        repo.Delete(reference);
        return Ok();
    }
}
