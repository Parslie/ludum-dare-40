using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public enum GameState { NotPlaying, Playing, Paused }
    public static GameState gameState = GameState.NotPlaying;

    [SerializeField]
    private float timeTilStart;

    [Header("UI")]
    [SerializeField]
    private TextMesh timer;

    private void Start()
    {
        StartCoroutine(InitGame());
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
}
