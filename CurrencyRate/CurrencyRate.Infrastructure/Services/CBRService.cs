using CurrencyRate.Infrastructure.Api.Interfaces;
using CurrencyRate.Infrastructure.Helpers.Serializators;
using CurrencyRate.Infrastructure.Services.Intefraces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace CurrencyRate.Infrastructure.Services
{
    public class CBRService : ICurrencyRateService
    {
        private readonly ICurrencyRateApi _currencyRateApi;
        private readonly ILogger<CBRService> _logger;
        
        public CBRService(
                ICurrencyRateApi currencyRateApi,
                ILogger<CBRService> logger
            )
        {
            _currencyRateApi = currencyRateApi;
            _logger = logger;
        }

        public async Task<string> GetJsonCurrencyRate(DateTime? date, string code)
        {
            string json = string.Empty;
            try
            {
                var xml = await GetXmlFromCBR(date ?? DateTime.Today);
                var valuteData = DeserializeXML(xml);
                if (!string.IsNullOrEmpty(code))
                    FilterByCode(valuteData, code);

                json = SerializeJson(valuteData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return json;
        }
        
        private async Task<XmlNode> GetXmlFromCBR(DateTime? date)
        {
            _logger.LogInformation($"Request to CBR was started");
            var xml = await _currencyRateApi.GetXmlCurrencyRates(date ?? DateTime.Today);
            _logger.LogInformation($"Request to CBR was finished");
            return xml;
        }

        private ValuteData DeserializeXML(XmlNode xmlNode)
        {
            _logger.LogInformation($"XML deserialization was started");
            var valuteData = XmlHelper<ValuteData>.Deserialize(xmlNode);
            _logger.LogInformation($"XML deserialization was finished");
            return valuteData;
        }

        private void FilterByCode(ValuteData valuteData, string code)
        {
            var x = valuteData.ValuteCursOnDate.Where(curs => curs.VchCode.ToLower().Equals(code.ToLower())).ToArray();
            valuteData.ValuteCursOnDate = valuteData.ValuteCursOnDate.Where(curs => curs.VchCode.Equals(code)).ToArray();
        }

        private string SerializeJson(ValuteData valuteData)
        {
            _logger.LogInformation($"JSON serialization was started");
            var json = valuteData.ValuteCursOnDate.Length > 0 
                ? JsonHelper<ValuteData>.Serialize(valuteData)
                : string.Empty;
            _logger.LogInformation($"JSON serialization was finished");
            return json;
        }
    }
}
