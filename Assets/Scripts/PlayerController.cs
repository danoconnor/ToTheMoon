using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static readonly string PlayerTag = "Player";

    public float BoostAcceleration;
    public float RotationRate;
    public float MaxSpeed;

    public GameObject MainCameraObject;
    public float CameraOffset;

    public void Start()
    {
        _cameraPosition = MainCameraObject.GetComponent<Transform>();
        _player = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sprites = Resources.LoadAll<Sprite>(SpriteName);
        _gameController = GameController.SharedInstance;
    }

    public void FixedUpdate()
    {
        if (!_gameController.Paused)
        {
            updateRotation();
            updateVelocity();
            updateSprite();
            updateCameraPosition();
        }
    }

    private void updateRotation()
    {
        if (Mathf.Abs(_player.angularVelocity) < RotationRate)
        {
            _player.angularVelocity = 0;
        }

        // If the player is spinning due to a collision, the arrows should just slow the spin rate until it is at zero
        // Then the player will regain control of the ship and they can continue to rotate like normal
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Mathf.Approximately(_player.angularVelocity, 0))
            {
                _player.rotation += RotationRate;
            }
            else
            {
                _player.angularVelocity += RotationRate;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Approximately(_player.angularVelocity, 0))
            {
                _player.rotation -= RotationRate;
            }
            else
            {
                _player.angularVelocity -= RotationRate;
            }
        }
    }

    private void updateVelocity()
    {
        if (!Input.GetKey(KeyCode.DownArrow))
        {
            float rotationInRads = _player.rotation * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(-1 * Mathf.Sin(rotationInRads), Mathf.Cos(rotationInRads));

            Vector2 newVelocity = _player.velocity + direction;

            // Only apply speed cap if the player is moving upwards and is greater than the max speed
            if (newVelocity.y > MaxSpeed && newVelocity.magnitude > MaxSpeed)
            {
                newVelocity = newVelocity.normalized * MaxSpeed;
            }

            _player.velocity = newVelocity;
        }
    }

    private void updateSprite()
    {
        _spriteRenderer.sprite = Input.GetKey(KeyCode.DownArrow) ? _sprites[0] : _sprites[1];
    }

    private void updateCameraPosition()
    {
        _cameraPosition.position = new Vector3(_player.position.x, _player.position.y + CameraOffset, _cameraPosition.position.z);
    }

    private Transform _cameraPosition;
    private Rigidbody2D _player;
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _sprites;
    private GameController _gameController;

    private const string SpriteName = "sprites/player";
}
