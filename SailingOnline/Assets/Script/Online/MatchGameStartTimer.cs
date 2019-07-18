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
        Debug.Log("カウントダウンしているよ");
        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            photonView.RPC("CountDown", RpcTarget.AllViaServer);
        }

    }

    [PunRPC]
    private void CountDown()
    {
        if (gameStartTime < 0.0f)
        {
            return;
        }

        gameStartTime -= Time.deltaTime;
        gameStartCount = (int)gameStartTime;
        timerText.text = gameStartCount.ToString("D2");

    }

    

}
