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
            _uut = Substitute.For<IDisplay>();
        }

        [TestCase("Hej")]
        public void Display_Print(string text)
        {
            _uut.Print(text);
            _uut.Received(1).Print(text);
        }
    }
}
