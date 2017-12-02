using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject[] levelPrefabs;
    private int timesSpawned;

    [SerializeField]
    private Vector2 nextSpawnPos;
    [SerializeField]
    private float distanceToSpawn;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(player.position, nextSpawnPos) <= distanceToSpawn)
        {
            GameObject tmp = Instantiate(levelPrefabs[Random.Range(0, levelPrefabs.Length)], nextSpawnPos, Quaternion.identity);
            timesSpawned++;
            tmp.name = timesSpawned.ToString();
            nextSpawnPos = GameObject.Find(tmp.name + "/NextSpawn").transform.position;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(nextSpawnPos, 0.25f);
        Gizmos.DrawLine(nextSpawnPos, nextSpawnPos + Vector2.left * distanceToSpawn);
    }
}
