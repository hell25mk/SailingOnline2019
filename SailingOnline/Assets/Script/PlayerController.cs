using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks {

    [SerializeField]
    private GameObject mainPlayerMark;
    private Camera playerCamera;
    private bool isPlayerMove;

    private void Start()
    {
        isPlayerMove = true;
        
        if (photonView.IsMine)
        {
            //メインプレイヤーのマークを表示する
            mainPlayerMark.SetActive(true);
            playerCamera = Camera.main;
            playerCamera.GetComponent<CameraController>().PlayerShip = this.gameObject;

            Debug.Log("カメラを取得しました");
        }
    }

	// Update is called once per frame
	void Update () {

        //自身かどうかの判断
        if (!photonView.IsMine)
        {
            return;
        }

        //一時停止 PC
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isPlayerMove = !isPlayerMove;
        }

        //ベクトルを正規化
        var direction = new Vector3(0.0f, 0.0f, -(isPlayerMove ? 3.0f : 0.0f)).normalized;
        //移動速度を時間依存にし、移動量を求める
        var dv = 5.0f * Time.deltaTime * direction;

        transform.Translate(dv.x, 0.0f, dv.z);
        transform.Rotate(0.0f, Input.GetAxis("Horizontal"), 0.0f);
    }

}
