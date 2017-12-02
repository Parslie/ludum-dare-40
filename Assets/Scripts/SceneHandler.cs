using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    [SerializeField]
    private GameObject overlay, overlayMask;
    private float overlaySize;
    private float maskSize, maskTargetSize, maskSizeVel;

    private string levelName;

    private void Start()
    {
        overlaySize = overlay.transform.localScale.x;
        maskSize = 0;
        maskTargetSize = overlaySize;
    }

    private void Update()
    {
        maskSize = Mathf.SmoothDamp(maskSize, maskTargetSize, ref maskSizeVel, 0.25f);
        overlayMask.transform.localScale = Vector2.one * maskSize;

        if (levelName != "" && maskSize <= 0.05f)
            LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(levelName);
    }

    public void ChangeScene(string name)
    {
        maskSize = overlaySize;
        maskTargetSize = 0;

        levelName = name;
    }
}
