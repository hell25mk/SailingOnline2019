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

        //部屋に入れる最大人数
        private const byte MaxPlayerNum = 8;
        //ルームIDの桁数
        private const byte RoomIDLength = 5;
        //ルームID生成用
        private const string StrListNumber = "0123456789";

        public void Awake()
        {

            Connected("1.0.0");

        }

        #region PhotonCallback

        /// <summary>
        /// @brief サーバーへ接続成功したときの処理
        /// </summary>
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            uiManager = GetComponent<LobbyUIChanger>();
            uiManager.Init();

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
        }

        #endregion

    }

}