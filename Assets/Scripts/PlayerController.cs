using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float BoostAcceleration;
    public float RotationRate;
    public float MaxSpeed;

    public GameObject MainCameraObject;
    public float CameraOffset;

    void Start()
    {
        _cameraPosition = MainCameraObject.GetComponent<Transform>();
        _player = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sprites = Resources.LoadAll<Sprite>(SpriteName);
    }

    void FixedUpdate()
    {
        updateRotation();
        updateVelocity();
        updateSprite();
        updateCameraPosition();
    }

    private void updateRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _player.rotation += RotationRate;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            _player.rotation -= RotationRate;
        }
    }

    private void updateVelocity()
    {
        if (Input.GetKey(KeyCode.UpArrow))
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
        _spriteRenderer.sprite = Input.GetKey(KeyCode.UpArrow) ? _sprites[1] : _sprites[0];
    }

    private void updateCameraPosition()
    {
        _cameraPosition.position = new Vector3(_player.position.x, _player.position.y + CameraOffset, _cameraPosition.position.z);
    }

    private Transform _cameraPosition;
    private Rigidbody2D _player;
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _sprites;

    private float _maxSpeed;

    private const string SpriteName = "sprites/player";
}
