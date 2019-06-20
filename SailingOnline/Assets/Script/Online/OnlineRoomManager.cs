using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Photon.Chat;

public class OnlineRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text roomNameText;
    [SerializeField]
    private GameObject menuUI;
    [SerializeField]
    private Text menuText;

    //部屋に入れる最大人数
    private const byte MaxPlayerNum = 8;
    //ルームIDの桁数
    private readonly byte RoomIDLength = 5;
    //ルームID生成用
    private const string StrRoomID = "0123456789";

    public void Start()
    {
        //PhotonServerSettingsに設定した内容を使用してマスターサーバーに接続
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    /// @brief 部屋を作成する
    /// </summary>
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(CreateRandomRoomID());

        Debug.Log("部屋を作成しました");
    }

    /// <summary>
    /// @brief 部屋に参加する
    /// </summary>
    public void JoinRoom()
    {
        //参加ルームIDを取得する
        string id = roomNameText.text.ToString();

        if (!PhotonNetwork.JoinRoom(id))
        {
            Debug.Log("部屋が見つかりませんでした");
            return;
        }

        Debug.Log("ルーム" + id + "に参加しました");
    }

    /// <summary>
    /// @brief ランダムマッチを押したときの処理
    /// </summary>
    public void RandomJoinRoom()
    {
        if (PhotonNetwork.JoinRandomRoom())
        {
            Debug.Log("ランダムな部屋に入室しました");
        }
    }

    /// <summary>
    /// @brief 部屋の参加に失敗したときの処理
    /// </summary>
    /// <param name="returnCode"></param>
    /// <param name="message"></param>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);

        PhotonNetwork.CreateRoom(CreateRandomRoomID());

        Debug.Log("ランダムルーム入室に失敗 ルームを作成します");
    }

    /// <summary>
    /// @brief サーバーへ接続成功したときの処理
    /// </summary>
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        menuUI.SetActive(true);
        menuText.text = "オンラインモード";

        Debug.Log("マスターサーバーに接続成功しました");
        Debug.Log("プレイヤーネームは" + PhotonNetwork.NickName);
    }

    /// <summary>
    /// @brief マッチングが成功したらプレイヤーを生成する
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
        char[] list = new char[RoomIDLength];

        for (int index = 0; index < list.Length; index++)
        {
            int rand = Random.Range(0, 9);
            list[index] = StrRoomID[rand];
            Debug.Log("[" + index + "]" + list[index]);
        }

        //charをstringに変換させる
        string roomID = new string(list);

        Debug.Log("ルームID:" + roomID);

        return roomID;
    }

    /// <summary>
    /// @brief 部屋オプションを作成する
    /// </summary>
    /// <param name="vis">部屋を公開するか</param>
    /// <param name="open">部屋に入れるかどうか</param>
    /// <param name="mP">部屋に入れる最大人数</param>
    /// <returns>作成したオプション</returns>
    public RoomOptions CreateRoomOption(bool vis = true, bool open = true, byte mP = MaxPlayerNum)
    {
        RoomOptions option = new RoomOptions
        {
            IsVisible = vis,
            IsOpen = open,
            MaxPlayers = mP
        };

        return option;
    }

}
