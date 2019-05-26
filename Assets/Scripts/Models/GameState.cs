using System;

namespace Assets.Scripts.Models
{
    /// <summary>
    /// This class is saved to the disk between game sessions.
    /// </summary>
    [Serializable]
    public class GameState
    {
        /// <summary>
        /// The amount of fuel that the player's ship starts with for each flight.
        /// </summary>
        public int FuelCapacity { get; set; }

        /// <summary>
        /// The amount of health that the player starts with for each flight.
        /// </summary>
        public int InitialHealth { get; set; }

        /// <summary>
        /// The level of the player's engine. This affects their maximum speed and acceleration.
        /// </summary>
        public int EngineLevel { get; set; }

        /// <summary>
        /// The level of the player's wings. This affects how quickly the player can turn the ship.
        /// </summary>
        public int WingLevel { get; set; }

        /// <summary>
        /// The amount of money that the currently has available for upgrades.
        /// </summary>
        public int Money { get; set; }

        /// <summary>
        /// If we have no saved data, then put default values in all of the fields.
        /// </summary>
        public void InitializeDefaultValues()
        {
            FuelCapacity = 100;
            InitialHealth = 100;
            EngineLevel = 1;
            WingLevel = 1;
        }
    }
}
