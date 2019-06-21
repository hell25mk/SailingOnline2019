using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eOnlineMenuUI : byte
{
    Menu_Select = 0,
    Menu_Friend,
    Menu_RoomSearch
}

public class OnlineMenuUIManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> menuUI;

    private int currentMenuIndex;

    public void Start()
    {
        currentMenuIndex = 0;
    }

    /// <summary>
    /// @broef 次のUIに切り替える
    /// </summary>
    public void OnNextUI()
    {
        if (currentMenuIndex == (int)eOnlineMenuUI.Menu_RoomSearch)
        {
            return;
        }

        MenuActiveReset();

        currentMenuIndex++;

        menuUI[currentMenuIndex].SetActive(true);
    }

    /// <summary>
    /// @broef 前のUIに切り替える
    /// </summary>
    public void OnBackUI()
    {
        if(currentMenuIndex == (int)eOnlineMenuUI.Menu_Select)
        {
            return;
        }

        MenuActiveReset();

        currentMenuIndex--;

        menuUI[currentMenuIndex].SetActive(true);
    }

    /// <summary>
    /// @brief 登録している全てのUIを非表示にする
    /// </summary>
    protected void MenuActiveReset() {
        foreach (GameObject obj in menuUI)
        {
            obj.SetActive(false);
        }
    }

}
