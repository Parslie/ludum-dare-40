using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

    [SerializeField]
    private string inGameSceneName;

    [SerializeField]
    private TextMesh highScore;
    [SerializeField]
    private GameObject pressAnyTo;
    [SerializeField]
    private float blinksPerSec;
    private float timeToBlink;

    private void Start()
    {
        highScore.text = "High Score: ";
        highScore.text += (PlayerPrefs.HasKey("High Score")) ? PlayerPrefs.GetInt("High Score").ToString() : "0";
    }

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
            SceneHandler.Instance().ChangeScene(inGameSceneName);

        if (Time.time >= timeToBlink)
        {
            pressAnyTo.SetActive(!pressAnyTo.activeSelf);
            timeToBlink = Time.time + 1 / blinksPerSec;
        }
    }
}
