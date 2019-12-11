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

            EditorSceneManager.OpenScene(CreateSceneDirectoryName(SceneNameList.OnlineTitleScene), OpenSceneMode.Single);

        }

        /// <summary>
        /// @brief MenuSceneを押された際、メニューシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "MenuScene")]
        public static void OpenMenuScene()
        {

            EditorSceneManager.OpenScene(CreateSceneDirectoryName(SceneNameList.OnlineMenuScene), OpenSceneMode.Single);

        }

        /// <summary>
        /// @brief MenuSceneを押された際、マッチングシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "MenuScene")]
        public static void OpenMatchingScene()
        {

            EditorSceneManager.OpenScene(CreateSceneDirectoryName(SceneNameList.OnlineMatchingScene), OpenSceneMode.Single);

        }

        /// <summary>
        /// @brief GameSceneを押された際、ゲームシーンへ移動する
        /// </summary>
        [MenuItem(MenuName + "GameScene")]
        public static void OpenGameScene()
        {

            EditorSceneManager.OpenScene(CreateSceneDirectoryName(SceneNameList.OnlineGameScene), OpenSceneMode.Single);

        }

        #endregion

    }

}