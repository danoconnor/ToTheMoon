using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class FlightData
    {
        /// <summary>
        /// How long the flight lasted
        /// </summary>
        public TimeSpan FlightTime { get; set; }

        /// <summary>
        /// The maximum height that the player reached.
        /// </summary>
        public float MaxHeight { get; set; }

        /// <summary>
        /// Any extra money the player earned by picking up bonus items during the flight.
        /// </summary>
        public int BonusMoney { get; set; }
    }
}
