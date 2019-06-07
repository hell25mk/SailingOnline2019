using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour {

    [SerializeField]
    private eSceneList moveScene;

	public void MoveScene()
    {
        SceneManager.LoadScene((int)moveScene);
    }

}
