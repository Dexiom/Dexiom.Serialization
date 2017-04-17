using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Dexiom.Serialization
{
    public static class SerializationExtensions
    {
        public static string SerializeToBase64(this object source)
        {
            return EncodingHelper.EncodeBase64(SerializeToString(source));
        }

        public static string SerializeToString(this object source)
        {
            var serializer = new XmlSerializer(source.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, source);
            }

            return sb.ToString();
        }

        public static T DeserializeFromString<T>(this string source)
        {
            return (T)DeserializeFromString(source, typeof(T));
        }

        public static T DeserializeFromBase64<T>(this string source)
        {
            return (T)DeserializeFromString(EncodingHelper.DecodeBase64(source), typeof(T));
        }

        //public static object DeserializeFromString(this string source, Type type)
        //{
        //    var serializer = new XmlSerializer(type);
        //    using (var reader = new StringReader(source))
        //    {
        //        return serializer.Deserialize(reader);
        //    }
        //}

        public static object DeserializeFromString(this string source, params Type[] types)
        {
            foreach (var type in types)
            {
                var serializer = new XmlSerializer(type);
                using (var stringReader = new StringReader(source))
                {
                    using (var xmlReader = XmlReader.Create(stringReader))
                    {
                        if (serializer.CanDeserialize(xmlReader))
                        {
                            return serializer.Deserialize(xmlReader);
                        }
                    }
                }
            }

            throw new Exception("Unable to deserialize to any of the provided types.");
        }

        public static bool IsDeserializableTo(this string source, Type type)
        {
            var serializer = new XmlSerializer(type);
            using (var stringReader = new StringReader(source))
            {
                using (var xmlReader = XmlReader.Create(stringReader))
                {
                    return serializer.CanDeserialize(xmlReader);
                }
            }
        }
    }
}
