using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private bool snap;

    private void LateUpdate()
    {
        if (target)
        {
            Vector2 pos = target.position;
            if (snap)
            {
                pos.x = Mathf.Round(pos.x);
                pos.y = Mathf.Round(pos.y);
            }
            transform.position = pos;
        }
    }
}
