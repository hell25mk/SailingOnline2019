using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class OnlineSceneManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private eSceneList moveScene;   //シーンの移動先

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

        MoveScene();
    }

    /// <summary>
    /// @brief シーンの切り替え
    /// </summary>
    public void MoveScene()
    {
        SceneManager.LoadScene((int)moveScene);
    }

}
