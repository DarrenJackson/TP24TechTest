using System.Text.Json;

namespace TP24.Receivables;

public class ReceivablesRepository
{
    private readonly Dictionary<string, Receivable> receivables = new ();

    public ReceivablesRepository() {

        // var f = File.OpenRead("TestData.json");
        // var data = JsonSerializer.Deserialize<List<Receivable>>(f);
        //
        // if (data is not null) {
        //     foreach (var p in data) {
        //         receivables[p.reference] = p;
        //     }
        // }
    }

    public bool Create(Receivable? receivable) {
        if (receivable is null) {
            return false;
        }

        if (receivables.ContainsKey(receivable.reference)) {
            return false;
        }
        
        receivables[receivable.reference] = receivable;
        return true;
    }

    public IEnumerable<Receivable> GetAll() => receivables.Values.ToList();

    public Receivable? GetByRef(string reference) => receivables.GetValueOrDefault(reference);

    public IEnumerable<Receivable> GetByDebtorRef(string debtorRef) =>
        receivables.Values
            .Where(x => x.debtorReference == debtorRef).ToList();

    public void Update(string reference, Receivable receivable) {
        var existingPayloads = GetByRef(reference);
        if (existingPayloads is null) {
            return;
        }

        receivables[reference] = receivable;
    }

    public void Delete(string reference) => receivables.Remove(reference);
}