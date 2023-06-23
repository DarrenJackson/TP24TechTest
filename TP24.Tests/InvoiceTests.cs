using System.Text.Json;
using FluentAssertions;
using TP24.Invoices;
using TP24.Receivables;

namespace TP24.Tests;

public class InvoiceTests 
{
    [Fact]
    public void GetAllInvoicesByDebtorRefShouldReturnAllInvoices() {
        var repo = new ReceivablesRepository();
        LoadTestData(repo);
        var handler = new InvoiceHandler(repo);

        var invoices = handler.GetByDebtorRef("JANE", null);

        invoices.InvoiceCollection.Count.Should().Be(2);
    }
    
    [Fact]
    public void GetAllInvoicesByDebtorRefShouldReturnTotalDue() {
        var repo = new ReceivablesRepository();
        LoadTestData(repo);
        var handler = new InvoiceHandler(repo);

        var invoices = handler.GetByDebtorRef("JANE", null);

        invoices.TotalDue.Should().Be(500.50);
    }
    
    [Fact]
    public void GetAllInvoicesByDebtorRefShouldReturnTotalPaid() {
        var repo = new ReceivablesRepository();
        LoadTestData(repo);
        var handler = new InvoiceHandler(repo);

        var invoices = handler.GetByDebtorRef("JANE", null);

        invoices.TotalPaid.Should().Be(400.50);
    }
    
    [Fact]
    public void GetOpenInvoicesByDebtorRefShouldReturnOpenInvoices() {
        var repo = new ReceivablesRepository();
        LoadTestData(repo);
        var handler = new InvoiceHandler(repo);

        var invoices = handler.GetByDebtorRef("JANE", isOpen: true);

        invoices.InvoiceCollection.Count.Should().Be(1);
    }
    
    [Fact]
    public void GetClosedInvoicesByDebtorRefShouldReturnClosedInvoices() {
        var repo = new ReceivablesRepository();
        LoadTestData(repo);
        var handler = new InvoiceHandler(repo);

        var invoices = handler.GetByDebtorRef("JANE", isOpen: false);

        invoices.InvoiceCollection.Count.Should().Be(1);
    }

    private void LoadTestData(ReceivablesRepository repo) {
        var f = File.OpenRead("TestData.json");
        var data = JsonSerializer.Deserialize<List<Receivable>>(f);

        if (data is not null) {
            foreach (var p in data) {
                repo.Create(p);
            }
        }
    }
}