using System;
using System.Collections.Generic;
using System.Linq;
using LyricsFormatter;
using NUnit.Framework;

namespace ApiCaller.Tests
{
    [TestFixture]
    public class ApiCallerTests
    {
        [Test]
        public void ApiCaller_FindArtist_Queen_ReturnsExpectedData()
        {
            List<int> totalWordCount = new List<int>();
           ApiCaller apiCaller = new ApiCaller();
           LyricFormatter lyricFormatter = new LyricFormatter();
           Guid artistId = apiCaller.FindArtist("queen");
           List<string> songTitles = apiCaller.FindSongTitlesByArtist(artistId);
           foreach (string songTitle in songTitles)
           {
               //Only add item to word count if lyrics found.

               string lyrics = apiCaller.FindLyricsByArtistAndTitle(songTitle);
               
               if(!string.IsNullOrWhiteSpace(lyrics)) totalWordCount.Add(lyricFormatter.GetTotalNumberOfWords(lyrics));
           }

           //RESULT!
           Math.Round(totalWordCount.Average());

           var a = totalWordCount;
        }
    }
}