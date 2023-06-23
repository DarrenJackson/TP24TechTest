namespace TP24.Invoices;

public record Invoices(
    string DebtorReference,
    double TotalDue,
    double TotalPaid,
    List<Invoice> InvoiceCollection);