using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour {

    [SerializeField]
    private float floatAmount = 0.1f, speed = 1;

    private Vector2 topPos, bottomPos;
    private float t;
    private bool reverse;


    private void Start()
    {
        topPos = transform.localPosition + Vector3.up * floatAmount;
        bottomPos = transform.localPosition + Vector3.down * floatAmount;
    }

    private void Update()
    {
        if (reverse)
        {
            t -= Time.deltaTime;
            if (t <= 0)
                reverse = false;
        }
        else
        {
            t += Time.deltaTime;
            if (t >= 1)
                reverse = true;
        }

        transform.localPosition = Vector3.Lerp(topPos, bottomPos, Mathf.SmoothStep(0, 1, t));
    }
}
