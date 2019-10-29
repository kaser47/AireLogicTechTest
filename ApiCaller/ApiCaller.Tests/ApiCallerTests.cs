using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiCaller.Tests
{
    [TestClass]
    public class ApiCallerTests
    {
        [TestMethod]
        public void ApiCaller_FindArtist_ACDC_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            caller.FindArtist("ACDC");
        }
    }
}
