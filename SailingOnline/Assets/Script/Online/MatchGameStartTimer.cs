/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MatchGameStartTimer : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private float gameStartTime;

    [SerializeField]
    private GameObject matchingManagerObject;
    private OnlineMatchingManager matchingManager;

    private int gameStartCount;
    private const byte canStartPlayerCount = 2;

    private bool isLimitTimeOver;

    // Use this for initialization
    void Start()
    {

        isLimitTimeOver = false;
        matchingManager = matchingManagerObject.GetComponent<OnlineMatchingManager>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isLimitTimeOver)
        {
            return;
        }

        //制限時間を過ぎた場合
        if (gameStartTime < 0.0f)
        {
            isLimitTimeOver = true;
            TimeOver();
            return;
        }

        //ルームマスターならカウントダウンを進める
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            gameStartTime -= Time.deltaTime;

            if (PhotonNetwork.InRoom)
            {
                photonView.RPC("SetTimerText", RpcTarget.AllViaServer, gameStartTime);
            }
        }

    }

    /// <summary>
    /// @brief 現在の制限時間を全員のテキストに表示する
    /// </summary>
    /// <param name="count">経過後の時間</param>
    [PunRPC]
    private void SetTimerText(float count)
    {
        //自身がマスター出ない場合、カウントダウン変数を同期する
        if (!PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            gameStartTime = count;
        }

        gameStartCount = (int)gameStartTime;
        timerText.text = gameStartCount.ToString("D2");
    }

    /// <summary>
    /// @brief 制限時間を過ぎた後の処理
    /// </summary>
    public void TimeOver()
    {

        //人数が一定数以上いる場合、ゲームスタートするいない場合はルームから退室する
        if (PhotonNetwork.CurrentRoom.PlayerCount >= canStartPlayerCount)
        {
            matchingManager.ReadyToGame();
        }
        else
        {
            matchingManager.ExitGameRoom();
        }
        
    }

}
