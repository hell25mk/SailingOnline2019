using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneManager : MonoBehaviour {

    private enum eSceneList : int
    {
        Scene_Menu = 0,
        Scene_Online,
    }

    [SerializeField]
    private eSceneList moveScene;

    public void SceneMove()
    {
        SceneManager.LoadScene((int)moveScene);
    }

}
