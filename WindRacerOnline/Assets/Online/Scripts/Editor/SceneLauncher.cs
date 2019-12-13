using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Text;

namespace Online.Editor
{

    public static class SceneLauncher
    {

        private const string MenuName = "Online/Scenes/";
        private const string SceneDirectoryName = "Assets/Online/Scenes/";

        /// <summary>
        /// @brief 開いているシーンを保存してシーンを移動する
        /// </summary>
        /// <param name="str">移動したいシーンの文字列</param>
        private static void Open(string str)
        {

            EditorSceneManager.SaveOpenScenes();
            EditorSceneManager.OpenScene(CreateSceneDirectoryName(str), OpenSceneMode.Single);

        }

        /// <summary>
        /// @brief 開くシーンファイルのパスを作成し返す
        /// </summary>
        /// <param name="sceneName">開きたいシーンの名前</param>
        /// <returns>ファイルパス</returns>
        private static string CreateSceneDirectoryName(string sceneName)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(SceneDirectoryName).Append(sceneName).Append(".unity");

            return builder.ToString();
        }

        #region プルダウンメニュー一覧

        /// <summary>
        /// @brief TitleSceneを押された際、タイトルシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "TitleScene")]
        public static void OpenTitleScene()
        {

            Open(SceneNameString.OnlineTitleScene);

        }

        /// <summary>
        /// @brief MenuSceneを押された際、メニューシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "LobbyScene")]
        public static void OpenMenuScene()
        {

            Open(SceneNameString.OnlineLobbyScene);

        }

        /// <summary>
        /// @brief MenuSceneを押された際、マッチングシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "MatchingScene")]
        public static void OpenMatchingScene()
        {

            Open(SceneNameString.OnlineMatchingScene);

        }

        /// <summary>
        /// @brief GameSceneを押された際、ゲームシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "GameScene")]
        public static void OpenGameScene()
        {

            Open(SceneNameString.OnlineGameScene);

        }

        #endregion

    }

}