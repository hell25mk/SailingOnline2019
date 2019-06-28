/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class OfflineMenuManager : MonoBehaviour
{

    [SerializeField]
    private Text playerName;
    private const string DefaultPlayerName = "Player";
    SceneMoveManager sceneMove;

    public void Start()
    {
        sceneMove = GetComponent<SceneMoveManager>();
        //ニックネームにデフォルト値を入れる
        PhotonNetwork.LocalPlayer.NickName = DefaultPlayerName;
    }

    public void JoinOnlineLobby()
    {
        SetNickName();
        
        sceneMove.SceneMove();
    }

    /// <summary>
    /// @brief ニックネームを登録する
    /// </summary>
    public void SetNickName()
    {
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
    }

}
