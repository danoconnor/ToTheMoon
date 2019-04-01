using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSpawnController : MonoBehaviour
{
    public GameObject Player;
    public GameObject PlaneEnemy;

    public int NumEnemies;
    public float RangeToSpawnObjects;
    public float SpawnBufferFromPlayerHorizontal;
    public float SpawnBufferFromPlayerAbove;
    public float SpawnBufferFromPlayerBelow;

    public void Start()
    {
        _player = Player.GetComponent<Rigidbody2D>();
        _enemyPositions = new List<Vector2>();
    }

    public void FixedUpdate()
    {
        spawnObjects();
    }

    private void spawnObjects()
    {
        // Need a given amount of enemies above, below, left, and right of the player
        // If there are not enough enemies in a given sector, spawn more now

        Vector2 playerPos = _player.position;

        float aboveWithBuffer = playerPos.y + SpawnBufferFromPlayerAbove;
        float belowWithBuffer = playerPos.y - SpawnBufferFromPlayerBelow;
        float rightWithBuffer = playerPos.x + SpawnBufferFromPlayerHorizontal;
        float leftWithBuffer = playerPos.x - SpawnBufferFromPlayerHorizontal;

        int aboveCount = _enemyPositions.Count(pos => pos.y > playerPos.y && pos.y - playerPos.y < RangeToSpawnObjects + SpawnBufferFromPlayerAbove);
        for (int i = aboveCount; i < NumEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(leftWithBuffer - RangeToSpawnObjects, rightWithBuffer + RangeToSpawnObjects),
                                            Random.Range(aboveWithBuffer, aboveWithBuffer + RangeToSpawnObjects));
            _enemyPositions.Add(spawnPos);
            Instantiate(PlaneEnemy, spawnPos, Quaternion.identity);
        }

        int belowCount = _enemyPositions.Count(pos => pos.y < playerPos.y && playerPos.y - pos.y < RangeToSpawnObjects + SpawnBufferFromPlayerBelow);
        for (int i = belowCount; i < NumEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(leftWithBuffer - RangeToSpawnObjects, rightWithBuffer + RangeToSpawnObjects),
                                            Random.Range(belowWithBuffer - RangeToSpawnObjects, belowWithBuffer));
            _enemyPositions.Add(spawnPos);
            Instantiate(PlaneEnemy, spawnPos, Quaternion.identity);
        }

        int rightCount = _enemyPositions.Count(pos => pos.x > playerPos.x && pos.x - playerPos.x < RangeToSpawnObjects + SpawnBufferFromPlayerHorizontal);
        for (int i = rightCount; i < NumEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(rightWithBuffer, rightWithBuffer + RangeToSpawnObjects),
                                            Random.Range(belowWithBuffer - RangeToSpawnObjects, aboveWithBuffer + RangeToSpawnObjects));
            _enemyPositions.Add(spawnPos);
            Instantiate(PlaneEnemy, spawnPos, Quaternion.identity);
        }

        int leftCount = _enemyPositions.Count(pos => pos.x < playerPos.x && playerPos.x - pos.x < RangeToSpawnObjects + SpawnBufferFromPlayerHorizontal);
        for (int i = leftCount; i < NumEnemies; i++)
        {
            Vector2 spawnPos = new Vector2(Random.Range(leftWithBuffer - RangeToSpawnObjects, leftWithBuffer),
                                            Random.Range(belowWithBuffer - RangeToSpawnObjects, aboveWithBuffer + RangeToSpawnObjects));
            _enemyPositions.Add(spawnPos);
            Instantiate(PlaneEnemy, spawnPos, Quaternion.identity);
        }
    }

    private Rigidbody2D _player;
    private List<Vector2> _enemyPositions;
}
