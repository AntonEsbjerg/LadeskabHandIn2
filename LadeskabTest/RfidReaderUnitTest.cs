using System;
using Ladeskab;
using Ladeskab.Doors;
using Ladeskab.EventArgs;
using Ladeskab.RfidReaders;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace LadeskabTest
{
    [TestFixture]
    class RfidReaderUnitTest
    {
        private RfidReader _uut;
        private RfidEventArgs _receivedEventArgs;
        //private RfidReader subReader = Substitute.For<RfidReader>();

            [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new RfidReader();

            //Event listener to check the event occurence and event data
            _uut.RfidEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }

        [Test]
        public void ReadRfid_EventFired() 

        {
            _uut.ReadRfid(1,DateTime.Now);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void ReadRfid_EventFiredNull()
        {

            _uut.RfidEvent += null;
            Assert.That(_receivedEventArgs, Is.Null);
        }

        [TestCase(12378u)]
        [TestCase(uint.MaxValue)]
        [TestCase(uint.MinValue)]
        public void ReadRfid_CorrectValueReceivedRfid(uint id) 
        {
            _uut.ReadRfid(id,DateTime.Now);

            Assert.That(_receivedEventArgs.Rfid, Is.EqualTo(id));
        }

        [TestCase(12378u)]
        [TestCase(uint.MaxValue)]
        [TestCase(uint.MinValue)]
        public void ReadRfid_CorrectValueReceivedDateTime(uint id)
        {
            DateTime date = new DateTime();
            _uut.ReadRfid(id, date = DateTime.Now);

            Assert.That(_receivedEventArgs.Time, Is.EqualTo(date));
        }

        [TestCase(12378u)]
        [TestCase(uint.MaxValue)]
        [TestCase(uint.MinValue)]
        public void ReadRfid_WrongValueReceived(uint id)
        {
            _uut.ReadRfid(id+1, DateTime.Now);

            Assert.That(_receivedEventArgs.Rfid, Is.Not.EqualTo(id));
        }

    }
}
