using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dexiom.Serialization
{
    internal class EncodingHelper
    {
        public static string EncodeBase64(string value) => EncodeBase64(value, null);
        public static string EncodeBase64(string value, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return Convert.ToBase64String(encoding.GetBytes(value));
        }
        

        public static string DecodeBase64(string encodedValue) => DecodeBase64(encodedValue, null);
        public static string DecodeBase64(string encodedValue, Encoding encoding)
        {
            encoding = encoding ?? Encoding.UTF8;
            return encoding.GetString(Convert.FromBase64String(encodedValue));
        }
    }
}
