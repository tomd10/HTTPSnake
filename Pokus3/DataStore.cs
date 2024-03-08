using Android.Hardware.Camera2;
using AndroidX.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokus3
{
    public static class DataStore
    {
        /*
         * Global variables
         */ 
        public static float res;
        public static Model.ConfigData cfgData;
        public static Model.ControlMethod method = Model.ControlMethod.Button;
        public static bool configured = false;
        public static bool running = false;
        public static bool cheat = false;
        public static Model.Direction pending = Model.Direction.Up;
        public static System.Timers.Timer timer;
        public static bool lost = false;


    }
}
