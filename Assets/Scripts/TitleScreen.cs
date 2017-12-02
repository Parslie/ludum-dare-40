using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour {

    [SerializeField]
    private string inGameSceneName;

    private void Start()
    {
        Application.targetFrameRate = 80;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
            SceneHandler.Instance().ChangeScene(inGameSceneName);
    }
}
