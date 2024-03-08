using Android.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokus3
{
    public static class Model
    {
        /*
         * Data structures
         */ 

        public struct ConfigData
        {
            public int speed;
            public int dim;
        }

        public enum ControlMethod
        {
            Button,
            Swipe
        }

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public enum cellState
        {
            Empty,
            Head,
            Body,
            Food
        }

        //Parsing of HTTP fetched config file
        // 0,0 - wrong format
        // -1, -1 - HTTP error
        // everything else - OKay
        public static ConfigData ParseConfig(string raw)
        {
            ConfigData result = new ConfigData();
            result.speed = 0;
            result.dim = 0;
            int speed = 0;
            int dim = 0;

            if (raw == "HTTPErr")
            {
                result.speed = -1;
                result.dim = -1;
                return result;
            }
            string[] lines = raw.Split(new[] { '\n', '\r' });
            if (lines.Length != 3) return result;
            if (lines[0] != "HTTPSnake Config File") return result;
            
            string[] words = lines[1].Split(':');
            if (words.Length != 2) return result;
            if (words[0] != "Speed") return result;
            try
            {
                speed = Convert.ToInt32(words[1].Trim());
            }
            catch
            {
                return result;
            }

            words = lines[2].Split(':');
            if (words.Length != 2) return result;
            if (words[0] != "Dim") return result;
            try
            {
                dim = Convert.ToInt32(words[1].Trim());
            }
            catch
            {
                return result;
            }

            if (speed <= 0) return result;
            if (dim <= 7 || dim > 20) return result;

            result.speed = speed;
            result.dim = dim;

            return result;

        }





    }
}
