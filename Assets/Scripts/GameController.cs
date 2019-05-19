using Assets.Scripts.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController SharedInstance;

    public bool Paused { get; private set; }

    public GameState GameState { get; private set; }

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
        // Save data to disk
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(getGameStateFilePath());

        formatter.Serialize(file, GameState);

        file.Close();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Paused = !Paused;
        }
    }

    public void SetGameOver()
    {
        SceneManager.LoadScene(_gameOverSceneName, LoadSceneMode.Single);
    }

    private string getGameStateFilePath()
    {
        return Application.persistentDataPath + "/GameState.dat";
    }

    private static readonly string _gameOverSceneName = "GameOverScene";
}
