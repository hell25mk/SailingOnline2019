using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float scale;
    public float cameraSpeed;
    private GameObject playerShip;
    private Vector3 prevPlayerPos;
    private Vector3 posVector;

    public GameObject PlayerShip {
        get { return playerShip; }
        set { playerShip = value; }
    }

    // Use this for initialization
    void Start()
    {
        playerShip = null;
        scale = 8.0f;
        cameraSpeed = 2.0f;

    }

    // Update is called once per frame
    void Update()
    {
        prevPlayerPos = new Vector3(0.0f, 0.0f, -1.0f);
    }

    void LateUpdate()
    {

        if (!playerShip)
        {
            return;
        }

        Vector3 currentPlayerPos = playerShip.transform.position;
        Vector3 backVector = (prevPlayerPos - currentPlayerPos).normalized;

        posVector = (backVector == Vector3.zero) ? posVector : backVector;

        Vector3 targetPos = currentPlayerPos + scale * posVector;

        targetPos.y = targetPos.y + 3.0f;

        this.transform.position = Vector3.Lerp(
            this.transform.position,
            targetPos,
            cameraSpeed * Time.deltaTime
            );

        this.transform.LookAt(playerShip.transform.position);
        prevPlayerPos = playerShip.transform.position;

    }

}
