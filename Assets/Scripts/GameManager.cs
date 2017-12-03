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

    public enum GameState { NotPlaying, Playing }
    public static GameState gameState = GameState.NotPlaying;

    [SerializeField]
    private float timeTilStart;

    private float score, scoreMultiplier;

    [Header("UI")]
    [SerializeField]
    private TextMesh timer;
    [SerializeField]
    private TextMesh scoreText, speedText, gameOverText;

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    private void Update()
    {
        if (gameState == GameState.Playing)
        {
            scoreText.text = "Score\n" + (int)score + string.Format(" (x{0:0.00})", scoreMultiplier);
            speedText.text = string.Format("Speed\n{0:0.0}%", Time.timeScale * 100);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            SceneHandler.Instance().ChangeScene("TitleScreen");
    }

    private IEnumerator InitGame()
    {
        timer.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        scoreMultiplier = 1;
        scoreText.text = "Score\n" + (int)score + string.Format(" (x{0:0.00})", scoreMultiplier);
        speedText.text = string.Format("Speed\n{0:0.0}%", Time.timeScale * 100);

        yield return new WaitForSeconds(timeTilStart - 3);

        timer.gameObject.SetActive(true);
        timer.text = "3";
        yield return new WaitForSeconds(1);

        timer.text = "2";
        yield return new WaitForSeconds(1);

        timer.text = "1";
        yield return new WaitForSeconds(1);

        timer.text = "0";
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

        gameOverText.gameObject.SetActive(true);
        for (float i = 0; i < 1; i += Time.deltaTime * 1.6f)
        {
            gameOverText.transform.localScale = Vector3.one * i;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameOverText.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(1.6f);
        for (float i = 0; i < 1; i += Time.deltaTime * 1.6f)
        {
            gameOverText.transform.localScale = Vector3.one - Vector3.one * i;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        gameOverText.transform.localScale = Vector3.zero;

        SceneHandler.Instance().ChangeScene("TitleScreen");
    }
}