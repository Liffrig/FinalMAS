using Uno.UI.Hosting;
using mas_wms;
using mas_wms.Data;
using mas_wms.Services;
using Microsoft.EntityFrameworkCore;

internal class Program {
    [STAThread]
    public static async Task Main(string[] args) {
        
        var services = new ServiceCollection();
        
        // Configure services
        services.AddDbContext<WmsDbContext>(options =>
            options.UseSqlite("Data Source=wms.db"));
        services.AddScoped<IWmsService, WmsService>();

        var serviceProvider = services.BuildServiceProvider();

        // Initialize database
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<WmsDbContext>();
            await context.Database.EnsureDeletedAsync(); // For demo
            await context.Database.EnsureCreatedAsync();
        }

        // Use the service
        using (var scope = serviceProvider.CreateScope())
        {
            var wmsService = scope.ServiceProvider.GetRequiredService<IWmsService>();
            
            await wmsService.AddSampleDataAsync();
            Console.WriteLine();
            
            await wmsService.ListAllItemsAsync();
            
            await wmsService.PrintLotsForItemAsync("FG001");
            
            // Block expired lots
            await wmsService.BlockExpiredLotsAsync();
        }
        
        
        var host = UnoPlatformHostBuilder.Create()
            .App(() => new App())
            .UseX11()
            .UseLinuxFrameBuffer()
            .UseMacOS()
            .UseWin32()
            .Build();

        host.Run();
    }
}
