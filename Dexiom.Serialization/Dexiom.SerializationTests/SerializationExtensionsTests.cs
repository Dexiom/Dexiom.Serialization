using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dexiom.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dexiom.Serialization.Tests
{
    [TestClass()]
    public class SerializationExtensionsTests
    {
        [TestMethod()]
        public void SerializeToBase64Test()
        {
            var data = new List<string> { "Toto", "Titi", "Tata" };
            var serializeToBase64 = data.SerializeToBase64();
            var deserializeFromBase64 = serializeToBase64.DeserializeFromBase64<List<string>>();
            Assert.IsTrue(deserializeFromBase64.Contains("Titi"));
        }

        [TestMethod()]
        public void SerializeToStringTest()
        {
            var data = new List<string> { "Toto", "Titi", "Tata" };
            var serializeToString = data.SerializeToString();
            var deserializeFromString = serializeToString.DeserializeFromString<List<string>>();
            Assert.IsTrue(deserializeFromString.Contains("Tata"));
        }

        [TestMethod()]
        public void DeserializeFromStringTest()
        {
            {
                const string stringValue = "25";
                var serializeToString = stringValue.SerializeToString();
                var deserializeFromString = serializeToString.DeserializeFromString(typeof(int), typeof(string));
                Assert.IsTrue(deserializeFromString.ToString() == stringValue);
            }

            {
                const int intValue = 360;
                var serializeToString = intValue.SerializeToString();
                var deserializeFromString = serializeToString.DeserializeFromString(typeof(int), typeof(string));
                Assert.IsTrue(deserializeFromString.ToString() == intValue.ToString());
            }

            {
                try
                {
                    const string stringValue = "25";
                    var serializeToString = stringValue.SerializeToString();
                    serializeToString.DeserializeFromString(typeof(int));
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Unable to deserialize"))
                        return;
                }
                Assert.Fail();
            }

        }

        [TestMethod()]
        public void IsDeserializableToTest()
        {
            const string stringValue = "25";
            var serializeToString = stringValue.SerializeToString();
            Assert.IsFalse(serializeToString.IsDeserializableTo(typeof(int)));
            Assert.IsTrue(serializeToString.IsDeserializableTo(typeof(string)));
        }

    }
}