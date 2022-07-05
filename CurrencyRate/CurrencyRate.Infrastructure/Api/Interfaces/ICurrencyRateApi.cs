using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CurrencyRate.Infrastructure.Api.Interfaces
{
    public interface ICurrencyRateApi
    {
        Task<XmlNode> GetXmlCurrencyRates(DateTime date);
    }
}
