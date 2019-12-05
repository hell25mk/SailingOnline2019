using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BackLobby : MonoBehaviour {

    //シーンマネージャー
    [SerializeField]
    private SceneMoveManager sceneMove;

    /// <summary>
    /// ボタンが押されたときの処理
    /// </summary>
    public void OnClick()
    {
        //Debug.Log("ButtonDown");

        // ルームを抜ける
        if (!PhotonNetwork.InRoom)
        {
            return;
        }

        // シーン遷移(ルーム選択画面への移動)
        PhotonNetwork.LeaveRoom();
        sceneMove.SceneMove();
    }

}
