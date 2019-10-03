using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private SceneMoveManager sceneManager;
    [SerializeField]
    private GameObject shipSpawnPoint;

    private const string playerPrefabName = "OnlinePlayer";
    //各船の出現位置
    /*private Vector3[] spawnPoint =
    {
        new Vector3(-20.0f,0.0f,0.0f),
        new Vector3(-15.0f,0.0f,0.0f),
        new Vector3(-10.0f,0.0f,0.0f),
        new Vector3(-5.0f,0.0f,0.0f),
        new Vector3(5.0f,0.0f,0.0f),
        new Vector3(10.0f,0.0f,0.0f),
        new Vector3(15.0f,0.0f,0.0f),
        new Vector3(20.0f,0.0f,0.0f),
    };*/

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

        //プレイヤーを生成する
        Vector3 spawnVector = new Vector3(Random.Range(-5.0f, 5.0f), 0.0f, Random.Range(-5.0f, 5.0f)) + shipSpawnPoint.transform.localPosition;
        PhotonNetwork.Instantiate(playerPrefabName, spawnVector, Quaternion.identity);

        //PhotonNetwork.Instantiate(playerPrefabName, spawnPoint[0], Quaternion.identity);

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
