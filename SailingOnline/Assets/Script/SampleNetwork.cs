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

    //サーバーへ接続成功した際に呼ばれる
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        //"room"という名前のルームに参加（なければ作成）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        Debug.Log("ルームに参加しました");
    }

    //マッチングが成功した際に呼ばれる
    public override void OnJoinedRoom()
    {
        var v = new Vector3(Random.Range(-15.0f, 15.0f), 0.0f, Random.Range(-15.0f, 15.0f));
        PhotonNetwork.Instantiate("GamePlayer", v, Quaternion.identity);
        Debug.Log("プレイヤーを生成しました");
    }

}
