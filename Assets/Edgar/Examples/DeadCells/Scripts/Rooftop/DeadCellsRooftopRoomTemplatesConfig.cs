using System;
using UnityEngine;

namespace Edgar.Unity.Examples.DeadCells
{
    [Serializable]
    public class DeadCellsRooftopRoomTemplatesConfig
    {
        public GameObject[] EntranceRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] OutsideNormalRoomTemplates;

        public GameObject[] OutsideTeleportRoomTemplates;

        public GameObject[] InsideTreasureRoomTemplates;

        public GameObject[] InsideTeleportRoomTemplates;

        public GameObject[] InsideNormalRoomTemplates;

        public GameObject[] InsideShopRoomTemplates;

        public GameObject[] InsideCorridorRoomTemplates;

        public GameObject[] GetRoomTemplates(DeadCellsRoom room)
        {
            if (room.Outside)
            {
                switch (room.Type)
                {
                    case DeadCellsRoomType.Entrance:
                        return EntranceRoomTemplates;

                    case DeadCellsRoomType.Exit:
                        return ExitRoomTemplates;

                    case DeadCellsRoomType.Teleport:
                        return OutsideTeleportRoomTemplates;

                    case DeadCellsRoomType.Normal:
                        return OutsideNormalRoomTemplates;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {                
                switch (room.Type)
                {
                    case DeadCellsRoomType.Teleport:
                        return InsideTeleportRoomTemplates;

                    case DeadCellsRoomType.Treasure:
                        return InsideTreasureRoomTemplates;

                    case DeadCellsRoomType.Shop:
                        return InsideShopRoomTemplates;

                    case DeadCellsRoomType.Normal:
                        return InsideNormalRoomTemplates;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}