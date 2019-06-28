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
    private Text roomIDText;
    [SerializeField]
    private Text playerCountText;

    public void Awake()
    {
        //メッセージ処理の実行を再開する
        PhotonNetwork.IsMessageQueueRunning = true;
    }

    // Use this for initialization
    public void Start()
    {
        roomIDText.text = "ルームばんごう : " + PhotonNetwork.CurrentRoom.Name;
        UpdatePlayerCountText();

        Debug.Log("プレイヤー人数 : " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    /// <summary>
    /// @brief プレイヤーがルームに入室した時の処理
    /// </summary>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        
        UpdatePlayerCountText();

        Debug.Log("プレイヤーが入室しました");
    }

    /// <summary>
    /// @brief プレイヤーがルームを退室したときの処理
    /// </summary>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        UpdatePlayerCountText();

        Debug.Log("プレイヤーが退室しました");
    }

    /// <summary>
    /// @brief プレイヤーが現在何人入っているかのテキストを更新する
    /// </summary>
    public void UpdatePlayerCountText()
    {
        playerCountText.text = "にんずう : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

}
