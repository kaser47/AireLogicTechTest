using System;

namespace LyricsFormatter
{
    public class LyricFormatter
    {
        public int GetTotalNumberOfWords(string lyrics)
        {
            if (String.IsNullOrWhiteSpace(lyrics)) return 0;
            
            lyrics = lyrics.Replace("/n", " ");
            string[] result = lyrics.Split(Char.Parse(" "));
            return result.Length;
        }
    }
}