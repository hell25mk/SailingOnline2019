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

    private int gameStartCount;
    

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStartTime < 0.0f)
        {
            return;
        }

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            CountDown();
            photonView.RPC("SetTimerText", RpcTarget.AllViaServer, gameStartTime);
        }

    }

    
    public void CountDown()
    {

        gameStartTime -= Time.deltaTime;

    }

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

}
