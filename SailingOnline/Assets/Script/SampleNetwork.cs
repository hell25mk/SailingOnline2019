using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SampleNetwork : MonoBehaviourPunCallbacks {

	// Use this for initialization
	private void Start () {
        //PhotonServerSettingsに設定した内容を使用してマスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        //"room"という名前のルームに参加（なければ作成）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        var v = new Vector3(Random.Range(-10.0f, 10.0f), 0.0f);
        PhotonNetwork.Instantiate("GamePlayer", v, Quaternion.identity);
    }

}
