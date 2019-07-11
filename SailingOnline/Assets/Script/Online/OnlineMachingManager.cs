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

    private const byte canStartPlayerCount = 2;

    // Use this for initialization
    public void Start()
    {
        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;

        roomIDText.text = "ルームばんごう : " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerCount();
        AddPlayer();

        Debug.Log("プレイヤー人数 : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        UpdatePlayerCount();

        Debug.Log("プレイヤーが入室しましたよ");

    }

    /// <summary>
    /// @brief 他プレイヤーがルームに入室した時の処理
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        UpdatePlayerCount();

        Debug.Log("プレイヤーが入室しましたwww");
    }

    /// <summary>
    /// @brief プレイヤーがルームを退室したときの処理
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        UpdatePlayerCount();

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
    /// @brief プレイヤーが現在何人入っているかを更新する
    /// </summary>
    public void UpdatePlayerCount()
    {

        if(PhotonNetwork.CurrentRoom.PlayerCount >= canStartPlayerCount)
        {
            gameStartButton.interactable = true;
            Debug.Log("ゲームスタートできます");
        }
        else
        {
            gameStartButton.interactable = false;
            Debug.Log("ゲームスタートできません");
        }

        playerCountText.text = "にんずう : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public void AddPlayer()
    {
        Vector3 vec = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject playerIcon = PhotonNetwork.Instantiate("PlayerIcon", vec, Quaternion.identity);

        //playerIcon.transform.SetParent(playerIconPanel.transform, false);   //子オブジェクトにする

        Debug.Log("プレイヤーアイコンを生成しました");

    }

}
