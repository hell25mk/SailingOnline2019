using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks {

    [SerializeField]
    private GameObject namePlate;
    [SerializeField]
    private TextMesh nameText;
    [SerializeField]
    private GameObject mainPlayerMark;
    private Camera playerCamera;
    private bool isPlayerMove;

    private void Start()
    {
        //プレイヤーの名前設定
        nameText.text = photonView.Owner.NickName;
        isPlayerMove = true;
        
        if (photonView.IsMine)
        {
            //メインプレイヤーの表示を生じする
            mainPlayerMark.SetActive(true);
            playerCamera = Camera.main;
            playerCamera.GetComponent<CameraController>().playerShip = this.gameObject;
            //メインプレイヤーの名前表示を消す
            namePlate.SetActive(false);

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

        //一時停止 スマホ
        if(Input.GetTouch(0).phase == TouchPhase.Began)
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
