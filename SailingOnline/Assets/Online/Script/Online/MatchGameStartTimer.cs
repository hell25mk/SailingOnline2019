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

    private double gameStartCount;
    private float leftCountTime;
    private const byte canStartPlayerCount = 2;

    private bool isLimitTimeOver;

    // Use this for initialization
    void Start()
    {

        isLimitTimeOver = false;
        matchingManager = matchingManagerObject.GetComponent<OnlineMatchingManager>();

        //自身がマスターの場合、カスタムプロパティにスタートまでの時間をセットする
        if (PhotonNetwork.IsMasterClient)
        {
            //GameStartCountというプロパティに設定
            var properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("GameStartCount", PhotonNetwork.Time);
            PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
        }

        gameStartCount = (double)PhotonNetwork.CurrentRoom.CustomProperties ["GameStartCount"];
    }

    // Update is called once per frame
    void Update()
    {

        if (isLimitTimeOver)
        {
            return;
        }

        //制限時間を過ぎた場合
        if (leftCountTime < 0.0f)
        {
            isLimitTimeOver = true;
            TimeOver();
            return;
        }

        double elapsedTime = PhotonNetwork.Time - gameStartCount;
        leftCountTime = gameStartTime - (float)elapsedTime;
        //小数点を切り上げて数値合わせしている
        timerText.text = Mathf.Ceil(leftCountTime).ToString("00");

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
