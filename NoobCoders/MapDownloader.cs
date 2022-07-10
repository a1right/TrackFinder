using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobCoders
{
    internal class MapDownloader
    {
        public static string[,] Map { get; set; }
        private string _path = "./map.txt";

        public void GetMapFromFile()
        {
            var mapString = File.ReadAllText(_path);
            var temp =  mapString.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.RemoveEmptyEntries);
            string[,] map = new string[temp.Length, temp[0].Length];
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = temp[i][j].ToString();
                }
            }
            Map = map;
        }

        public string[,] GetMap()
        {
            return Map;
        }

        public void PrintMap()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                Console.WriteLine();
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write(Map[i, j].ToString() + "");
                }
            }
        }
    }
}
