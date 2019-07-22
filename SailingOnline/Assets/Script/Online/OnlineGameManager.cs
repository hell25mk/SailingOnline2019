using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private SceneMoveManager sceneManager;

    // Use this for initialization
    void Start()
    {

        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;

        //プレイヤーを生成する
        Vector3 vec = new Vector3(0.0f, 0.0f);
        PhotonNetwork.Instantiate("OnlinePlayer", vec, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// @brief ゲームルームを抜ける
    /// </summary>
    public void ExitGameRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// @brief ルームを抜けた場合の処理
    /// </summary>
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();

        //シーンを移動させる
        sceneManager.SetMoveScene(eSceneList.Scene_OnlineMenu);
        sceneManager.SceneMove();
    }

}
