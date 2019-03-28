using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlaneEnemy;

    public int NumEnemies;
    public float RangeToSpawnEnemies;

    void Start()
    {
        _player = Player.GetComponent<Rigidbody2D>();
        _enemyPositions = new List<Vector2>();
    }

    void FixedUpdate()
    {
        spawnObjects();
    }

    private void spawnObjects()
    {
        // Need a given amount of enemies above, below, left, and right of the player
        // If there are not enough enemies in a given sector, spawn more now

        Vector2 playerPos = _player.position;

        int aboveCount = _enemyPositions.Count(pos => pos.y > playerPos.y && pos.y - playerPos.y < RangeToSpawnEnemies);
        for (int i = aboveCount; i < NumEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(playerPos.x - RangeToSpawnEnemies, playerPos.x + RangeToSpawnEnemies),
                                            Random.Range(playerPos.y, playerPos.y + RangeToSpawnEnemies));
            _enemyPositions.Add(spawnPos);
            Instantiate(PlaneEnemy, spawnPos, Quaternion.identity);
        }
    }

    private Rigidbody2D _player;
    private List<Vector2> _enemyPositions;

    private const string _playerTag = "Player";
}
