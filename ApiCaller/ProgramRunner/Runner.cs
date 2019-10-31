using System;
using System.Collections.Generic;
using System.Linq;
using ApiCaller;
using LyricsFormatter;

namespace ProgramRunner
{
    public class Runner
    {
        public event EventHandler<MessageEventArgs> MessageLogged;  
        private ApiCaller.ApiCaller _apiCaller;
        private LyricFormatter _lyricFormatter;
        public Runner()
        {
            _lyricFormatter = new LyricFormatter();
            _apiCaller = new ApiCaller.ApiCaller();
        }

        public double FindAverageNumberOfWordsByArtistName(string artistName)
        {
            List<int> totalWordCount = new List<int>();
            Guid artistId = _apiCaller.FindArtist(artistName);
            List<string> songTitles = _apiCaller.FindSongTitlesByArtist(artistId);
            foreach (string songTitle in songTitles)
            {
                string lyrics = _apiCaller.FindLyricsByArtistAndTitle(songTitle);
                if(!string.IsNullOrWhiteSpace(lyrics)) totalWordCount.Add(_lyricFormatter.GetTotalNumberOfWords(lyrics));
            }
            double result = Math.Round(totalWordCount.Average());
            return result;
        }
    }

    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }
}