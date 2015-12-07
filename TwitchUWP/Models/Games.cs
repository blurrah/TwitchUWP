﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchUWP.Models
{
    /*
    public class Game
    {
        public string title { get; set; }
        public int id { get; set; }
        public int viewers { get; set; }
        public int channels { get; set; }
        public List<BoxArt> boxarts { get; set; }
    }

    public class BoxArt
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string small { get; set; }
    }

    public class GamesContainer
    {
        public int total { get; set; }
        public List<Game> results { get; set; }
    }

    public class GameWrapper
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public string etag { get; set; }
        public GamesContainer data { get; set; }
    }
    */

    public class Links
    {
        public string self { get; set; }
        public string next { get; set; }
    }

    public class Box
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string small { get; set; }
        public string template { get; set; }
    }

    public class Logo
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string small { get; set; }
        public string template { get; set; }
    }

    public class Links2
    {
    }

    public class Game
    {
        public string name { get; set; }
        public int _id { get; set; }
        public int giantbomb_id { get; set; }
        public Box box { get; set; }
        public Logo logo { get; set; }
        public Links2 _links { get; set; }
    }

    public class Top
    {
        public int viewers { get; set; }
        public int channels { get; set; }
        public Game game { get; set; }
    }

    public class RootObject
    {
        public int _total { get; set; }
        public Links _links { get; set; }
        public List<Top> top { get; set; }
    }

    public class GameWrapper
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public string etag { get; set; }
        public RootObject data { get; set; }
    }
}
