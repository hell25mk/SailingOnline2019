/*
 * 長嶋
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MachPlayerManager : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject parentObject;

    private List<GameObject> playerList;

    // Use this for initialization
    void Start()
    {

        playerList = new List<GameObject>();
        AddPlayer();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPlayer()
    {
        Vector3 vector = new Vector3(0.0f, 0.0f);
        GameObject player = PhotonNetwork.Instantiate("PlayerIcon", vector, Quaternion.identity);

        player.transform.SetParent(parentObject.transform, false);   //子オブジェクトにする
        playerList.Add(player);

    }

    public void OutPlayer()
    {



    }

}
