/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class OnlineMachingManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private SceneMoveManager sceneManager;
    [SerializeField]
    private Text roomIDText;
    [SerializeField]
    private Text playerCountText;
    [SerializeField]
    private Button gameStartButton;
    [SerializeField]
    private GameObject playerIconPanel;
    [SerializeField]
    private List<Sprite> playerIconSpriteList;

    private const byte canStartPlayerCount = 2;

    // Use this for initialization
    public void Start()
    {
        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;

        roomIDText.text = "ルームばんごう : " + PhotonNetwork.CurrentRoom.Name;
        UpdateMatchingPlayer();
        UpdateMatchingPlayer();

        Debug.Log("プレイヤー人数 : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        UpdateMatchingPlayer();

        Debug.Log("プレイヤーが入室しましたよ");

    }

    /// <summary>
    /// @brief 他プレイヤーがルームに入室した時の処理
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        UpdateMatchingPlayer();

        Debug.Log("プレイヤーが入室しましたwww");
    }

    /// <summary>
    /// @brief プレイヤーがルームを退室したときの処理
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        UpdateMatchingPlayer();

        Debug.Log("プレイヤーが退室しました");
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
        sceneManager.SceneMove();
    }

    /// <summary>
    /// @brief ルームの情報を更新し、それをGUIに適用させる
    /// </summary>
    public void UpdateMatchingPlayer()
    {
        Debug.Log("プレイヤー人数は" + PhotonNetwork.CurrentRoom.PlayerCount);

        //パネルの状態を更新する
        for (int count = 0; count < PhotonNetwork.CurrentRoom.MaxPlayers; count++)
        {
            Image image = playerIconPanel.transform.Find("PlayerIcon" + count).transform.Find("IconImage").GetComponent<Image>();
            Text name = playerIconPanel.transform.Find("PlayerIcon" + count).transform.Find("NameText").GetComponent<Text>();

            //プレイヤーが存在する場合はニックネームを入れる
            if (count < PhotonNetwork.CurrentRoom.PlayerCount)
            {
                image.sprite = playerIconSpriteList[count];
                name.text = PhotonNetwork.PlayerList[count].NickName;
            }
            else
            {
                image.sprite = playerIconSpriteList[playerIconSpriteList.Count - 1];
                name.text = "ぼしゅうちゅう";
            }


        }

        //プレイヤーの人数を確認し一定数以上いる且自身がマスターならばスタートボタンを押せるようにする
        if (PhotonNetwork.CurrentRoom.PlayerCount >= canStartPlayerCount)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                gameStartButton.interactable = true;
            }
            Debug.Log("ゲームスタートできます");
        }
        else
        {
            gameStartButton.interactable = false;
            Debug.Log("ゲームスタートできません");
        }

        //人数のテキストを更新する
        playerCountText.text = "にんずう : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

    }

}
