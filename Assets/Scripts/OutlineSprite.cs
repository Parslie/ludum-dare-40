using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineSprite : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer[] outlines = new SpriteRenderer[4];

    [SerializeField]
    private bool updateOutlines;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer.drawMode == SpriteDrawMode.Simple)
            spriteRenderer.size = Vector2.one;

        outlines[0] = new GameObject("OutlineTop", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
        outlines[1] = new GameObject("OutlineRight", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
        outlines[2] = new GameObject("OutlineBottom", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();
        outlines[3] = new GameObject("OutlineLeft", typeof(SpriteRenderer)).GetComponent<SpriteRenderer>();

        foreach (SpriteRenderer outline in outlines)
        {
            outline.transform.parent = transform;
            outline.transform.position = transform.position;
            outline.transform.localScale = spriteRenderer.size;
            outline.color = Color.black;
            outline.sortingOrder = spriteRenderer.sortingOrder - 5000;
            outline.sortingLayerID = spriteRenderer.sortingLayerID;
            outline.transform.rotation = spriteRenderer.transform.rotation;
            outline.sprite = spriteRenderer.sprite;
        }

        outlines[0].transform.Translate(Vector2.up * 0.1f);
        outlines[1].transform.Translate(Vector2.right * 0.1f);
        outlines[2].transform.Translate(Vector2.down * 0.1f);
        outlines[3].transform.Translate(Vector2.left * 0.1f);
    }

    private void Update()
    {
        if (updateOutlines)
            foreach (SpriteRenderer outline in outlines)
                outline.sprite = spriteRenderer.sprite;
    }
}
