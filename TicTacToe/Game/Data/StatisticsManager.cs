using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace TicTacToe.Game.Data
{
    class StatisticsManager
    {
        public Statistic LoadPlayersStatistics(int playerID)
        {
            XDocument textureConfig = XDocument.Load("assets/data/Statistics.xml");
        }
    }
}
