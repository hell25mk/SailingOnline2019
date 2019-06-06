using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //カメラのy方向の角度範囲
    /*private const float YAngle_Min = -89.0f;
    private const float YAngle_Max = 89.0f;*/

    public Transform target;
    //public Vector3 offset;
    //private Vector3 lookAt;

    //private float distance = 5.0f;
    //private float minDistance = 1.0f;
    //private float maxDistance = 20.0f;
    //private float currentX = 0.0f;
    //private float currentY = 0.0f;

    //private float keyMoveX = 1.0f;

	// Use this for initialization
	void Start () {
        target = null;
	}
	
	// Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.A))
        {
            currentX -= keyMoveX;
        }
        if (Input.GetKey(KeyCode.D))
        {
            currentX += keyMoveX;
        }
        */
        //distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

	void LateUpdate () {

        if(!target)
        {
            return;
        }

        /*lookAt = target.position + offset;
        Vector3 dire = new Vector3(0.0f, 0.0f, distance);
        transform.position = lookAt + 0 * dire;*/
        transform.LookAt(target);

        /*lookAt = target.position + offset;
        Vector3 dire = new Vector3(0.0f, 0.0f, -distance);
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);

        transform.position = lookAt + rotation * dire;
        transform.rotation = rotation;
        transform.LookAt(lookAt);*/

    }

}
