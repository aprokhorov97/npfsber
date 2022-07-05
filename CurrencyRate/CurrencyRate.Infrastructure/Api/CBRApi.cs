using CBRServiceReference;
using CurrencyRate.Infrastructure.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CurrencyRate.Infrastructure.Api
{
    public class CBRApi : ICurrencyRateApi
    {
        private readonly DailyInfoSoap _client;

        public CBRApi()
        {
            _client = new DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap);
        }

        public async Task<XmlNode> GetXmlCurrencyRates(DateTime date)
        {
            var result = await _client.GetCursOnDateXMLAsync(date);
            return result;
        }
    }
}
