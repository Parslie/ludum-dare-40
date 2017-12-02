using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager Instance()
    {
        return FindObjectOfType<GameManager>();
    }
    #endregion

    public enum GameState { NotPlaying, Playing, Paused }
    public static GameState gameState = GameState.NotPlaying;

    [SerializeField]
    private float timeTilStart, speedPerPoint, maxSpeed;

    private int score;

    [Header("UI")]
    [SerializeField]
    private TextMesh timer;
    [SerializeField]
    private TextMesh scoreText, speedText;

    private void Start()
    {
        StartCoroutine(InitGame());
    }

    private void Update()
    {
        scoreText.text = "Score: " + score;
        speedText.text = "Speed: " + Time.timeScale + ":1";
    }

    private IEnumerator InitGame()
    {
        timer.gameObject.SetActive(false);

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

    public void AddPoints(int toAdd)
    {
        score += toAdd;
        Time.timeScale = 1 + score * speedPerPoint;
    }
}
