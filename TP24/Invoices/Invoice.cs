namespace TP24.Invoices;

public record Invoice(
    DateTime IssueDate,
    DateTime DueDate,
    double OpeningValue,
    double PaidValue);