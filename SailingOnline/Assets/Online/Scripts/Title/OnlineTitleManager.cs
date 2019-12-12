/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Online.Title
{

    public class OnlineTitleManager : MonoBehaviour
    {

        [SerializeField]
        private Text playerName;
        private const string DefaultPlayerName = "プレイヤー";
        SceneMoveManager sceneMove;

        public void Start()
        {
            sceneMove = GetComponent<SceneMoveManager>();
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
            //名前が付けられていない場合はデフォルト値を設定する
            if (playerName.text == "")
            {
                PhotonNetwork.LocalPlayer.NickName = DefaultPlayerName;
                return;
            }

            PhotonNetwork.LocalPlayer.NickName = playerName.text;
        }

    }

}