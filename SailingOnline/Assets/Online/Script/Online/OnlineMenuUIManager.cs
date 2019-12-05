/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eOnlineMenuUI : byte
{
    Menu_Select = 0,
    Menu_Friend,
    Menu_RoomSearch
}

public class OnlineMenuUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenuUI;              //MenuをまとめたUI
    [SerializeField]
    private GameObject firstSubMenuUI;          //一番初めに表示されるUI
    [SerializeField]
    private Stack<GameObject> subMenuStack;     //
    
    [SerializeField]
    private Text connectStateText;              //通信状態を表示するテキスト
    [SerializeField]
    private Text roomIDText;                    //ルームIDを表示するテキスト

    public void Start()
    {
        subMenuStack = new Stack<GameObject>();
    }

    /// <summary>
    /// @brief UIの初期化を行う
    /// </summary>
    public void Init()
    {
        mainMenuUI.SetActive(true);
        firstSubMenuUI.SetActive(true);
        subMenuStack.Push(firstSubMenuUI);
        mainMenuUI.SetActive(true);
        connectStateText.text = "オンラインモード";
    }

    /// <summary>
    /// @broef 次のUIに切り替える
    /// </summary>
    public void PuchNextUI(GameObject obj)
    {
        subMenuStack.Peek().SetActive(false);
        subMenuStack.Push(obj);
        subMenuStack.Peek().SetActive(true);
    }

    /// <summary>
    /// @broef 前のUIに切り替える
    /// </summary>
    public void PopPreviousUI()
    {
        if(subMenuStack.Count == 1)
        {
            LeftNetworkServer network = GetComponent<LeftNetworkServer>();
            network.Disconnect();
            return;
        }

        subMenuStack.Peek().SetActive(false);
        subMenuStack.Pop();
        subMenuStack.Peek().SetActive(true);
    }


    /// <summary>
    /// @brief ルームIDのテキストのアクセサー
    /// </summary>
    public Text RoomIDText {
        get { return roomIDText; }
        set { roomIDText = value; }
    }

}
