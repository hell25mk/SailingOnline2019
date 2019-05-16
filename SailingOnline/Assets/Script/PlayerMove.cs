using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPunCallbacks {
	
	// Update is called once per frame
	void Update () {

        if (!photonView.IsMine)
        {
            return;
        }

        var direction = new Vector3(Input.GetAxis("Horizontal"), 0.0f,Input.GetAxis("Vertical")).normalized;
        var dv = 5.0f * Time.deltaTime * direction;

        transform.Translate(dv.x, 0.0f, dv.z);
    }
}
