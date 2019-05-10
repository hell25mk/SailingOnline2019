using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager {

    private void Start() {

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public void StartupHost() {

        SetPort();
        NetworkManager.singleton.StartHost();

    }

    public void JoinGame() {

        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();

    }

    private void SetIPAddress() {

        string ipAd = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAd;

    }

    private void SetPort() {

        //NetworkManager.singleton.networkPort = UnityEngine.Random.Range(1000, 10000);
        NetworkManager.singleton.networkPort = 7777;

    }

    /// <summary>
    /// @brief 呼び出されたシーンに伴い処理を行う
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="sceneMode"></param>
    private void OnSceneLoaded(Scene scene,　LoadSceneMode sceneMode) {

        if(scene.buildIndex == 0) {
            SetupMenuSceneButtons();
            return;
        }

        SetupOtherSceneButtons();

    }

    private void SetupMenuSceneButtons() {

        GameObject.Find("StartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("StartHost").GetComponent<Button>().onClick.AddListener(StartupHost);
        
        GameObject.Find("JoinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("JoinGame").GetComponent<Button>().onClick.AddListener(JoinGame);

    }

    private void SetupOtherSceneButtons() {

        GameObject.Find("Disconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("Disconnect").GetComponent<Button>().onClick.AddListener(singleton.StopHost);

    }

}
