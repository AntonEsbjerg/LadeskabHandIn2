using System;
using Ladeskab;
using NUnit.Framework;
using NSubstitute;

namespace LadeskabTest
{
    [TestFixture]
    class DisplayUnitTest
    {
        private IDisplay _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [TestCase("Hej")]
        public void Display_Print(string text)
        {
            _uut.Print(text);
            Assert.That(_uut.Message, Is.EqualTo(text));
        }
    }
}
