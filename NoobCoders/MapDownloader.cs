using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoobCoders
{
    internal class MapDownloader
    {
        public string[,] Map { get; private set; }
        private string _path = "./map.txt";

        public void GetMapFromFile()
        {
            var mapString = File.ReadAllText(_path);
            var temp =  mapString.Split(new string[] { "\r\n", "\r", "\n" },StringSplitOptions.RemoveEmptyEntries);
            Map = new string[temp.Length, temp[0].Length];
            for(int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j] = temp[i][j].ToString();
                }
            }
        }

        public void PrintMap()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write(Map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
