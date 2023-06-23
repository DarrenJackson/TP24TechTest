using TP24.Receivables;

namespace TP24.Invoices;

public class InvoiceHandler
{
    private readonly ReceivablesRepository repo;

    public InvoiceHandler(ReceivablesRepository repo) {
        this.repo = repo;
    }

    public Invoices GetByDebtorRef(string debtorReference, bool? isOpen) {
        var payloads = repo.GetByDebtorRef(debtorReference).ToList();

        if (!isOpen.HasValue) {
            return GetAllInvoices(debtorReference, payloads);
        }

        if (isOpen.Value) {
            return GetOpenInvoices(debtorReference, payloads);
        }

        return GetClosedInvoices(debtorReference, payloads);
    }

    private Invoices GetAllInvoices(string debtorReference, List<Receivable> payloads) {
        return new Invoices(
            debtorReference,
            payloads.Sum(x => x.openingValue),
            payloads.Sum(x => x.paidValue),
            payloads.Select(y => new Invoice(y.issueDate, y.dueDate, y.openingValue, y.paidValue)).ToList());
    }

    private Invoices GetOpenInvoices(string debtorReference, List<Receivable> payloads) {
        var openInvoices = payloads.Where(x => x.closedDate is null).ToList();

        return new Invoices(
            debtorReference,
            openInvoices.Sum(x => x.openingValue),
            openInvoices.Sum(x => x.paidValue),
            openInvoices.Select(y => new Invoice(y.issueDate, y.dueDate, y.openingValue, y.paidValue)).ToList());
    }

    private Invoices GetClosedInvoices(string debtorReference, List<Receivable> payloads) {
        var closedInvoices = payloads.Where(x => x.closedDate is not null).ToList();
        return new Invoices(
            debtorReference,
            closedInvoices.Sum(x => x.openingValue),
            closedInvoices.Sum(x => x.paidValue),
            closedInvoices
                .Select(y => new Invoice(y.issueDate, y.dueDate, y.openingValue, y.paidValue))
                .ToList());
    }
}