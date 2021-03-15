using UnityEngine;
using System;


namespace Edgar.Unity
{
    [Serializable]
    public class WandererRoomTemplateConfig
    {
        public GameObject[] MediumRoomTemplates;

        public GameObject[] BossRoomTemplates;

        public GameObject[] SpawnRoomTemplates;

        public GameObject[] LargeRoomTemplates;

        public GameObject[] SmallRoomTemplates;

        public GameObject[] RewardRoomTemplates;

        public GameObject[] ShopRoomTemplates;

        public GameObject[] SecretRoomTemplates;

        public GameObject[] CorridorRoomTemplates;

        /// <summary>
        /// Get room templates for a given room.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public GameObject[] GetRoomTemplates(WandererRoom room)
        {
            switch (room.Type)
            {
                case RoomType.Boss:
                    return BossRoomTemplates;

                case RoomType.Shop:
                    return ShopRoomTemplates;

                case RoomType.Reward:
                    return RewardRoomTemplates;

                case RoomType.Small:
                    return SmallRoomTemplates;

                case RoomType.Large:
                    return LargeRoomTemplates;

                case RoomType.Spawn:
                    return SpawnRoomTemplates;

                case RoomType.Medium:
                    return MediumRoomTemplates;

                case RoomType.Corridor:
                    return CorridorRoomTemplates;

                default:
                    return LargeRoomTemplates;
            }
        }
    }
}

