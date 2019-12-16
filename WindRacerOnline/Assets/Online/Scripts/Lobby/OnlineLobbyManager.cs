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

    public class OnlineLobbyManager : BaseNetworkObject
    {
        private OnlineLobbyUIManager uiManager;

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

        /// <summary>
        /// @brief ランダムな部屋に参加する
        /// </summary>
        public void RandomJoinRoom()
        {

            PhotonNetwork.JoinRandomRoom();

        }

        /// <summary>
        /// @brief 部屋を作成する
        /// </summary>
        public void CreateRoom()
        {

            PhotonNetwork.CreateRoom(CreateRandomRoomID(), CreateRoomOption(MaxPlayerNum, false));

        }

        /// <summary>
        /// @brief 部屋に参加する
        /// </summary>
        public void JoinRoom()
        {
            //参加ルームIDを取得する
            string id = uiManager.RoomIDText.text.ToString();

            //IDが正しくない場合、処理を終了する(InputFieldで数字のみに制限しているので文字数のみで判断)
            if (id.Length < RoomIDLength)
            {
                Debug.LogWarning("IDが正しくありません");
                return;
            }

            //部屋が見つからなかったとき処理を終了する
            if (!PhotonNetwork.JoinRoom(id))
            {
                Debug.LogWarning("部屋が見つかりませんでした");
                return;
            }

        }

        /// <summary>
        /// @brief ランダムなルームIDを生成する
        /// </summary>
        /// <returns>ランダムな部屋ID</returns>
        public string CreateRandomRoomID()
        {
            //使用文字が変わっていいように新しく変数を作成
            string list = StrListNumber;
            char[] id = new char[RoomIDLength];

            for (int index = 0; index < id.Length; index++)
            {
                int rand = Random.Range(0, list.Length - 1);
                id[index] = list[rand];
            }

            //charをstringに変換させる
            string roomID = new string(id);

            Debug.Log("ルームID:" + roomID);

            return roomID;
        }

        #region PhotonCallback

        /// <summary>
        /// @brief ランダムな部屋の参加に失敗したときの処理
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);

            PhotonNetwork.CreateRoom(CreateRandomRoomID(), CreateRoomOption(MaxPlayerNum));

        }

        /// <summary>
        /// @brief 部屋の入室に失敗したときの処理
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);

        }

        /// <summary>
        /// @brief サーバーへ接続成功したときの処理
        /// </summary>
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            uiManager = GetComponent<OnlineLobbyUIManager>();
            uiManager.Init();

        }

        /// <summary>
        /// @brief マッチングが成功したときの処理
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