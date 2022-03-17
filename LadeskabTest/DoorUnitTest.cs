using Ladeskab;
using Ladeskab.Doors;
using NUnit.Framework;
using NSubstitute;
using Ladeskab.EventArgs;

namespace LadeskabTest
{
    [TestFixture]
    public class DoorUnitTest
    {
        private Door _uut;
        private DoorEventArgs _receivedEventArgs;
        
        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new Door();
            _uut.OnDoorOpen();
            _uut.OnDoorClose();
            
            //Event listener to check the event occurence and event data
            _uut.DoorEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }
        
        [Test]
        public void OnDoorOpen_EventFired() //tester om vi har modtaget eventet i vores door subjekt
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs,Is.Not.Null); 
        }
        
        [Test]
        public void OnDoorOpen_CorrectValueReceived() //tester at OnDoorOpen er true
        {
            _uut.OnDoorOpen();
            Assert.That(_receivedEventArgs.IsOpen, Is.EqualTo(true));
        }
        
        [Test]
        public void OnDoorClose_CorrectValueReceived() //tester at UnlockDoor er false
        {
            _uut.OnDoorClose();
            Assert.That(_receivedEventArgs.IsOpen, Is.EqualTo(false));
        }
    }
}