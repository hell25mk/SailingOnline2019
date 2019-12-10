using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameDirection : MonoBehaviour
{
    void LateUpdate()
    {
        // 回転をカメラと同期させる
        transform.rotation = Camera.main.transform.rotation;
    }

}
