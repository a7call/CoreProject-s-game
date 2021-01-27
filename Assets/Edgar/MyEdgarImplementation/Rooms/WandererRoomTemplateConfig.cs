using UnityEngine;
using System;


namespace Edgar.Unity
{
    [Serializable]
    public class WandererRoomTemplateConfig
    {
        public GameObject[] BasicRoomTemplates;

        public GameObject[] BossRoomTemplates;

        public GameObject[] SpawnRoomTemplates;

        public GameObject[] HubRoomTemplates;

        public GameObject[] RewardRoomTemplates;

        public GameObject[] ShopRoomTemplates;

        public GameObject[] SecretRoomTemplates;

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

                case RoomType.Hub:
                    return HubRoomTemplates;

                case RoomType.Spawn:
                    return SpawnRoomTemplates;

                case RoomType.Normal:
                    return BasicRoomTemplates;

                default:
                    return BasicRoomTemplates;
            }
        }
    }
}

