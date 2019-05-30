using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour {

    [SerializeField]
    private eSceneList moveScene;

    private enum eSceneList : int
    {
        Scene_Menu = 0,
        Scene_Online,
    }

	public void MoveScene()
    {
        SceneManager.LoadScene((int)moveScene);
    }

}
