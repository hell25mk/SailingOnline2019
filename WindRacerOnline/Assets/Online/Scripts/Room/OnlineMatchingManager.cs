/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace Online.InRoom.Matching
{

    public class OnlineMatchingManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private SceneMoveManager sceneManager;
        [SerializeField]
        private Text roomIDText;
        [SerializeField]
        private Text playerCountText;
        [SerializeField]
        private Button gameStartButton;
        [SerializeField]
        private GameObject playerIconPanel;
        [SerializeField]
        private List<Sprite> playerIconSpriteList;
        [SerializeField]
        private const byte canStartPlayerCount = 1;

        public void Awake()
        {

            //Photonに接続していなかった場合は強制的にタイトルへ移動させる
            if (!PhotonNetwork.IsConnected)
            {
                Debug.LogError("Photonに接続していません。タイトルに戻ります");

                sceneManager.SetMoveScene(SceneNameEnum.OnlineTitleScene);
                sceneManager.SceneMove();
                return;
            }

        }

        // Use this for initialization
        public void Start()
        {

            //メッセージ処理の実行を再開する
            PhotonNetwork.IsMessageQueueRunning = true;

            roomIDText.text = "ルームばんごう : " + PhotonNetwork.CurrentRoom.Name;
            UpdateMatchingPlayer();

        }

        /// <summary>
        /// @brief 他プレイヤーがルームに入室した時の処理
        /// </summary>
        /// <param name="newPlayer"></param>
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);

            UpdateMatchingPlayer();

        }

        /// <summary>
        /// @brief プレイヤーがルームを退室したときの処理
        /// </summary>
        /// <param name="otherPlayer"></param>
        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);

            UpdateMatchingPlayer();

        }

        /// <summary>
        /// @brief ゲームルームを抜ける
        /// </summary>
        public void ExitGameRoom()
        {
            if (!PhotonNetwork.InRoom)
            {
                return;
            }

            PhotonNetwork.LeaveRoom();

        }

        /// <summary>
        /// @brief ルームを抜けた場合の処理
        /// </summary>
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();

            //シーンを移動させる
            sceneManager.SetMoveScene(SceneNameEnum.OnlineLobbyScene);
            sceneManager.SceneMove();
        }

        /// <summary>
        /// @brief ルームの情報を更新し、それをGUIに適用させる
        /// </summary>
        public void UpdateMatchingPlayer()
        {

            //パネルの状態を更新する
            for (int count = 0; count < PhotonNetwork.CurrentRoom.MaxPlayers; count++)
            {
                Image image = playerIconPanel.transform.Find("PlayerIcon" + count).transform.Find("IconImage").GetComponent<Image>();
                Text name = playerIconPanel.transform.Find("PlayerIcon" + count).transform.Find("NameText").GetComponent<Text>();

                //プレイヤーが存在する場合は画像の変更とニックネームを設定する
                if (count < PhotonNetwork.CurrentRoom.PlayerCount)
                {
                    image.sprite = playerIconSpriteList[count];
                    name.text = PhotonNetwork.PlayerList[count].NickName;
                }
                else
                {
                    image.sprite = playerIconSpriteList[playerIconSpriteList.Count - 1];
                    name.text = "ぼしゅうちゅう";
                }


            }

            //プレイヤーの人数を確認し一定数以上いる且自身がマスターならばスタートボタンを押せるようにする
            if (PhotonNetwork.CurrentRoom.PlayerCount >= canStartPlayerCount)
            {
                if (PhotonNetwork.IsMasterClient)
                {
                    gameStartButton.interactable = true;
                }
            }
            else
            {
                gameStartButton.interactable = false;
            }

            //人数のテキストを更新する
            playerCountText.text = "にんずう : " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        }

        /// <summary>
        /// @brief ルーム内にいる全員のGameStart関数を呼び出す
        /// </summary>
        public void ReadyToGame()
        {

            if (!PhotonNetwork.InRoom)
            {
                return;
            }

            //これ以降の部屋入室を禁止する
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            photonView.RPC("GameStart", RpcTarget.AllViaServer);

        }

        /// <summary>
        /// @brief ゲームを開始するためシーンを切り替える
        /// </summary>
        [PunRPC]
        private void GameStart()
        {

            PhotonNetwork.IsMessageQueueRunning = false;

            sceneManager.SetMoveScene(SceneNameEnum.OnlineGameScene);
            sceneManager.SceneMove();

        }

    }

}