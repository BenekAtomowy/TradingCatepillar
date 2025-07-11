// See https://aka.ms/new-console-template for more information
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TradingCatepillar.AlpacaAPI.Connector;
using TradingCatepillar.Core.Mapping;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Persistence;

Console.WriteLine("Trading Catepillar - console app");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


    })
    .ConfigureServices((context, services) =>
    {
        services.AddTradingCatepillarServices();

        //var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

        string baseDirectory = AppContext.BaseDirectory; // To jest np. bin/Debug/netX.Y/

        string projectRootPath = Path.GetFullPath(Path.Combine(baseDirectory, "..", "..", ".."));

        string dbFileName = "mydatabase.db";
        string fullDbPath = Path.Combine(projectRootPath, dbFileName);

        string connectionString = $"Data Source={fullDbPath}";

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));


        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });


    })
    .Build();


await host.Services.GetRequiredService<AlpacaAPIClient>().Initialize();
host.Services.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();

var workerService = host.Services.GetRequiredService<IInstrumentWorkerService>();

workerService.AddWorker("MSFT");
//workerService.AddWorker("AAPL");
//workerService.AddWorker("LMT");
//workerService.AddWorker("GOOGL");
//workerService.AddWorker("AMZN");
//workerService.AddWorker("TSLA");
//workerService.AddWorker("NFLX");
//workerService.AddWorker("META");
//workerService.AddWorker("NVDA");
//workerService.AddWorker("AMD");
//workerService.AddWorker("INTC");
//workerService.AddWorker("CSCO");


await workerService.WorkAllAsync();

var test = "";



