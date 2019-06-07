using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class SampleNetwork : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text roomNameText;

    // Use this for initialization
    private void Start()
    {
        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;

        //ランダムな場所にプレイヤーを生成
        var vec = new Vector3(Random.Range(-15.0f, 15.0f), 0.0f, Random.Range(-15.0f, 15.0f));
        PhotonNetwork.Instantiate("Player", vec, Quaternion.identity);

        //ルーム名を追加する
        /*foreach(RoomInfo room in PhotonNetwork.GetCustomRoomList)
        {
            roomNameText.text += room.Name;
        }*/
        

    }

}
