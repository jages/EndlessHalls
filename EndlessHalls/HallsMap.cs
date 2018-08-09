using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ShadowedHalls
{
    public class HallsMap
    {
        private MapTile[,] tiles;

        public HallsMap()
        {
            tiles = MapTileFactory.createTiles(4);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
                for (int j = 0; j < tiles.GetLength(0); j++) //Vertical
                {
                    for (int k = 0; k < tiles.GetLength(1); k++) //Horizontal
                    {
                        sb.Append(tiles[j, k]);
                        sb.Append(" ");
                    }

                    sb.Append('\n');
                }

            return sb.ToString();
        }

        public IteratableTile getStart()
        {
            MapTile start = null;
            foreach (MapTile tile in tiles)
            {
                if (tile.Type == MapTile.MapTileType.Start)
                    start = tile;
            }

            if (start != null)
            {
                return new IteratableTile(start, this, null);
            }
            else
            {
                return null;
            }
        }

        public IteratableTile GeTileByNeighbour(IteratableTile parent, MapTile.MapTileExits direction)
        {
            MapTile tile = parent.tile;
            MapTile nextTile = null;
            for (int j = 0; j < tiles.GetLength(0); j++) //Vertical
            {
                for (int k = 0; k < tiles.GetLength(1); k++) //Horizontal
                {
                    if (tiles[j, k] == tile)
                    {
                        MapTile north = (j == 0) ? tiles[tiles.GetLength(0)-1, k] : tiles[j - 1, k];
                        MapTile east = (k == tiles.GetLength(1)-1) ? tiles[j, 0] : tiles[j, k + 1];
                        MapTile west = (k == 0) ? tiles[j, tiles.GetLength(1)-1] : tiles[j, k - 1];
                        MapTile south = (j == tiles.GetLength(0) - 1) ? tiles[0, k] : tiles[j + 1, k];

                        switch (direction)
                        {
                                case MapTile.MapTileExits.East:
                                    nextTile = east;
                                    break;
                                case MapTile.MapTileExits.North:
                                    nextTile = north;
                                    break;
                                case MapTile.MapTileExits.South:
                                    nextTile = south;
                                    break;
                                default: //West
                                    nextTile = west;
                                    break;
                        }
                        
                    }
                }
            }
            return new IteratableTile(nextTile, this, tile);
        }
    }
}