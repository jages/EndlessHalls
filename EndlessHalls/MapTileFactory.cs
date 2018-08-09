using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowedHalls
{
    public static class MapTileFactory
    {
        public static MapTile[,] createTiles(int GridSize)
        {
            if (GridSize == 1)
            {
                return new MapTile[,]{ { new MapTile() { Type = MapTile.MapTileType.Start, Exits = MapTileFactory.GenerateFourExits}} };
            }
            else
            {

                MapTile[,] map = gererateEmptyMap(GridSize);
                
                GenerateRandomTile(GridSize, map, MapTile.MapTileType.Start);
                GenerateRandomTile(GridSize, map, MapTile.MapTileType.Key);
                GenerateRandomTile(GridSize, map, MapTile.MapTileType.Exit);

                for (int j = 0; j < map.GetLength(0); j++) //Vertical
                {
                    for (int k = 0; k < map.GetLength(1); k++) //Horizontal
                    {
                        if (map[j, k].Type == MapTile.MapTileType.Empty)
                        {
                            GenerateTile(GridSize, map, MapTile.MapTileType.Regular, j, k);
                        }
                    }
                }


                return map;
            }

        }

        private static MapTile[,] gererateEmptyMap(int GridSize)
        {
            MapTile[,] map = new MapTile[GridSize, GridSize];

            for (int j = 0; j < map.GetLength(0); j++) //Vertical
            {
                for (int k = 0; k < map.GetLength(1); k++) //Horizontal
                {
                    map[j, k] = new MapTile();
                }
            }
            
            return map;
        }

        private static void GenerateRandomTile(int GridSize, MapTile[,] map, MapTile.MapTileType TileType)
        {
            Random rng = new Random();
            int i = (int) (GridSize * rng.NextDouble());
            int j = (int) (GridSize * rng.NextDouble());

            
            while ((map[i, j].Type != MapTile.MapTileType.Empty) && (map[i, j].Type != MapTile.MapTileType.Regular))
            {
                i = (int) (GridSize * rng.NextDouble());
                j = (int) (GridSize * rng.NextDouble());
            }

            GenerateTile(GridSize, map, TileType, i, j);
        }

        private static void GenerateTile(int GridSize, MapTile[,] map, MapTile.MapTileType TileType, int i, int j)
        {
            map[i, j] = new MapTile {Type = TileType, Exits = generateExits(map, i, j, GridSize)};
        }

        private static MapTile.MapTileExits[] GenerateFourExits => new[] { MapTile.MapTileExits.East, MapTile.MapTileExits.South, MapTile.MapTileExits.West, MapTile.MapTileExits.North};

        private static MapTile.MapTileExits[] generateExits(MapTile[,] map, int i, int j, int GridSize)
        {
            MapTile north = (i == 0) ? map[GridSize-1, j] : map[i - 1, j];
            MapTile east = (j == GridSize - 1) ? map[i, 0] : map[i, j + 1];
            MapTile west = (j == 0) ? map[i, GridSize-1] : map[i, j - 1];
            MapTile south = (i == GridSize - 1) ? map[0, j] : map[i + 1, j];
            
            List<MapTile.MapTileExits> exits = new List<MapTile.MapTileExits>();
            List<MapTile.MapTileExits> excludedExits = new List<MapTile.MapTileExits>();

            
            if (east.Type != MapTile.MapTileType.Empty){ 
                if(east.Exits.Contains(MapTile.MapTileExits.West))
                    exits.Add(MapTile.MapTileExits.East);
                else
                    excludedExits.Add(MapTile.MapTileExits.East);
            }
            if (south.Type != MapTile.MapTileType.Empty){ 
                if(south.Exits.Contains(MapTile.MapTileExits.North))
                    exits.Add(MapTile.MapTileExits.South);
                else
                    excludedExits.Add(MapTile.MapTileExits.South);
            }
            if (west.Type != MapTile.MapTileType.Empty){ 
                if(west.Exits.Contains(MapTile.MapTileExits.East))
                    exits.Add(MapTile.MapTileExits.West);
                else
                    excludedExits.Add(MapTile.MapTileExits.West);
            }
            if (north.Type != MapTile.MapTileType.Empty){ 
                if(north.Exits.Contains(MapTile.MapTileExits.South))
                    exits.Add(MapTile.MapTileExits.North);
                else
                    excludedExits.Add(MapTile.MapTileExits.North);
            }

            
            exits.AddRange(randomExits(excludedExits.ToArray()));

            return exits.Distinct().ToArray();

        }

        private static MapTile.MapTileExits[] randomExits(MapTile.MapTileExits[] excluded)
        {
            List<MapTile.MapTileExits> exits = new List<MapTile.MapTileExits>();
            Random rng = new Random();
            if(!excluded.Contains(MapTile.MapTileExits.South) && (rng.Next(2) == 1))
                exits.Add(MapTile.MapTileExits.South);
            if(!excluded.Contains(MapTile.MapTileExits.East) && (rng.Next(2) == 1))
                exits.Add(MapTile.MapTileExits.East);
            if(!excluded.Contains(MapTile.MapTileExits.West) && (rng.Next(2) == 1))
                exits.Add(MapTile.MapTileExits.West);
            if(!excluded.Contains(MapTile.MapTileExits.North) && (rng.Next(2) == 1))
                exits.Add(MapTile.MapTileExits.North);

            return exits.ToArray();
        }

        private static MapTile.MapTileExits EntranceToExit(MapTile.MapTileExits exit)
        {
            MapTile.MapTileExits entrance;
            switch (exit)
            {
                case MapTile.MapTileExits.East:
                    entrance = MapTile.MapTileExits.West;
                    break;
                case MapTile.MapTileExits.South:
                    entrance = MapTile.MapTileExits.North;
                    break;
                case MapTile.MapTileExits.West:
                    entrance = MapTile.MapTileExits.East;
                    break;
                default: //MapTile.MapTileExits.North
                    entrance = MapTile.MapTileExits.South;
                    break;
            }

            return entrance;
        }
    }
}