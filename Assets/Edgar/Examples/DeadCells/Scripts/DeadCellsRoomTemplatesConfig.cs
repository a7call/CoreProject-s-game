using System;
using UnityEngine;

namespace Edgar.Unity.Examples.DeadCells
{
    [Serializable]
    public class DeadCellsRoomTemplatesConfig
    {
        public GameObject[] DefaultRoomTemplates;

        public GameObject[] ShopRoomTemplates;

        public GameObject[] TeleportRoomTemplates;

        public GameObject[] TreasureRoomTemplates;

        public GameObject[] EntranceRoomTemplates;

        public GameObject[] ExitRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        public GameObject[] GetRoomTemplates(DeadCellsRoom room)
        {
            switch (room.Type)
            {
                case DeadCellsRoomType.Teleport:
                    return TeleportRoomTemplates;

                case DeadCellsRoomType.Treasure:
                    return TreasureRoomTemplates;

                case DeadCellsRoomType.Shop:
                    return ShopRoomTemplates;

                case DeadCellsRoomType.Entrance:
                    return EntranceRoomTemplates;

                case DeadCellsRoomType.Exit:
                    return ExitRoomTemplates;

                default:
                    return DefaultRoomTemplates;
            }
        }
    }
}