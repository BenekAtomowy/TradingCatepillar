using Alpaca.Markets;
using Microsoft.Extensions.DependencyInjection;
using TradingCatepillar.Alpaca.Connector.Services;
using TradingCatepillar.Alpaca.Connector.Services.Interfaces;
using TradingCatepillar.AlpacaAPI.Connector;
using TradingCatepillar.Core.Builders;
using TradingCatepillar.Core.Builders.Interfaces;
using TradingCatepillar.Core.Services;
using TradingCatepillar.Core.Services.Interafaces;
using TradingCatepillar.Data.Analysis.Services;
using TradingCatepillar.Data.Analysis.Services.Interfaces;
using TradingCatepillar.Integration.GoogleGemini.Services;
using TradingCatepillar.Integration.GoogleGemini.Services.Interfaces;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTradingCatepillarServices(this IServiceCollection services)
    {
        services.AddSingleton<AlpacaAPIClient>();
        services.AddTransient<IAlpacaDataClient>(s => s.GetRequiredService<AlpacaAPIClient>().DataClient);
        services.AddTransient<IAlpacaTradingClient>(s => s.GetRequiredService<AlpacaAPIClient>().TradingClient);
        services.AddTransient<IAlpacaDataService, AlpacaDataService>();
        services.AddTransient<IAlpacaTradeService, AlpacaTradeService>();
        services.AddHttpClient();
        services.AddTransient<IGeminiHttpClient, GeminiHttpClient>();
        services.AddTransient<IAIRecommendationService, AIRecommendationService>();
        services.AddTransient<IAIPromptBuilder, AIPromptBuilder>();
        services.AddTransient<IIndicatorCalculationService, IndicatorCalculationService>();
        services.AddTransient<InstrumentWorkerBuilder>();
        return services;
    }
}