using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    [SerializeField]
    private Transform target;

    private bool targetHasDied;

    private void LateUpdate()
    {
        if (target)
            transform.position = target.position + Vector3.back * 10;
        else if (!targetHasDied)
            StartCoroutine(ShakeScreen());
    }

    private IEnumerator ShakeScreen()
    {
        targetHasDied = true;

        Vector3 originalPos = transform.position;
        for (int i = 0; i < 20; i++)
        {
            transform.position = originalPos + Vector3.right * Random.Range(-.25f, .25f) + Vector3.up * Random.Range(-.25f, .25f); 
            yield return new WaitForSeconds(0.015f);
        }
        transform.position = originalPos;
    }
}
