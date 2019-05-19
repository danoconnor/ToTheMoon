using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseableObject : MonoBehaviour
{
    public void Start()
    {
        _gameController = GameController.SharedInstance;
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (_gameController.Paused)
        {
            if (!_pausedMovementValues.HasValue)
            {
                _pausedMovementValues = new PausedMovementValues()
                {
                    Position = _rigidBody.position,
                    Velocity = _rigidBody.velocity,
                    Rotation = _rigidBody.rotation,
                    AngularVelocity = _rigidBody.angularVelocity
                };
            }

            _rigidBody.position = _pausedMovementValues.Value.Position;
            _rigidBody.rotation = _pausedMovementValues.Value.Rotation;
            _rigidBody.velocity = new Vector2(0, 0);
            _rigidBody.angularVelocity = 0;
        }
        else if (_pausedMovementValues.HasValue)
        {
            _rigidBody.velocity = _pausedMovementValues.Value.Velocity;
            _rigidBody.angularVelocity = _pausedMovementValues.Value.AngularVelocity;

            _pausedMovementValues = null;
        }
    }

    private GameController _gameController;
    private Rigidbody2D _rigidBody;
    private PausedMovementValues? _pausedMovementValues;

    private struct PausedMovementValues
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity;
    }
}
