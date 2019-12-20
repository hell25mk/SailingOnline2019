/*
 * 長嶋
 * 
 * Photonの機能で使いそうなものをまとめたクラス
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Online
{

    public class BaseNetworkObject : MonoBehaviourPunCallbacks
    {

        /// <summary>
        /// @brief PhotonServerに接続する
        /// </summary>
        /// <param name="gameVer">使用するゲームのバージョン</param>
        public void Connected(string gameVer)
        {

            if (PhotonNetwork.IsConnected)
            {
                return;
            }

            PhotonNetwork.GameVersion = gameVer;
            PhotonNetwork.ConnectUsingSettings();

            Debug.Log("PhotonServerに接続しました");

        }

        /// <summary>
        /// @brief PhotonServerから切断する
        /// </summary>
        public void Disconnect()
        {

            if (!PhotonNetwork.IsConnected)
            {
                return;
            }

            PhotonNetwork.Disconnect();

            Debug.Log("PhotonServerから切断しました");

        }

    }

}