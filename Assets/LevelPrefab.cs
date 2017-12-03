using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefab : MonoBehaviour {

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player)
            if (Vector2.Distance(transform.position, player.position) > 50)
                Destroy(gameObject);
    }
}
