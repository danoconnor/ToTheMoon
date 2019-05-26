using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public static readonly string FlightControllerTag = "FlightController";

    public bool IsPaused { get; private set; }

    public void Start()
    {
        IsPaused = false;
        _flightStartTime = DateTime.Now;
        _bonusMoney = 0;

        _player = GameObject.FindGameObjectWithTag(PlayerController.PlayerTag).GetComponent<PlayerController>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            IsPaused = !IsPaused;
        }
    }

    public void GameOver()
    {
        TimeSpan flightDuration = DateTime.Now - _flightStartTime;
        float maxHeight = _player.MaxHeight;

        FlightData flightData = new FlightData()
        {
            FlightTime = flightDuration,
            MaxHeight = maxHeight,
            BonusMoney = _bonusMoney
        };

        GameController.SharedInstance.SetGameOver(flightData);
    }

    public void PlayerCollectedBonusMoney(int value)
    {
        _bonusMoney += value;
    }

    private int _bonusMoney;
    private DateTime _flightStartTime;
    private PlayerController _player;
}
