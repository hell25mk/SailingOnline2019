using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OfflineMenuManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text playerName;

    public void JoinOnlineLobby()
    {
        SetNickName();
        SceneMoveManager sceneMove = GetComponent<SceneMoveManager>();
        sceneMove.SceneMove();
    }

    /// <summary>
    /// @brief ニックネームを登録する
    /// </summary>
    public void SetNickName()
    {
        PhotonNetwork.NickName = playerName.text;
    }

}
