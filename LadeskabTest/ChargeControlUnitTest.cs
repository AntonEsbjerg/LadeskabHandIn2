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

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger);
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

    }
}