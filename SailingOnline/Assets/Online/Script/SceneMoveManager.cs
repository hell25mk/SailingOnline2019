using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneList : int
{
    Scene_OfflineMenu = 0,
    Scene_OnlineMenu,
    Scene_OnlineMatching,
    Scene_OnlineGame,
}

public class SceneMoveManager : MonoBehaviour {

    [SerializeField]
    private eSceneList moveScene;

	public void SceneMove()
    {
        SceneManager.LoadScene((int)moveScene);
    }

    public void SetMoveScene(eSceneList scene)
    {
        moveScene = scene;
    }

}
