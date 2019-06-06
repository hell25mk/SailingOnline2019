using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks {

    //[SerializeField]
    //private TextMesh namePlate;
    private Camera playerCamera;

    private void Start()
    {
        //namePlate.text = "プレイヤー";
        
        if (photonView.IsMine)
        {
            playerCamera = Camera.main;
            playerCamera.GetComponent<CameraController>().playerShip = this.gameObject;
            Debug.Log("カメラを取得しました");
            //GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

	// Update is called once per frame
	void Update () {

        //自身かどうかの判断
        if (!photonView.IsMine)
        {
            return;
        }

        //ベクトルを正規化
        var direction = new Vector3(0.0f, 0.0f, -3.0f).normalized;
        //移動速度を時間依存にし、移動量を求める
        var dv = 5.0f * Time.deltaTime * direction;

        transform.Translate(dv.x, 0.0f, dv.z);
        transform.Rotate(0.0f, Input.GetAxis("Horizontal"), 0.0f);
    }

}
