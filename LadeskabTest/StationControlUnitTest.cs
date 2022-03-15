using Ladeskab;
using Ladeskab.Doors;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabTest
{
    [TestFixture]
    public class StationControlUnitTest
    {
        private IDoor _door;

        private StationControl _uut;
        
        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();

            _uut = new StationControl(_door);
        }
        
        // Vi får testet vores StationControl observer er koblet til door event
        // Vi får testet vores StationControl modtager data fra Door
        // Vi får testet at vores StationControl giver de rigtige værdier ud fra de data den modtager
        [TestCase(true)]
        [TestCase(false)]
        public void DoorChanged_DifferentArguments_IsDoorOpen(bool o) 
        {
            _door.DoorEvent += Raise.EventWith(new DoorEventArgs {IsOpen = o});
            Assert.That(_uut.CurrentDoorStatus, Is.EqualTo(o));
        }
    }
}