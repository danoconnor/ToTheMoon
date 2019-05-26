using Assets.Scripts.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController SharedInstance;

    public bool Paused { get; private set; }

    public GameState GameState { get; private set; }
    public FlightData LastFlightData { get; private set; }

    public void Start()
    {
        Paused = false;
    }

    public void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        // Load data from disk
        if (File.Exists(getGameStateFilePath()))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(getGameStateFilePath(), FileMode.Open);

            GameState = (GameState)formatter.Deserialize(file);
        }
        else
        {
            GameState = new GameState();
        }
    }

    public void OnDisable()
    {
        if (GameState == null)
        {
            return;
        }

        // Save data to disk
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(getGameStateFilePath());

        formatter.Serialize(file, GameState);

        file.Close();
    }

    public void SetGameOver(FlightData flightData)
    {
        LastFlightData = flightData;
        GameState.Money += MoneyForAltitude(LastFlightData.MaxHeight) + MoneyForDuration(LastFlightData.FlightTime) + LastFlightData.BonusMoney;
        SceneManager.LoadScene(_gameOverSceneName, LoadSceneMode.Single);
    }

    public int MoneyForAltitude(float altitude)
    {
        return (int)(altitude / 10);
    }

    public int MoneyForDuration(TimeSpan duration)
    {
        return (int)(duration.TotalSeconds * 2);
    }

    private string getGameStateFilePath()
    {
        return Application.persistentDataPath + "/GameState.dat";
    }

    private static readonly string _gameOverSceneName = "FlightResultsScene";
}
