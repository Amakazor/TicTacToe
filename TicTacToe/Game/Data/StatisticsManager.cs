using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TicTacToe.Game.Data
{
    internal class StatisticsManager
    {
        public StatisticsManager() { }

        public Statistic LoadPlayersStatistics(int playerID)
        {
            XDocument statisticsData = XDocument.Load("assets/data/Statistics.xml");

            int total = (from match in statisticsData.Descendants("match")
                         where int.Parse(match.Element("player1").Value) == playerID || int.Parse(match.Element("player2").Value) == playerID
                         select match).Count();

            int won = (from match in statisticsData.Descendants("match")
                       where (int.Parse(match.Element("player1").Value) == playerID || int.Parse(match.Element("player2").Value) == playerID) && int.Parse(match.Element("result").Value) == playerID
                       select match).Count();

            int lost = (from match in statisticsData.Descendants("match")
                        where (int.Parse(match.Element("player1").Value) == playerID || int.Parse(match.Element("player2").Value) == playerID) && int.Parse(match.Element("result").Value) != playerID && int.Parse(match.Element("result").Value) != 0
                        select match).Count();

            int draws = (from match in statisticsData.Descendants("match")
                         where (int.Parse(match.Element("player1").Value) == playerID || int.Parse(match.Element("player2").Value) == playerID) && int.Parse(match.Element("result").Value) == 0
                         select match).Count();

            return new Statistic(total, won, lost, draws);
        }

        public Statistic LoadPlayersStatistics(int firstPlayerID, int secondPlayerID)
        {
            XDocument statisticsData = XDocument.Load("assets/data/Statistics.xml");

            int total = (from match in statisticsData.Descendants("match")
                         where (
                                (int.Parse(match.Element("player1").Value) == firstPlayerID || int.Parse(match.Element("player2").Value) == firstPlayerID)
                                &&
                                (int.Parse(match.Element("player1").Value) == secondPlayerID || int.Parse(match.Element("player2").Value) == secondPlayerID)
                               )
                         select match).Count();

            int won = (from match in statisticsData.Descendants("match")
                       where (
                              (int.Parse(match.Element("player1").Value) == firstPlayerID || int.Parse(match.Element("player2").Value) == firstPlayerID)
                              &&
                              (int.Parse(match.Element("player1").Value) == secondPlayerID || int.Parse(match.Element("player2").Value) == secondPlayerID)
                              &&
                              int.Parse(match.Element("result").Value) == firstPlayerID
                             )
                       select match).Count();

            int lost = (from match in statisticsData.Descendants("match")
                        where (
                               (int.Parse(match.Element("player1").Value) == firstPlayerID || int.Parse(match.Element("player2").Value) == firstPlayerID)
                               &&
                               (int.Parse(match.Element("player1").Value) == secondPlayerID || int.Parse(match.Element("player2").Value) == secondPlayerID)
                               &&
                               int.Parse(match.Element("result").Value) != firstPlayerID
                               &&
                               int.Parse(match.Element("result").Value) != 0
                              )
                        select match).Count();

            int draws = (from match in statisticsData.Descendants("match")
                         where (
                                (int.Parse(match.Element("player1").Value) == firstPlayerID || int.Parse(match.Element("player2").Value) == firstPlayerID)
                                &&
                                (int.Parse(match.Element("player1").Value) == secondPlayerID || int.Parse(match.Element("player2").Value) == secondPlayerID)
                                &&
                                int.Parse(match.Element("result").Value) == 0
                               )
                         select match).Count();

            return new Statistic(total, won, lost, draws);
        }

        public void AddAndSaveStatistic(int firstPlayerId, int secondPlayerId, int result)
        {
            XmlDocument statisticsData = new XmlDocument();

            statisticsData.Load("assets/data/Statistics.xml");

            XmlElement newMatch = statisticsData.CreateElement("match");

            XmlElement newPlayer1 = statisticsData.CreateElement("player1");
            newPlayer1.InnerText = firstPlayerId.ToString();
            newMatch.AppendChild(newPlayer1);

            XmlElement newPlayer2 = statisticsData.CreateElement("player2");
            newPlayer2.InnerText = secondPlayerId.ToString();
            newMatch.AppendChild(newPlayer2);

            XmlElement resultNode = statisticsData.CreateElement("result");
            resultNode.InnerText = result.ToString();
            newMatch.AppendChild(resultNode);

            statisticsData.DocumentElement.AppendChild(newMatch);

            statisticsData.Save("assets/data/Statistics.xml");
        }
    }
}