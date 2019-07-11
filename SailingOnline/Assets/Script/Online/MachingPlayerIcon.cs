using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MachingPlayerIcon : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private Text playerName;

    // Use this for initialization
    void Start () {

        playerName.text = PhotonNetwork.LocalPlayer.NickName;
        this.transform.SetParent(GameObject.Find("PlayerIconPanel").GetComponent<Transform>(), false);  //パネルの子オブジェクトに設定する

    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
