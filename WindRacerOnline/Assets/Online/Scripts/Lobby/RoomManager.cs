using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Online.Lobby
{
    public class RoomManager : BaseNetworkObject
    {

        [SerializeField]
        private Text inRoomIDText;

        //部屋に入れる最大人数
        private const byte MaxPlayerNum = 8;
        //ルームIDの桁数
        private const byte RoomIDLength = 5;
        //ルームID生成用
        private const string StrListNumber = "0123456789";

        #region ランダムマッチ

        /// <summary>
        /// @brief ランダムな部屋に参加する
        /// </summary>
        public void JoinRandomRoom()
        {

            PhotonNetwork.JoinRandomRoom();

        }

        #endregion

        #region 友達と遊ぶ

        /// <summary>
        /// @brief 新規の部屋を作成する
        /// </summary>
        public void CreateFriendRoom()
        {

            PhotonNetwork.CreateRoom(CreateRandomRoomID(), CreateRoomOption());

        }

        /// <summary>
        /// @brief 部屋に参加する
        /// </summary>
        public void JoinFriendRoom()
        {
            //参加ルームIDを取得する
            string id = inRoomIDText.ToString();

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

        #endregion

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

        /// <summary>
        /// @brief 部屋オプションを作成する
        /// </summary>
        /// /// <param name="mP">部屋に入れる最大人数</param>
        /// <param name="vis">部屋を公開するか</param>
        /// <param name="open">部屋に入れるかどうか</param>
        /// <returns>作成したオプション</returns>
        public RoomOptions CreateRoomOption(byte mP = MaxPlayerNum, bool vis = true, bool open = true)
        {
            RoomOptions option = new RoomOptions
            {
                MaxPlayers = mP,
                IsVisible = vis,
                IsOpen = open
            };

            return option;
        }

        #region PhotonCollBack

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

        #endregion



    }

}
