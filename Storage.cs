using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntMe.Deutsch;

namespace AntMe.Player.AntMe
{
    public static class Storage
    {
        public static List<Zucker> zucker = new List<Zucker>();
        public static Dictionary<Obst, int> obst = new Dictionary<Obst, int>();
        public static List<Brain> ants = new List<Brain>();
        public static Dictionary<int, bool> scouting = new Dictionary<int, bool>();
        public static List<Ameise> enemy = new List<Ameise>();
    }
}
