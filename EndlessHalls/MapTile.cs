using System;
using System.Linq;

namespace ShadowedHalls
{
    public class MapTile
    {
        public enum MapTileExits
        {
            East,
            South,
            West,
            North
        }

        public enum MapTileType
        {
            Regular,
            Start,
            Key,
            Exit,
            Empty
        }

        public MapTileExits[] Exits;

        public MapTileType Type;

        public MapTile()
        {
            Type = MapTileType.Empty;
            Exits = new MapTileExits[0];
        }


        public override string ToString()
        {
            String result = "";
            switch (Type)
            {
                case MapTileType.Empty:
                    result = "▒";
                    break;
                case MapTileType.Start:
                    result = "S";
                    break;
                case MapTileType.Key:
                    result = "K";
                    break;
                case MapTileType.Exit:
                    result = "E";
                    break;
                default: //MapTileType.Regular
                    switch (Exits.Length)
                    {
                        case 1:
                            /*switch (Exits.First())
                            {
                                case MapTileExits.East:
                                    result = "╶";
                                    break;
                                case MapTileExits.South:
                                    result = "╷";
                                    break;
                                case MapTileExits.West:
                                    result = "╴";
                                    break;
                                case MapTileExits.North:
                                    result = "╵";
                                    break;
                            }*/
                            result = "░";

                            break;
                        case 2:
                            if (Exits.Contains(MapTileExits.East) & Exits.Contains(MapTileExits.South))
                            {
                                result = "┌";
                            }
                            else if (Exits.Contains(MapTileExits.East) & Exits.Contains(MapTileExits.West))
                            {
                                result = "─";
                            }
                            else if (Exits.Contains(MapTileExits.East) & Exits.Contains(MapTileExits.North))
                            {
                                result = "└";
                            }
                            else if (Exits.Contains(MapTileExits.West) & Exits.Contains(MapTileExits.South))
                            {
                                result = "┐";
                            }
                            else if (Exits.Contains(MapTileExits.West) & Exits.Contains(MapTileExits.North))
                            {
                                result = "┘";
                            }
                            else if (Exits.Contains(MapTileExits.South) & Exits.Contains(MapTileExits.North))
                            {
                                result = "│";
                            }

                            break;

                        case 3:
                            if (!Exits.Contains(MapTileExits.East))
                            {
                                result = "┤";
                            }
                            else if (!Exits.Contains(MapTileExits.South))
                            {
                                result = "┴";
                            }
                            else if (!Exits.Contains(MapTileExits.West))
                            {
                                result = "├";
                            }
                            else if (!Exits.Contains(MapTileExits.North))
                            {
                                result = "┬";
                            }

                            break;

                        default: //4 Exits
                            result = "┼";
                            break;
                    }

                    break;
            }


            return result; // invalid tile is displayed as null
        }
    }
}