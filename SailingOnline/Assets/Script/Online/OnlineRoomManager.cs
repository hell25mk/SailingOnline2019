/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class OnlineRoomManager : MonoBehaviourPunCallbacks
{
    private OnlineMenuUIManager uiManager;

    //部屋に入れる最大人数
    private const byte MaxPlayerNum = 8;
    //ルームIDの桁数
    private const byte RoomIDLength = 5;
    //ルームID生成用
    private const string StrListNumber = "0123456789";

    public void Awake()
    {
        PhotonNetwork.GameVersion = "1.0";
        //PhotonServerSettingsに設定した内容を使用してマスターサーバーに接続
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// @brief ランダムな部屋に参加する
    /// </summary>
    public void RandomJoinRoom()
    {
        if (PhotonNetwork.JoinRandomRoom())
        {
            Debug.Log("ランダムな部屋に入室しました");
        }
    }

    /// <summary>
    /// @brief ランダムな部屋の参加に失敗したときの処理
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        PhotonNetwork.CreateRoom(CreateRandomRoomID(), CreateRoomOption(MaxPlayerNum));

        Debug.Log("ランダムルーム入室に失敗 ルームを作成します");
    }

    /// <summary>
    /// @brief 部屋を作成する
    /// </summary>
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRandomRoomID(), CreateRoomOption(MaxPlayerNum, false));

        Debug.Log("部屋を作成しました");
    }

    /// <summary>
    /// @brief 部屋に参加する
    /// </summary>
    public void JoinRoom()
    {
        //参加ルームIDを取得する
        string id = uiManager.RoomIDText.text.ToString();

        if (!PhotonNetwork.JoinRoom(id))
        {
            Debug.Log("部屋が見つかりませんでした");
            return;
        }

        Debug.Log("ルーム" + id + "に参加しました");
    }

    /// <summary>
    /// @brief 部屋の入室に失敗したときの処理
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        Debug.Log("部屋の入室に失敗しました");
    }

    /// <summary>
    /// @brief サーバーへ接続成功したときの処理
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        uiManager = GetComponent<OnlineMenuUIManager>();
        uiManager.Init();

        Debug.Log("マスターサーバーに接続成功しました");
        Debug.Log("プレイヤーネームは" + PhotonNetwork.LocalPlayer.NickName);
    }

    /// <summary>
    /// @brief マッチングが成功したときの処理
    /// </summary>
    public override void OnJoinedRoom()
    {
        //メッセージ処理の実行を一時停止
        PhotonNetwork.IsMessageQueueRunning = false;

        //シーンを移動させる
        SceneMoveManager sceneMove = GetComponent<SceneMoveManager>();
        sceneMove.SceneMove();
    }

    /// <summary>
    /// @brief ランダムなルームIDを生成する
    /// </summary>
    /// <returns>ランダムな部屋ID</returns>
    public string CreateRandomRoomID()
    {
        //使用文字が変わっていいように新しく変数を作成
        string list = StrListNumber;
        char[] id = new char[RoomIDLength];

        for (int index = 0; index < id.Length; index++)
        {
            int rand = Random.Range(0, list.Length - 1);
            id[index] = list[rand];
            Debug.Log("[" + index + "]" + id[index]);
        }

        //charをstringに変換させる
        string roomID = new string(id);

        Debug.Log("ルームID:" + roomID);

        return roomID;
    }

    /// <summary>
    /// @brief 部屋オプションを作成する
    /// </summary>
    /// /// <param name="mP">部屋に入れる最大人数</param>
    /// <param name="vis">部屋を公開するか</param>
    /// <param name="open">部屋に入れるかどうか</param>
    /// <returns>作成したオプション</returns>
    public RoomOptions CreateRoomOption(byte mP = MaxPlayerNum, bool vis = true, bool open = true)
    {
        RoomOptions option = new RoomOptions
        {
            MaxPlayers = mP,
            IsVisible = vis,
            IsOpen = open
        };

        return option;
    }

}
