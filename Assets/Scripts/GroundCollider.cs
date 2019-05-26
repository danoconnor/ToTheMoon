using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public void Start()
    {
        _flightController = GameObject.FindGameObjectWithTag(FlightController.FlightControllerTag).GetComponent<FlightController>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerController.PlayerTag)
        {
            _flightController.GameOver();
        }

        Destroy(collision.gameObject);
    }

    private FlightController _flightController;
}
