/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Online.Lobby
{

    public class LobbyManager : BaseNetworkObject
    {

        private LobbyUIChanger uiManager;

        public void Awake()
        {

            Connected("1.0.0");

        }

        #region PhotonCallback

        /// <summary>
        /// @brief サーバーへ接続成功したとき、UIを初期化する
        /// </summary>
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            uiManager = GetComponent<LobbyUIChanger>();
            uiManager.Init();

            Debug.Log("サーバーへの接続に成功しました");

        }

        /// <summary>
        /// @brief 自身が部屋に入ることに成功したらシーンを移動させる
        /// </summary>
        public override void OnJoinedRoom()
        {
            //メッセージ処理の実行を一時停止
            PhotonNetwork.IsMessageQueueRunning = false;

            //シーンを移動させる
            SceneMoveManager sceneMove = GetComponent<SceneMoveManager>();
            sceneMove.SceneMove();

            Debug.Log("部屋の入室に成功しました");

        }

        #endregion

    }

}