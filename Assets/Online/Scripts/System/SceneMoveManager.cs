using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour
{

    [SerializeField]
    private SceneNameEnum moveScene;

    public void SceneMove()
    {

        SceneManager.LoadScene((int)moveScene);

    }

    public void SceneMove(SceneNameEnum scene)
    {

        SceneManager.LoadScene((int)scene);

    }

    public void SetMoveScene(SceneNameEnum scene)
    {

        moveScene = scene;

    }

}