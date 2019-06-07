using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class OnlineRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private eSceneList moveScene;
    [SerializeField]
    private Text roomNameText;
    private string roomName;

    public void Start()
    {
        roomName = "DefaultRoomName";

        //PhotonServerSettingsに設定した内容を使用してマスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        //部屋を作成する
        if (!PhotonNetwork.CreateRoom(roomName))
        {
            Debug.Log("部屋の作成に失敗しました");
            return;
        }

        Debug.Log(roomName + "という部屋を作成しました");
    }

    public void JoinRoom()
    {
        //参加ルーム名を取得する
        roomName = roomNameText.text.ToString();

        if (!PhotonNetwork.JoinRoom(roomName))
        {
            Debug.Log("部屋が見つかりませんでした");
            return;
        }

        //Debug.Log(joinRoomName + "という部屋は無かったため作成しました");
        Debug.Log(roomName + "という部屋に参加しました");
    }

    public void RandomJoinRoom()
    {

        PhotonNetwork.JoinRandomRoom();

    }

    /// <summary>
    /// @brief サーバーへ接続成功したときの処理
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("マスターサーバーに接続成功しました");
    }

    /// <summary>
    /// @brief マッチングが成功したらプレイヤーを生成する
    /// </summary>
    public override void OnJoinedRoom()
    {
        //メッセージ処理の実行を一時停止
        PhotonNetwork.IsMessageQueueRunning = false;
        //シーンを移動させる
        SceneManager.LoadScene((int)moveScene);

    }

}
