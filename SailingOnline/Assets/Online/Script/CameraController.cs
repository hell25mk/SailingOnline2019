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

    void Start()
    {
        playerShip = null;
        scale = 5.0f;
        cameraSpeed = 1.0f;

        prevPlayerPos = new Vector3(-5.0f, 1.0f, -5.0f);

    }


    void Update()
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
