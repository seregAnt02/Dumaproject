using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace duma.Models
{
    public class TotalPrice
    {
        public int Id { get; set; }
        public int TotatalAreaHouse { get; set; }
        public int NumberRooms { get; set; }
        public int Lighting { get; set; }
        public int HeatingSystem { get; set; }
        public int VentilationSystem { get; set; }

        public int AirConditionigControl { get; set; }
        public int MotionSensor { get; set; }
        public int FireSensor { get; set; }
        public int ManagingTheInputGroup { get; set; }
        public int LeakProtection { get; set; }
        public int ManagingSockets { get; set; }
        public int InstallitionOfElectricalWiring { get; set; }
    }
}