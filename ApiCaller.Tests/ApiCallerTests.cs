using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiCaller.Tests
{
    [TestClass]
    public class ApiCallerTests
    {
        private string testArtistName = "Queen";
        private Guid testArtistId = Guid.Parse("0383dadf-2a4e-4d10-a46a-e9e041da8eb3"); //Queens Artist Id
        private Guid testIncorrectArtistId = Guid.Parse("0383dadf-2a4e-4d10-a46a-e9e041da8eb4");
        private string testSongTitle = "We are the champions";

        [TestMethod]
        public void ApiCaller_FindArtist_Queen_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            Guid result =caller.FindArtist(testArtistName);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(Guid.Empty, result);
        }
        
        [TestMethod]
        public void ApiCaller_FindArtist_EmptyString_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            Guid result =caller.FindArtist("");
            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Empty, result);
        }
        
        [TestMethod]
        public void ApiCaller_FindArtist_WhiteSpace_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            Guid result =caller.FindArtist(" ");
            Assert.IsNotNull(result);
            Assert.AreEqual(Guid.Empty, result);
        }
        
        [TestMethod]
        public void ApiCaller_FindTitles_Queen_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            List<string> result =caller.FindSongTitlesByArtist(testArtistId);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0); 
            //I thought about asserting the exact number of songs that are returned 220. However if queen ever released another song
            //this unit test would break.
        }
        
        [TestMethod]
        public void ApiCaller_FindTitles_EmptyGuid_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            List<string> result =caller.FindSongTitlesByArtist(Guid.Empty);
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void ApiCaller_FindTitles_IncorrectGuid_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            List<string> result = caller.FindSongTitlesByArtist(testIncorrectArtistId);
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void ApiCaller_FindLyrics_QueenWeAreTheChampions_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            string result =caller.FindLyricsByArtistAndTitle(testArtistName ,testSongTitle);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("champions"));
        }
        
        [TestMethod]
        public void ApiCaller_FindLyrics_IncorrectArtistWeAreTheChampions_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            string result =caller.FindLyricsByArtistAndTitle(testArtistName ,testSongTitle);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Contains("champions"));
        }
        
        [TestMethod]
        public void ApiCaller_FindLyrics_NullArtisttWeAreTheChampions_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            string result =caller.FindLyricsByArtistAndTitle("" ,testSongTitle);
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void ApiCaller_FindLyrics_QueenNullTitle_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            string result =caller.FindLyricsByArtistAndTitle(null ,"");
            Assert.IsNull(result);
        }
        
        [TestMethod]
        public void ApiCaller_FindLyrics_nullArtist_nullTitle_ReturnsCorrectData()
        {
            ApiCaller caller = new ApiCaller();
            string result =caller.FindLyricsByArtistAndTitle(null ,null);
            Assert.IsNull(result);
        }

        
    }
}
