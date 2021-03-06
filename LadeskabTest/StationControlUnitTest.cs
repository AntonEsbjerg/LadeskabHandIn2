using System;
using Ladeskab;
using Ladeskab.Doors;
using Ladeskab.EventArgs;
using Ladeskab.RfidReaders;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
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
        private IJsonLogger _fakeJsonLogger;
        
        
        [SetUp]
        public void Setup()
        {
            _fakeDoor = Substitute.For<IDoor>();
            _fakeChargeControl = Substitute.For<IChargeControl>();
            _fakeReader = Substitute.For<IRfidReader>();
            _fakeDisplay = Substitute.For<IDisplay>();
            _fakeJsonLogger = Substitute.For<IJsonLogger>();
            _uut = new StationControl(_fakeDoor, _fakeChargeControl, _fakeReader, _fakeDisplay,_fakeJsonLogger);
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
            _uut.CurrentDoorStatus = false;
            _fakeChargeControl.Connected.Returns(true);
            _uut.DoorClosed();
            _fakeDisplay.Received(1).Print("Indlæs RFID");
        }

        [TestCase(5)]
        public void StationControl_RFIDDetected_AvailableAndPhoneConnectet(int id)
        {
            
            _fakeChargeControl.Connected = true;

            _uut.RFIDDetected(Convert.ToUInt32(id), DateTime.Now);
            _uut.RFIDDetected(Convert.ToUInt32(id),DateTime.Now);

            _fakeDoor.Received(1).LockDoor();
            _fakeChargeControl.Received(1).StartCharge();
            _fakeDisplay.Received(1).Print("Skabet er låst og din telefon lades. " +"Brug dit RFID tag til at låse op.");
        }
        [TestCase(5)]
        public void StationControl_RFIDDetected_AvailableAndPhoneDisconnectet(int id)
        {

            _fakeChargeControl.Connected = false;

            _uut.RFIDDetected(Convert.ToUInt32(id), DateTime.Now);

            _fakeDisplay.Received(1).Print("Din telefon er ikke ordentlig tilsluttet. " + "Prøv igen.");
        }
        [TestCase(5)]
        public void StationControl_RFIDDetected_DoorOpen(int id)
        {
            _uut.DoorOpened();

            _uut.RFIDDetected(Convert.ToUInt32(id),DateTime.Now);

            _fakeDisplay.Received(1).Print("The door is open...");
        }

  
        [TestCase(2u,5u)]
        public void StationControl_RFIDDetected_Locked_InvalidId(uint Id, uint wrongId)
        {
            uint rfid = 111u;
            DateTime time = DateTime.Now;

            _fakeChargeControl.Connected.Returns(true);
            _fakeReader.RfidEvent += (sender, args) =>
            {
                rfid = Id;
                time = DateTime.Now;
            };
            RfidEventArgs eventArgs = new RfidEventArgs();
            eventArgs.Time = time;
            eventArgs.Rfid = rfid;
            //Tell the substitute to raise the event with a sender and EventArgs:
            _fakeReader.RfidEvent += Raise.EventWith(new object(), eventArgs);
            eventArgs.Time = time;
            eventArgs.Rfid = wrongId;
            _fakeReader.RfidEvent += Raise.EventWith(new object(), eventArgs);
            _fakeDisplay.Received(1).Print("Forkert RFID tag");
        }

        [TestCase(1u,1u,true)]
        [TestCase(5u,5u,true)]
        [TestCase(1234u,1234u,true)]
        [TestCase(5u,10u,false)]
        [TestCase(20u,5u,false)]
        public void StationControl_CheckId(uint oldId, uint Id, bool expectedResult)
        {
            bool result = _uut.CheckId(oldId, Id);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void StationControl_DoorClosed_LoggerDoorLockedIsCalled()
        {
            DateTime time= DateTime.Now;
            //_uut._state = StationControl.LadeskabState.Available;
            _fakeChargeControl.Connected.Returns(true);
            _uut.RFIDDetected(123,time);
            _fakeJsonLogger.Received(1).LogDoorLocked(123, time);
        }
        [Test]
        public void StationControl_DoorOpened_LoggerDoorUnlockedIsCalled()
        {
             DateTime time = DateTime.Now;
            uint oldId = 123;
            //_uut._state = StationControl.LadeskabState.Locked;
            _fakeChargeControl.Connected.Returns(true);
            _uut.RFIDDetected(oldId, time);
            _uut.RFIDDetected(123, time);
            _fakeJsonLogger.Received(1).LogDoorUnlocked(123, time);
        }

        [Test]
        public void StationControl_DoorEventHandler_Open()
        {
            var Dooropen = false;
            _fakeDoor.DoorEvent += (sender, args) => Dooropen = false;
            DoorEventArgs eventArgs = new DoorEventArgs();
            eventArgs.IsOpen = true;
            _fakeDoor.DoorEvent += Raise.EventWith(new object(), eventArgs);
            Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(true));
        }

        [Test]
        public void StationControl_DoorEventHandler_Close()
        {
            var Dooropen = true;
            _fakeDoor.DoorEvent += (sender, args) => Dooropen = true;
            DoorEventArgs eventArgs = new DoorEventArgs();
            eventArgs.IsOpen = false;
            _fakeDoor.DoorEvent += Raise.EventWith(new object(), eventArgs);
            Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(false));
        }

        [Test]
        public void StationControl_RFIDEventHandler()
        {
            uint rfid = 111u;
            DateTime time= DateTime.Now;
            
            _fakeChargeControl.Connected.Returns(true);
            _fakeReader.RfidEvent += (sender, args) =>
            {
                rfid = 111;
                time = DateTime.Now;
            };
            RfidEventArgs eventArgs = new RfidEventArgs();
            eventArgs.Time = time;
            eventArgs.Rfid = rfid;
            //Tell the substitute to raise the event with a sender and EventArgs:
           _fakeReader.RfidEvent += Raise.EventWith(new object(), eventArgs);
           _fakeDisplay.Received(1).Print("Skabet er låst og din telefon lades. " +
                                          "Brug dit RFID tag til at låse op.");
        }
        [Test]
        public void StationControl_RFIDEventHandlerTwoUnlocks()
        {
            uint rfid = 111u;
            DateTime time = DateTime.Now;

            _fakeChargeControl.Connected.Returns(true);
            _fakeReader.RfidEvent += (sender, args) =>
            {
                rfid = 111;
                time = DateTime.Now;
            };
            RfidEventArgs eventArgs = new RfidEventArgs();
            eventArgs.Time = time;
            eventArgs.Rfid = rfid;
            //Tell the substitute to raise the event with a sender and EventArgs:
            _fakeReader.RfidEvent += Raise.EventWith(new object(), eventArgs);
            _fakeReader.RfidEvent += Raise.EventWith(new object(), eventArgs);
            _fakeDisplay.Received(1).Print("Tag din telefon ud af skabet og luk døren");
        }
    }
}