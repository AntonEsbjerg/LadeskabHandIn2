using Ladeskab;
using Ladeskab.Doors;
using NSubstitute;
using NUnit.Framework;

namespace LadeskabTest
{
    [TestFixture]
    public class ChargeControlUnitTest
    {
        private ChargeControl _uut;
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbCharger,_display);
        }

        [Test]
        public void PhonePluggedInConnectedIsTrue()
        {
            _uut.PluginPhone();
            Assert.That(_uut.Connected,Is.True);
        }
        [Test]
        public void PhoneUnpluggedConnectedIsFalse()
        {
            _uut.UnplugPhone();
            Assert.That(_uut.Connected, Is.False);
        }
        [Test]
        public void StartCharge__UsbChargeStartChargeIsCalled()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }
        [Test]
        public void StopCharge__UsbChargerStopChargeIsCalled()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }
        [TestCase(double.MinValue)]
        [TestCase(double.MaxValue)]
        [TestCase(0)]
        public void HandleCurrentEvent(double current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() {Current = current});
            Assert.That(_uut.Current, Is.EqualTo(current));
        }

        [TestCase(double.MinValue, null,0)]
        [TestCase(0d, null,0)]
        [TestCase(0.1, "Batteriet er fuldt opladt og kan tages fra strømmen",1)]
        [TestCase(5, "Batteriet er fuldt opladt og kan tages fra strømmen",1)]
        [TestCase(5.1, "Batteriet oplader",1)]
        [TestCase(500, "Batteriet oplader",1)]
        [TestCase(501, "Fejl: Opladning skal afsluttes med det sammer",1)]
        [TestCase(double.MaxValue, "Fejl: Opladning skal afsluttes med det sammer",1)]
        public void DisplayCurrentMessage(double current, string message, int expectedCalls)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            _display.Received(expectedCalls).Print(message);
        }

    }
}