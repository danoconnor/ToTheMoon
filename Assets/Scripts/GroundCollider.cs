using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public void Start()
    {
        _gameController = GameController.SharedInstance;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerController.PlayerTag)
        {
            _gameController.SetGameOver();
        }
    }

    private GameController _gameController;
}
