using System;
using System.Collections.Generic;
using System.Linq;
using ApiCaller;
using Common;
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
            _lyricFormatter.LyricFormatterMessageLogged += OnMessageLogged;
            _apiCaller = new ApiCaller.ApiCaller();
            _apiCaller.ApiMessageLogged += OnMessageLogged;
        }

        public int FindAverageNumberOfWordsByArtistName(string artistName)
        {
            List<int> totalWordCount = new List<int>();
            Guid artistId = _apiCaller.FindArtist(artistName);
            if (artistId == Guid.Empty)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log("Artist not found");
                return 0;
            }
            
            List<string> songTitles = _apiCaller.FindSongTitlesByArtist(artistId);
            if (songTitles.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log("No song titles found");
                return 0;
            }
            
            foreach (string songTitle in songTitles)
            {
                string lyrics = _apiCaller.FindLyricsByArtistAndTitle(artistName, songTitle);
                if(!string.IsNullOrWhiteSpace(lyrics)) totalWordCount.Add(_lyricFormatter.GetTotalNumberOfWords(lyrics));
            }

            if (totalWordCount.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Log("No lyrics found");
                return 0;
            }
            
            int result = (int)Math.Round(totalWordCount.Average());
            return result;
        }
        
        private void Log(string message)
        {
            OnMessageLogged(this, new MessageEventArgs(message));
        }

        protected virtual void OnMessageLogged(object sender, MessageEventArgs e)
        {
            MessageLogged?.Invoke(this, e);
        }
    }
}