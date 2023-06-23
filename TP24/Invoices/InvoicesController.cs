using Microsoft.AspNetCore.Mvc;

namespace TP24.Invoices;

[ApiController]
[Route("[controller]")]

public class InvoicesController : ControllerBase
{
    private readonly InvoiceHandler handler;

    public InvoicesController(InvoiceHandler handler) {
        this.handler = handler;
    }

    [HttpGet("{debtorReference}")]
    public ActionResult<Invoices> Get([FromRoute] string debtorReference, [FromQuery] bool? isOpen) {
        var invoices = handler.GetByDebtorRef(debtorReference, isOpen);

        return invoices.InvoiceCollection.Any()
            ? Ok(invoices)
            : NotFound();
    }
}
