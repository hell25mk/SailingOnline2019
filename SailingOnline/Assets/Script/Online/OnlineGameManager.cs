using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{

	// Use this for initialization
	void Start () {
        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
