using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomButton : MonoBehaviour {

    [SerializeField]
    private GameObject modeSelectUI;
    [SerializeField]
    private GameObject roomInUI;
    [SerializeField]
    private GameObject backOfflineButton;
    [SerializeField]
    private GameObject backButton;
    private SceneMoveManager sceneMove;

    /// <summary>
    /// @brief [部屋に参加]を押したときの処理
    /// </summary>
    public void OnJoinRoomButton()
    {
        modeSelectUI.SetActive(false);
        roomInUI.SetActive(true);
        backOfflineButton.SetActive(false);
        backButton.SetActive(true);
    }

    /// <summary>
    /// @brief [戻る]を押したときの処理
    /// </summary>
    public void OnBackButton()
    {
        modeSelectUI.SetActive(true);
        roomInUI.SetActive(false);
        backOfflineButton.SetActive(true);
        backButton.SetActive(false);
    }

}
