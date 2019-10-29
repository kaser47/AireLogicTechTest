using System;
using System.Collections.Generic;

namespace ApiCaller
{
    public class ArtistResponse
    {
        public class LifeSpan
{
    public object ended { get; set; }
    public string begin { get; set; }
}
        public class Tag
{
    public int count { get; set; }
    public string name { get; set; }
}

public class LifeSpan2
{
    public object ended { get; set; }
}

public class LifeSpan3
{
    public object ended { get; set; }
}

public class BeginArea
{
    public string id { get; set; }
    public string type { get; set; }
    public string __invalid_name__type  { get; set; }
    public string name { get; set; }
    public string __invalid_name__sort  { get; set; }
    public LifeSpan3 __invalid_name__life  { get; set; }
}

public class Artist
{
    public string id { get; set; }
    public int score { get; set; }
    public string name { get; set; }
    public string __invalid_name__sort { get; set; }
    public LifeSpan __invalid_name__life { get; set; }
    public List<Tag> tags { get; set; }
    public string type { get; set; }
    public string __invalid_name__type { get; set; }
    public string country { get; set; }
    public BeginArea __invalid_name__begin { get; set; }
    public string disambiguation { get; set; }
    public List<string> isnis { get; set; }
}

public class RootObject
{
    public DateTime created { get; set; }
    public int count { get; set; }
    public int offset { get; set; }
    public List<Artist> artists { get; set; }
}
    }
}