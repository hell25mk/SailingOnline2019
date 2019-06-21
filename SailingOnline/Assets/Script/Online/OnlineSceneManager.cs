using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class OnlineSceneManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text roomNameText;
    [SerializeField]
    private Text roomPlayerCountText;
    private byte roomPlayerCount;

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
        SceneMoveManager sceneMove = GetComponent<SceneMoveManager>();
        sceneMove.SceneMove();
    }

    public void SetRoomInfomation()
    {
        //roomNameText.text = PhotonNetwork;
    }

    public void SetPlayerCount(byte pCount)
    {
        roomPlayerCount += pCount;

        roomPlayerCountText.text = roomPlayerCount.ToString();
    }

}
