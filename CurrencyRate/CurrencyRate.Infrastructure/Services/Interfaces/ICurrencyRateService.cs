using System;
using System.Threading.Tasks;

namespace CurrencyRate.Infrastructure.Services.Intefraces
{
    public interface ICurrencyRateService
    {
        Task<string> GetJsonCurrencyRate(DateTime? date, string code);
    }
}
