using Assets.Scripts.Constants;
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
        if (HasSavedData())
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(getGameStateFilePath(), FileMode.Open);

            GameState = (GameState)formatter.Deserialize(file);
        }
        else
        {
            GameState = new GameState();
            GameState.InitializeDefaultValues();
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
        SceneManager.LoadScene(SceneNames.FlightResults, LoadSceneMode.Single);
    }

    public int MoneyForAltitude(float altitude)
    {
        return (int)(altitude / 10);
    }

    public int MoneyForDuration(TimeSpan duration)
    {
        return (int)(duration.TotalSeconds * 2);
    }

    public bool HasSavedData()
    {
        return File.Exists(getGameStateFilePath());
    }

    public void ClearSavedData()
    {
        // Just reset the GameState object, when the game closes we'll write it to disk and overwrite the old data there
        GameState = new GameState();
        GameState.InitializeDefaultValues();
    }

    private string getGameStateFilePath()
    {
        return Application.persistentDataPath + "/GameState.dat";
    }
}
