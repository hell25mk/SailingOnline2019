using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using UnityEngine.UI;

public class OnlineRoomManager : MonoBehaviourPunCallbacks
{

    private enum eSceneList : int
    {
        Scene_Menu = 0,
        Scene_Online,
    }

    [SerializeField]
    private eSceneList moveScene;
    [SerializeField]
    private Text joinRoomNameText;
    private string joinRoomName;

    public void Start()
    {
        joinRoomName = "DefaultRoomName";
    }

    public void CreateRoom()
    {
        //PhotonServerSettingsに設定した内容を使用してマスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinRoom()
    {
        //参加ルーム名を取得する
        joinRoomName = joinRoomNameText.text.ToString();
        //PhotonServerSettingsに設定した内容を使用してマスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// @brief サーバーへ接続成功したらルームを探して参加する
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        //"room"という名前のルームに参加（なければ作成）
        PhotonNetwork.JoinOrCreateRoom(joinRoomName, new RoomOptions(), TypedLobby.Default);

        //Debug.Log(joinRoomName + "という部屋は無かったため作成しました");
        Debug.Log(joinRoomName + "という部屋に参加しました");
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
