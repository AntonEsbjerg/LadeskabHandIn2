using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Ladeskab;
using NUnit.Framework.Internal;

namespace LadeskabTest
{
    [TestFixture]
    public class LoggerUnitTest
    {
        private IJsonLogger _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new JsonLogger();
        }

        [TestCase(12u)]
        [TestCase(uint.MinValue)]
        [TestCase(uint.MaxValue)]
        public void LogDoorLocked(uint rfid)
        {
            DateTime time= DateTime.Now;
            List<IJsonFileModel> jsonList = new List<IJsonFileModel>();
            jsonList.Add(new JsonFileModel()
            {
                Rfid = rfid,
                Time = time,
                IsDoorLocked = true
            });
            string expected= JsonSerializer.Serialize(jsonList);
            string result=_uut.LogDoorLocked(rfid,time);
            Assert.That(result,Is.EqualTo(expected));
        }
        [TestCase(12u)]
        [TestCase(uint.MinValue)]
        [TestCase(uint.MaxValue)]
        public void LogDoorUnlocked(uint rfid)
        {
            DateTime time = DateTime.Now;
            List<IJsonFileModel> jsonList = new List<IJsonFileModel>();
            jsonList.Add(new JsonFileModel()
            {
                Rfid = rfid,
                Time = time,
                IsDoorLocked = false
            });
            string expected = JsonSerializer.Serialize(jsonList);
            string result = _uut.LogDoorUnlocked(rfid, time);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}