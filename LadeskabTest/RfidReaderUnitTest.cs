using Ladeskab;
using Ladeskab.Doors;
using Ladeskab.EventArgs;
using Ladeskab.RfidReaders;
using NUnit.Framework;
using NSubstitute;

namespace LadeskabTest
{
    [TestFixture]
    class RfidReaderUnitTest
    {
        private RfidReader _uut;
        private RfidEventArgs _receivedEventArgs;

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
            _uut.ReadRfid(1);
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(12378u)]
        [TestCase(uint.MaxValue)]
        [TestCase(uint.MinValue)]
        public void ReadRfid_CorrectValueReceived(uint id) 
        {
            _uut.ReadRfid(id);

            Assert.That(_receivedEventArgs.Rfid, Is.EqualTo(id));
        }

        [TestCase(12378u)]
        public void ReadRfid_(uint id)
        {
            _uut.ReadRfid(id);
            
                
        }



    }
}
