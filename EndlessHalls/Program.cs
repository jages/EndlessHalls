using System;

namespace ShadowedHalls
{
    class Program
    {
        static void Main(string[] args)
        {
            HallsMap map = new HallsMap();
            Console.WriteLine(map.ToString());
            Console.WriteLine("Write something to find a Path (type 'exit' to quit)");
            while (Console.ReadLine() != "exit")
            {
                IteratableTile startTile = map.getStart();
                var path = startTile.Path;
                foreach (var tile in path)
                {
                    Console.WriteLine(tile);
                    //todo improve visual outpu
                }
            }
        }
    }
}