using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager Instance()
    {
        return FindObjectOfType<GameManager>();
    }
    #endregion

    public enum GameState { NotPlaying, Playing, Ended }
    public static GameState gameState = GameState.NotPlaying;

    [SerializeField]
    private float timeTilStart;

    private float score, scoreMultiplier;

    [Header("UI")]
    [SerializeField]
    private TextMesh timer;
    [SerializeField]
    private TextMesh scoreText, speedText;
    [SerializeField]
    private GameObject gameOverScreen;

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            scoreText.text = "Score:" + (int)score + string.Format(" (x{0:0.00})", scoreMultiplier);
            speedText.text = string.Format("Speed: {0:0.0}%", Time.timeScale * 100);
        }
        else if (gameState == GameState.Ended)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.Instance().SelectSound();
                SceneHandler.Instance().ChangeScene("InGame");
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                AudioManager.Instance().SelectSound();
                SceneHandler.Instance().ChangeScene("TitleScreen");
            }
        }
    }

    private IEnumerator InitGame()
    {
        timer.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
        gameState = GameState.NotPlaying;
        scoreMultiplier = 1;
        scoreText.text = "Score:" + (int)score + string.Format(" (x{0:0.00})", scoreMultiplier);
        speedText.text = string.Format("Speed: {0:0.0}%", Time.timeScale * 100);

        yield return new WaitForSeconds(timeTilStart - 3);

        timer.gameObject.SetActive(true);
        timer.text = "3";
        AudioManager.Instance().StartUpSound(1);
        yield return new WaitForSeconds(1);

        timer.text = "2";
        AudioManager.Instance().StartUpSound(1);
        yield return new WaitForSeconds(1);

        timer.text = "1";
        AudioManager.Instance().StartUpSound(1);
        yield return new WaitForSeconds(1);

        timer.text = "0";
        AudioManager.Instance().StartUpSound(1.25f);
        gameState = GameState.Playing;
        yield return new WaitForSeconds(1);

        timer.gameObject.SetActive(false);
    }

    public void AddToMultiplier(float toAdd)
    {
        scoreMultiplier += toAdd;
    }

    public void AddPoints(float toAdd)
    {
        score += toAdd;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCo());
    }

    private IEnumerator GameOverCo()
    {
        if (PlayerPrefs.HasKey("High Score") && PlayerPrefs.GetInt("High Score") < (int)(score * scoreMultiplier) || !PlayerPrefs.HasKey("High Score"))
            PlayerPrefs.SetInt("High Score", (int)(score * scoreMultiplier));
        Time.timeScale = 1;
        gameState = GameState.Ended;

        yield return new WaitForSeconds(0.25f);
        gameOverScreen.gameObject.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime * 1.4f)
        {
            gameOverScreen.transform.localScale = Vector3.one * Mathf.SmoothStep(0, 1, i);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameOverScreen.transform.localScale = Vector3.one;
    }
}