using Photon.Pun;
using UnityEngine;

public class PlayerName : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerName;  // 名前のオブジェクトをアタッチする用

    void Start ()
    {
        playerName.GetComponent<TextMesh>().text = photonView.Owner.NickName;
    }

}
