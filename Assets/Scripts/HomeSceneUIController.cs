using Assets.Scripts.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneUIController : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewGameButton;

    public void Start()
    {
        if (!GameController.SharedInstance.HasSavedData())
        {
            // If we don't have any saved data, then make the primary button say "Start" and remove the secondary button.
            Destroy(NewGameButton.gameObject);
            ContinueButton.GetComponentInChildren<Text>().text = "Start";
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneNames.Game, LoadSceneMode.Single);
    }

    public void NewGame()
    {
        GameController.SharedInstance.ClearSavedData();
        ContinueGame();
    }
}
