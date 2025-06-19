using mas_wms.Data;
using mas_wms.Model;
using Microsoft.EntityFrameworkCore;

namespace mas_wms.Services;

public interface IWmsService
{
    Task PrintLotsForItemAsync(string itemNumber);
    Task AddSampleDataAsync();
    Task ListAllItemsAsync();
    Task<IEnumerable<Lot>> GetExpiredLotsAsync();
    Task BlockExpiredLotsAsync();
}

public class WmsService : IWmsService
{
    private readonly WmsDbContext _context;

    public WmsService(WmsDbContext context)
    {
        _context = context;
    }

    public async Task PrintLotsForItemAsync(string itemNumber)
    {
        var item = await _context.FinishedGoods
            .Include(i => i.Lots)
            .FirstOrDefaultAsync(i => i.ItemNumber == itemNumber);
            
        if (item == null)
        {
            Console.WriteLine($"Item {itemNumber} not found.");
            return;
        }

        Console.WriteLine($"Lots for Item: {item.ItemNumber} - {item.Description}");
        Console.WriteLine($"Weight: {item.Weight} kg, Value: ${item.BaseValue}");
        Console.WriteLine(new string('-', 60));

        if (!item.Lots.Any())
        {
            Console.WriteLine("No lots found for this item.");
            return;
        }

        foreach (var lot in item.Lots.OrderBy(l => l.ExpiryDate))
        {
            var status = lot.IsBlocked ? "BLOCKED" : "ACTIVE";
            var expired = lot.ExpiryDate < DateTime.Now ? "EXPIRED" : "VALID";
            var daysToExpiry = (lot.ExpiryDate - DateTime.Now).Days;
            
            Console.WriteLine($"Lot: {lot.LotNumber}");
            Console.WriteLine($"  Expiry Date: {lot.ExpiryDate:yyyy-MM-dd}");
            Console.WriteLine($"  Days to Expiry: {daysToExpiry}");
            Console.WriteLine($"  Status: {status}");
            Console.WriteLine($"  Validity: {expired}");
            Console.WriteLine();
        }
    }

    public async Task AddSampleDataAsync()
    {
        // Create finished goods
        var finishedGood1 = new FinishedGood(
            "FG001", "Premium Chocolate Bar", 0.5, 2.0, 10.0, 15.0, 5.99, 25.0);
        var finishedGood2 = new FinishedGood(
            "FG002", "Organic Coffee Beans", 1.0, 20.0, 15.0, 10.0, 12.99, 30.0);

        _context.FinishedGoods.AddRange(finishedGood1, finishedGood2);
        await _context.SaveChangesAsync();

        // Create lots
        var lots = new[]
        {
            Lot.CreateNewLot("LOT001", DateTime.Now.AddDays(30), finishedGood1),
            Lot.CreateNewLot("LOT002", DateTime.Now.AddDays(60), finishedGood1),
            Lot.CreateNewLot("LOT003", DateTime.Now.AddDays(-10), finishedGood1), // Expired
            Lot.CreateNewLot("LOT004", DateTime.Now.AddDays(180), finishedGood2),
            Lot.CreateNewLot("LOT005", DateTime.Now.AddDays(150), finishedGood2)
        };

        // Block expired lot
        lots[2].BlockLot();

        _context.Lots.AddRange(lots);
        await _context.SaveChangesAsync();

        Console.WriteLine("Sample data added successfully!");
    }

    public async Task ListAllItemsAsync()
    {
        var items = await _context.FinishedGoods
            .Include(i => i.Lots)
            .ToListAsync();
        
        Console.WriteLine("All Items in Database:");
        Console.WriteLine(new string('=', 60));
        
        foreach (var item in items)
        {
            Console.WriteLine($"{item.ItemNumber}: {item.Description}");
            Console.WriteLine($"  Weight: {item.Weight} kg, Value: ${item.BaseValue}");
            Console.WriteLine($"  Active Lots: {item.Lots.Count(l => !l.IsBlocked)}");
            Console.WriteLine($"  Blocked Lots: {item.Lots.Count(l => l.IsBlocked)}");
            Console.WriteLine();
        }
    }

    public async Task<IEnumerable<Lot>> GetExpiredLotsAsync()
    {
        return await _context.Lots
            .Include(l => l.RefItem)
            .Where(l => l.ExpiryDate < DateTime.Now)
            .ToListAsync();
    }

    public async Task BlockExpiredLotsAsync()
    {
        var expiredLots = await _context.Lots
            .Where(l => l.ExpiryDate < DateTime.Now && !l.IsBlocked)
            .ToListAsync();

        foreach (var lot in expiredLots)
        {
            lot.BlockLot();
        }

        await _context.SaveChangesAsync();
        Console.WriteLine($"Blocked {expiredLots.Count} expired lots.");
    }
}
