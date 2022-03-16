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
            IJsonFileModel expected = 
            new JsonFileModel()
            {
                Rfid = rfid,
                Time = time,
                IsDoorLocked = true
            };
            IJsonFileModel jsonFileModel = _uut.LogDoorLocked(rfid, time);
            Assert.That(jsonFileModel.Time, Is.EqualTo(expected.Time));
            Assert.That(jsonFileModel.IsDoorLocked, Is.EqualTo(expected.IsDoorLocked));
            Assert.That(jsonFileModel.Rfid, Is.EqualTo(expected.Rfid));
        }
        [TestCase(12u)]
        [TestCase(uint.MinValue)]
        [TestCase(uint.MaxValue)]
        public void LogDoorUnlocked(uint rfid)
        {
            DateTime time = DateTime.Now;
            IJsonFileModel expected =
                new JsonFileModel()
                {
                    Rfid = rfid,
                    Time = time,
                    IsDoorLocked = false
                };
            IJsonFileModel jsonFileModel = _uut.LogDoorUnlocked(rfid, time);
            Assert.That(jsonFileModel.Time, Is.EqualTo(expected.Time));
            Assert.That(jsonFileModel.IsDoorLocked, Is.EqualTo(expected.IsDoorLocked));
            Assert.That(jsonFileModel.Rfid, Is.EqualTo(expected.Rfid));
        }
    }
}