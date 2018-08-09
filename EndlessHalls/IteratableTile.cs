using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace ShadowedHalls
{
    public class IteratableTile
    {
        private MapTile parent;
        
        public MapTile tile;
        private HallsMap halls;

        public IteratableTile(MapTile tile, HallsMap halls, MapTile parent)
        {
            this.tile = tile;
            this.halls = halls;
            this.parent = parent;
        }

        public IteratableTile()
        {
        }

        public IEnumerable<String> Path {
            get
            {
                List<MapTile> parents = new List<MapTile>();
                foreach (var c in Traverse(this, parents))
                    yield return c;
            }
        }

        private IEnumerable<String> Traverse(IteratableTile current, List<MapTile> parents)
        {
            
            if (parents.Contains(current.tile))
            {
                yield break;
            }
            parents.Add(current.tile);

            yield return current.tile.ToString();
            foreach (var direction in current.tile.Exits)
            {
                foreach (var v in Traverse(current.halls.GeTileByNeighbour(current, direction), parents))
                {
                    yield return v;
                }
            }
        }
    }
}