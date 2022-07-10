using System.Text;

namespace NoobCoders
{
    
    internal class Program
    {
        static void Main(string[] args)
        { 
            MapDownloader mapDownloader = new MapDownloader();
            mapDownloader.GetMapFromFile();
            var searcher = new WaySearcher(mapDownloader.GetMap());
            searcher.SearchWay();
            
            Console.ReadLine();
        }
        
            
    }

    
}