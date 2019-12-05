/*
 長嶋
 */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Photon.Pun;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private SceneMoveManager sceneManager;
    [SerializeField]
    private GameObject shipSpawnPoint;

    private const string playerPrefabName = "OnlinePlayer";

    public void Awake()
    {

        //Photonに接続していなかった場合は強制的にタイトルへ移動させる
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Photonに接続していません。タイトルに戻ります");

            sceneManager.SetMoveScene(eSceneList.Scene_OfflineMenu);
            sceneManager.SceneMove();
            return;
        }

    }

    // Use this for initialization
    void Start()
    {

        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;

        SpawnPlayer();

    }

    /// <summary>
    /// @brief プレイヤーを生成する
    /// </summary>
    public void SpawnPlayer()
    {

        Vector3 start = shipSpawnPoint.transform.localPosition;
        float x = 2.0f * PhotonNetwork.LocalPlayer.ActorNumber;
        float z = 1.0f * PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("ナンバー : " + PhotonNetwork.LocalPlayer.ActorNumber);
        Vector3 spawnVector = new Vector3(x + start.x, start.y, start.z);
        
        PhotonNetwork.Instantiate(playerPrefabName, spawnVector, Quaternion.identity);
        
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
