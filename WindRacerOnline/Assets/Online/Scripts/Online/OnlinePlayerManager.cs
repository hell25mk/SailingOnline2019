using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerManager : MonoBehaviourPunCallbacks
{
    private const string playerPrefabName = "OnlinePlayer";
    //各船の出現位置
    private Vector3[] spawnPoint =
    {
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
        new Vector3(0.0f,0.0f,0.0f),
    };
    private List<GameObject> playerList;

	// Use this for initialization
	void Start () {

        //プレイヤーを生成する
        //Vector3 spawnPoint = new Vector3(0.0f, 0.0f);
        PhotonNetwork.Instantiate(playerPrefabName, spawnPoint[0], Quaternion.identity);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
