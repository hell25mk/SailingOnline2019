using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Online.InRoom
{

    public class OnlinePlayerFactory : MonoBehaviour
    {

        [SerializeField]
        private GameObject shipSpawnPoint;

        private const string playerPrefabName = "OnlinePlayer";

        public void SpawnPlayer()
        {

            Vector3 start = shipSpawnPoint.transform.localPosition;
            float x = 2.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            //float z = 1.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            Vector3 spawnVector = new Vector3(x + start.x, start.y, start.z);

            PhotonNetwork.Instantiate(playerPrefabName, spawnVector, Quaternion.identity);

        }

    }

}