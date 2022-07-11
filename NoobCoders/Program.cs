using System.Text;

namespace NoobCoders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MapDownloader mapDownloader = new MapDownloader();
            mapDownloader.GetMapFromFile();
            mapDownloader.PrintMap();
            var searcher = new WaySearcher(mapDownloader.Map);
            searcher.SearchWay();

            Console.ReadLine();
        }
    }
}