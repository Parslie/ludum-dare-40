using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour {

    #region Singleton
    public static SceneHandler Instance()
    {
        return FindObjectOfType<SceneHandler>();
    }
    #endregion

    [SerializeField]
    private GameObject overlay, overlayMask;
    private float overlaySize;
    private float maskSizeStart, maskSizeEnd, t;

    private string levelName;

    private void Start()
    {
        overlaySize = overlay.transform.localScale.x;
        maskSizeStart = 0;
        maskSizeEnd = overlaySize;
        t = 0;
        levelName = "";
    }

    private void Update()
    {
        if (t != 1)
            t += Time.deltaTime * 1.25f;
        if (t > 1)
            t = 1;

        overlayMask.transform.localScale = Vector2.one * Mathf.Lerp(maskSizeStart, maskSizeEnd, t);

        if (levelName != "" && overlayMask.transform.localScale.x == maskSizeEnd)
            LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(levelName);
    }

    public void ChangeScene(string levelName)
    {
        maskSizeStart = overlaySize;
        maskSizeEnd = 0;
        t = 0;
        overlayMask.transform.localScale = Vector2.one * Mathf.Lerp(maskSizeStart, maskSizeEnd, t);

        this.levelName = levelName;
    }
}
