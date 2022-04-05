using System;
using System.IO;
using Ladeskab;
using NUnit.Framework;
using NSubstitute;

namespace LadeskabTest
{
    [TestFixture]
    class DisplayUnitTest
    {
        private IDisplay _uut;
        private StringWriter _sw;

        [SetUp]
        public void Setup()
        {
            _sw = new StringWriter();
            _uut = new Display(_sw);   
        }

        [TestCase("Hej")]
        public void Display_Print(string text)
        {
            _uut.Print(text);
            Assert.That(_sw.ToString(), Is.EqualTo(text));
        }
    }
}
