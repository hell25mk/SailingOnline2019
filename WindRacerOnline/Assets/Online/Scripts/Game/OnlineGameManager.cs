/*
 長嶋
 */

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using ExitGames.Client.Photon;

namespace Online.InRoom.GamePlay
{

    using CustomOption;

    public class OnlineGameManager : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private SceneMoveManager sceneManager;
        [SerializeField]
        private GameObject shipSpawnPoint;
        [SerializeField]
        private Text waitingText;

        private const string playerPrefabName = "OnlinePlayer";

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

            //メッセージ処理の実行を再開する
            PhotonNetwork.IsMessageQueueRunning = true;

        }

        public override void OnEnable()
        {
            base.OnEnable();

            CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;

        }


        // Use this for initialization
        void Start()
        {

            SpawnPlayer();

            waitingText.text = "他の人を待っています";

            Hashtable props = new Hashtable { { CustomPropertyKey.PlayerLoadLevel, true } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(props);

        }

        public override void OnDisable()
        {
            base.OnDisable();

            CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
        }

        void Update()
        {



        }
        /// <summary>
        /// @brief プレイヤーを生成する
        /// </summary>
        public void SpawnPlayer()
        {

            Vector3 start = shipSpawnPoint.transform.localPosition;
            float x = 2.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            //float z = 1.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            Vector3 spawnVector = new Vector3(x + start.x, start.y, start.z);

            PhotonNetwork.Instantiate(playerPrefabName, spawnVector, Quaternion.identity);

        }

        /// <summary>
        /// @brief ゲームを開始して、プレイヤーの動作をアクティブ化する
        /// </summary>
        public void StartRace()
        {

            Debug.Log("ゲームがスタートしました");

        }

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

            //シーンを移動させる
            sceneManager.SetMoveScene(SceneNameEnum.OnlineLobbyScene);
            sceneManager.SceneMove();

        }

        /// <summary>
        /// @brief 各プレイヤーのロードレベルプロパティを更新する
        /// </summary>
        /// <param name="targetPlayer"></param>
        /// <param name="changedProps"></param>
        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.ContainsKey(CustomPropertyKey.PlayerLoadLevel))
            {
                CheckEndOfGame();
                return;
            }

            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            if (!changedProps.ContainsKey(CustomPropertyKey.PlayerLoadLevel))
            {
                return;
            }

            if (!CheckAllPlayerLoadedLevel())
            {
                return;
            }

            Hashtable props = new Hashtable { { CountdownTimer.CountdownStartTime, (float)PhotonNetwork.Time } };
            PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        }

        /// <summary>
        /// @brief 各プレイヤーのシーンロード状態を確認する
        /// </summary>
        /// <returns>全プレイヤーが読み込みを終了していた場合true、そうでない場合false</returns>
        private bool CheckAllPlayerLoadedLevel()
        {
            foreach (Player p in PhotonNetwork.PlayerList)
            {
                object playerLoadedLevel;

                if (p.CustomProperties.TryGetValue(CustomPropertyKey.PlayerLoadLevel, out playerLoadedLevel))
                {
                    //読み込みが完了していたら次のプレイヤーの読み込み状態を確認する
                    if ((bool)playerLoadedLevel)
                    {
                        continue;
                    }
                }

                Debug.Log("読み込みは終わっていません");

                return false;
            }

            Debug.Log("読み込みが終わりました");

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnCountdownTimerIsExpired()
        {

            StartRace();

        }

        private IEnumerator EndOfGame()
        {

            float timer = 5.0f;

            while (timer > 0.0f)
            {

                waitingText.text = string.Format("Returning to login screen in {0} seconds.", timer.ToString("n2"));

                yield return new WaitForEndOfFrame();

                timer -= Time.deltaTime;
            }

            PhotonNetwork.LeaveRoom();

        }

        private void CheckEndOfGame()
        {

            if (PhotonNetwork.IsMasterClient)
            {
                StopAllCoroutines();
            }


            //StartCoroutine(EndOfGame());

        }

    }

}