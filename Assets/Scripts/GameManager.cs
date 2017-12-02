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
}
