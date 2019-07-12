using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MachingPlayerIcon : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text playerName;
    [SerializeField]
    private Image playerImage;
    [SerializeField]
    private Sprite otherPlayerImage;

    // Use this for initialization
    void Start()
    {

        if (photonView.IsMine)
        {
            playerName.text = PhotonNetwork.LocalPlayer.NickName;
        }
        else
        {
            playerName.text = PhotonNetwork.PlayerListOthers[PhotonNetwork.CurrentRoom.PlayerCount - 2].NickName;
            playerImage.sprite = otherPlayerImage;
        }

        //this.transform.SetParent(GameObject.Find("PlayerIconPanel").GetComponent<Transform>(), false);  //パネルの子オブジェクトに設定する

        

    }

    // Update is called once per frame
    void Update()
    {

    }

}
