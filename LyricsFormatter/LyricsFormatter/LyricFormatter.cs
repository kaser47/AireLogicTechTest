using System;
using Common;

namespace LyricsFormatter
{
    public class LyricFormatter
    {
        public event EventHandler<MessageEventArgs> LyricFormatterMessageLogged;  
        
        public int GetTotalNumberOfWords(string lyrics)
        {
            if (String.IsNullOrWhiteSpace(lyrics)) return 0;
            
            lyrics = lyrics.Replace("/n", " ");
            string[] result = lyrics.Split(Char.Parse(" "));
            return result.Length;
        }
        
        private void Log(string message)
        {
            OnLyricFormatterMessageLogged(new MessageEventArgs(message));
        }
        
        protected virtual void OnLyricFormatterMessageLogged(MessageEventArgs e)
        {
            LyricFormatterMessageLogged?.Invoke(this, e);
        }
    }
}