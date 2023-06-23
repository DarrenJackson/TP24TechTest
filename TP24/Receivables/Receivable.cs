// ReSharper disable InconsistentNaming
namespace TP24.Receivables;

public record Receivable (
    string reference, 
    string currencyCode,
    DateTime issueDate,
    double openingValue,
    double paidValue,
    DateTime dueDate,
    DateTime? closedDate,
    bool? cancelled,
    string debtorName,
    string debtorReference,
    string debtorAddress1,
    string debtorAddress2,
    string debtorTown,
    string debtorState,
    string debtorZip,
    string debtorCountryCode,
    string debtorRegistrationNumber);