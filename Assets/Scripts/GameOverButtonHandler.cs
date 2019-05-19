using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene(_gameSceneName, LoadSceneMode.Single);
    }

    private static readonly string _gameSceneName = "GameScene";
}
