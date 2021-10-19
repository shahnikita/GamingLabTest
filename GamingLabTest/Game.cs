using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingLabTest
{
    public class Game
    {
        /// <summary>
        /// the primary key of the Game.
        /// </summary>
        public int GameID { get; set; }

        /// <summary>
        /// the name of the Game.
        /// </summary>
        public string GameName { get; set; }
        /// <summary>
        /// the date that the Game was submitted to GLI by the customer.
        /// </summary>
        public DateTime ReceivedDate { get; set; }
        /// <summary>
        /// the date that the Game was accepted for testing by a GLI Engineering Manager.
        /// </summary>
        public DateTime AcceptedDate { get; set; }
        /// <summary>
        /// the total dollar amount that GLI stands to gain by testing the Game.
        /// </summary>
        public decimal ContractValue { get; set; }
        /// <summary>
        /// the level of complexity that the Game represents (Low, Medium, or High)
        /// </summary>
        public GameComplexity Complexity { get; set; }
    }

    public enum GameComplexity
    {
        Low,
        Medium,
        High
    }

    public class GameList
    {
        public List<Game> Games { get; set; }
    }
}
