using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinRoomButton : MonoBehaviour
{

    [SerializeField]
    private GameObject mainMenuUI;
    [SerializeField]
    private GameObject subMenuUI;

    /// <summary>
    /// @brief [部屋に参加]を押したとき、サブメニューを表示する
    /// </summary>
    public void OnJoinRoomButton()
    {
        mainMenuUI.SetActive(false);
        subMenuUI.SetActive(true);
    }

    /// <summary>
    /// @brief [戻る]を押したとき、メインメニューを表示する
    /// </summary>
    public void OnBackButton()
    {
        mainMenuUI.SetActive(true);
        subMenuUI.SetActive(false);
    }

}
