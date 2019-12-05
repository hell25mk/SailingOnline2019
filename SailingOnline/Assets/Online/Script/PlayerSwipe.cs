using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSwipe : MonoBehaviourPunCallbacks
{
    private bool isFlick;
    private bool isClick;
    private Vector3 touchStartPos;
    private Vector3 touchEndPos;

    public void Update()
    {
        //自身かどうかの判断
        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            isFlick = true;
            touchStartPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);

            //0.2秒後にFlickOff処理
            Invoke("FlickOff", 0.2f);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                        Input.mousePosition.y,
                        Input.mousePosition.z);


            float directionX = (touchEndPos.x - touchStartPos.x) * 0.003f;
            if (directionX >= 1.0f) directionX = 1.0f;
            if (directionX <= -1.0f) directionX = -1.0f;


            transform.Rotate(0, directionX, 0);



            if (touchStartPos != touchEndPos)
            {
                ClickOff();
            }
        }
    }

    public void FlickOff()
    {
        isFlick = false;
    }

    public bool IsFlick()
    {
        return isFlick;
    }

    public void ClickOn()
    {
        isClick = true;
        Invoke("ClickOff", 0.2f);
    }

    public bool IsClick()
    {
        return isClick;
    }

    public void ClickOff()
    {
        isClick = false;
    }
}
