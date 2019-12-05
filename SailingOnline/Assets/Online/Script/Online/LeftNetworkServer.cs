/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LeftNetworkServer : MonoBehaviourPunCallbacks
{
    private SceneMoveManager sceneMove;

    // Use this for initialization
    void Start()
    {
        sceneMove = GetComponent<SceneMoveManager>();
    }

    /// <summary>
    /// @brief サーバーから抜ける処理
    /// </summary>
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();

        Debug.Log("Disconnect");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);

        sceneMove.SetMoveScene(eSceneList.Scene_OfflineMenu);
        sceneMove.SceneMove();

        Debug.Log("Photonサーバーから切断しました");
    }

}
