using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private void LateUpdate()
    {
        if (target)
            transform.position = target.position + Vector3.back * 10;
    }
}
