using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GamingLabTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var games = LoadGames();
            Requirement1(games);
            Requirement2(games);
            Requirement3(games);


        }

        public static GameList LoadGames()
        {
            try
            {
                var gameList = new GameList();
                var filePath = "games.json";
                using (var r = new StreamReader(filePath))
                {
                    var json = r.ReadToEnd();
                    gameList = JsonConvert.DeserializeObject<GameList>(json);
                }

                return gameList;
            }
            catch (FileNotFoundException notfound)
            {
                Console.WriteLine(notfound.FileName + "not found");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Game Requirement1(GameList games)
        {
            var game = new Game();

            game = games.Games.OrderByDescending(x => x.ContractValue)
                .ThenBy(x => x.ReceivedDate)
                .ThenBy(x => x.Complexity)
                .ThenBy(x => (x.AcceptedDate - x.ReceivedDate).Days).FirstOrDefault();

            return game;
        }

        public static List<KeyValuePair<string, double>> Requirement2(GameList games)
        {

            List<KeyValuePair<string, double>> avgList = games.Games
                .GroupBy(x => x.Complexity)
                .Select(y => KeyValuePair.Create(y.Key.ToString(), y.Average(u => NumberOfWorkhrs(u.ReceivedDate, u.AcceptedDate)))).ToList();

            return avgList;
        }

        private static int NumberOfWorkhrs(DateTime start, DateTime end)
        {
            var workDays = 0;
            while (start != end)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    workDays++;
                }

                start = start.AddDays(1);
            }

            return workDays * 8;
        }

        public static List<Game> Requirement3(GameList games)
        {
            var totalAmt = 30000;
            var contractValue = 0;

            games.Games = games.Games.OrderBy(x => x.Complexity).ThenByDescending(x => x.ContractValue).ToList();
            var returnList = new List<Game>();


            foreach (var game in games.Games)
            {
                contractValue = (int)(contractValue + game.ContractValue);
                returnList.Add(game);
                if (contractValue >= totalAmt)
                {
                    break;
                }
            }

            return returnList;
        }

    }
}
