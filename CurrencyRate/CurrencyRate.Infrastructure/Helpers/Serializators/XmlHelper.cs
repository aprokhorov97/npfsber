using System.Xml;
using System.Xml.Serialization;

namespace CurrencyRate.Infrastructure.Helpers.Serializators
{
    public static class XmlHelper<T>
    {
        public static T Deserialize(XmlNode xmlNode)
        {
            T result;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (XmlNodeReader reader = new XmlNodeReader(xmlNode))
            {
                result = (T)xmlSerializer.Deserialize(reader);
            }

            return result;
        }
    }
}
