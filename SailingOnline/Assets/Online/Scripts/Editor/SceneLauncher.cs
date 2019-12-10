using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Online.Editor
{

    public class SceneLauncher
    {

        private const string MenuName = "Online/Scenes/";
        private const string SceneDirectoryName = "Assets/Online/Scenes/";

        [MenuItem(MenuName + "TitleScene")]
        public static void OpenTitleScene()
        {

            EditorSceneManager.OpenScene(SceneDirectoryName + "OnlineTitleScene.unity", OpenSceneMode.Single);

        }

        [MenuItem(MenuName + "MenuScene")]
        public static void OpenMenuScene()
        {

            EditorSceneManager.OpenScene(SceneDirectoryName + "Scenes/OnlineMenuScene.unity", OpenSceneMode.Single);

        }

        [MenuItem(MenuName + "MatchingScene")]
        public static void OpenMatchingScene()
        {

            EditorSceneManager.OpenScene(SceneDirectoryName + "OnlineMatchingScene.unity", OpenSceneMode.Single);

        }

        [MenuItem(MenuName + "GameScene")]
        public static void OpenGameScene()
        {

            EditorSceneManager.OpenScene(SceneDirectoryName + "OnlineGameScene.unity", OpenSceneMode.Single);

        }

        private static void OpenSceneFile()
        {
            //最終的に引数をここで受け取ってそれに合わせたファイルを開く予定
            EditorSceneManager.OpenScene(SceneDirectoryName + "OnlineGameScene.unity", OpenSceneMode.Single);

        }

    }

}