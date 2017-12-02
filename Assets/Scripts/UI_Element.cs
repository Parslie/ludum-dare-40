using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Element : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private enum Origin { TopLeft, Top, TopRight, Left, Centre, Right, BottomLeft, Bottom, BottomRight };
    [SerializeField]
    private Origin origin;

    [SerializeField]
    private Vector2 offset;
    private Vector2 originPos;

    public void Start()
    {
        // Sets origin position of game object
        switch(origin) 
        {
            case Origin.TopLeft:
                originPos = cam.ScreenToWorldPoint(new Vector2(0, cam.pixelHeight));
                break;
            case Origin.Top:
                originPos = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth / 2, cam.pixelHeight));
                break;
            case Origin.TopRight:
                originPos = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight));
                break;
            case Origin.Left:
                originPos = cam.ScreenToWorldPoint(new Vector2(0, cam.pixelHeight / 2));
                break;
            case Origin.Centre:
                originPos = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth / 2, cam.pixelHeight / 2));
                break;
            case Origin.Right:
                originPos = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, cam.pixelHeight / 2));
                break;
            case Origin.BottomLeft:
                originPos = cam.ScreenToWorldPoint(new Vector2(0, 0));
                break;
            case Origin.Bottom:
                originPos = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth / 2, 0));
                break;
            case Origin.BottomRight:
                originPos = cam.ScreenToWorldPoint(new Vector2(cam.pixelWidth, 0));
                break;
        }
        // Makes origin position pixel-perfect (1 unit = 10 pixels)
        originPos.x = Mathf.Round(originPos.x * 10) / 10;
        originPos.y = Mathf.Round(originPos.y * 10) / 10;

        transform.position = originPos + offset;
    }
}
