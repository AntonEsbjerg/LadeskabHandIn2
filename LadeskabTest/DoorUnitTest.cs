using Ladeskab;
using Ladeskab.Doors;
using NUnit.Framework;
using NSubstitute;

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
            _uut.LockDoor();
            _uut.UnlockDoor();
            
            //Event listener to check the event occurence and event data
            _uut.DoorEvent +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }
        
        [Test]
        public void LockDoor_EventFired() //tester om vi har modtaget eventet i vores door subjekt
        {
            _uut.LockDoor();
            Assert.That(_receivedEventArgs,Is.Not.Null); 
        }
        
        [Test]
        public void LockDoor_CorrectValueReceived() //tester at lockDoor er true
        {
            _uut.LockDoor();
            Assert.That(_receivedEventArgs.IsOpen, Is.EqualTo(true));
        }
        
        [Test]
        public void UnLockDoor_CorrectValueReceived() //tester at UnlockDoor er false
        {
            _uut.UnlockDoor();
            Assert.That(_receivedEventArgs.IsOpen, Is.EqualTo(false));
        }
    }
}