using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Layer : MonoBehaviour {

    [SerializeField]
    private string layerName;
    [SerializeField]
    private int layerIndex;

    public void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = layerName;
        renderer.sortingOrder = layerIndex;
    }
}
