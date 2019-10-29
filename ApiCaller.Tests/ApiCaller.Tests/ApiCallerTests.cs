using System;
using NUnit.Framework;

namespace ApiCaller.Tests
{
    [TestFixture]
    public class ApiCallerTests
    {
        [Test]
        public void ApiCaller_FindArtist_ACDC_ReturnsExpectedData()
        {
           ApiCaller apiCaller = new ApiCaller();
           apiCaller.FindArtist("ACDC");
        }
    }
}