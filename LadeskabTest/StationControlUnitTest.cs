using Ladeskab;
using Ladeskab.Doors;
using Ladeskab.RfidReaders;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabTest
{
    [TestFixture]
    public class StationControlUnitTest
    {
        private StationControl _uut;
        private IDoor _fakeDoor;
        private IChargeControl _fakeChargeControl;
        private IRfidReader _fakeReader;
        private IDisplay _fakeDisplay;
        
        
        [SetUp]
        public void Setup()
        {
            _fakeDoor = Substitute.For<IDoor>();
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeReader = Substitute.For<IRfidReader>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _uut = new StationControl(_fakeDoor, _fakeChargeControl, _fakeReader, _fakeDisplay);
        }

        [Test]
        public void StationControl_DoorOpen_IsCalled()
        {
            _uut.DoorOpened();
            _fakeDisplay.Received(1).Print("Tilslut telefon");
        }
        [Test]
        public void StationControl_DoorClosed_IsCalled()
        {
            _uut.DoorClosed();
            _fakeDisplay.Received(1).Print("Indlæs RFID");
        }

        [TestCase()]
        public void StationControl_RFIDDetected_AvailableAndPhoneConnectet(uint id,StationControl.LadeskabState state, bool ChargeConnectionBool)
        {
            _uut._state = state;
            _fakeChargeControl.Connected = ChargeConnectionBool;

            _uut.RFIDDetected(id);
            _fakeDisplay.Received(1).Print("Indlæs RFID");
        }

        //[TestCase(0)]
        //public void HandleCurrentEvent(double current)
        //{
        //    _uut. += Raise.EventWith(new CurrentEventArgs() { Current = current });
        //    Assert.That(_uut.Current, Is.EqualTo(current));
        //}

        [TestCase(1,1,true)]
        [TestCase(5,5,true)]
        [TestCase(1234,1234,true)]
        [TestCase(5,10,false)]
        [TestCase(20,5,false)]
        public void StationControl_CheckId(uint oldId, uint Id, bool expectedResult)
        {
            bool result = _uut.CheckId(oldId, Id);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}