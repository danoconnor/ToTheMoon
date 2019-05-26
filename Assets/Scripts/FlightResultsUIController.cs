using Assets.Scripts.Constants;
using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlightResultsUIController : MonoBehaviour
{
    public Text AltitudeValueText;
    public Text AltitudeMoneyText;
    public Text DurationValueText;
    public Text DurationMoneyText;
    public Text BonusMoneyText;
    public Text MoneyThisFlightText;
    public Text TotalMoneyText;

    public void Start()
    {
        FlightData flightData = GameController.SharedInstance.LastFlightData;

        int altitudeMoney = GameController.SharedInstance.MoneyForAltitude(flightData.MaxHeight);
        AltitudeValueText.text = Math.Round(flightData.MaxHeight).ToString();
        AltitudeMoneyText.text = $"${altitudeMoney}";

        int durationMoney = GameController.SharedInstance.MoneyForDuration(flightData.FlightTime);
        DurationValueText.text = flightData.FlightTime.ToString(@"mm\:ss");
        DurationMoneyText.text = $"${durationMoney}";

        BonusMoneyText.text = $"${flightData.BonusMoney}";

        int thisFlightMoney = altitudeMoney + durationMoney + flightData.BonusMoney;
        MoneyThisFlightText.text = $"${thisFlightMoney}";

        TotalMoneyText.text = $"${GameController.SharedInstance.GameState.Money}";
    }

    public void FlyAgain()
    {
        SceneManager.LoadScene(SceneNames.Game, LoadSceneMode.Single);
    }

    public void GoToUpgrades()
    {
        SceneManager.LoadScene(SceneNames.Upgrades, LoadSceneMode.Single);
    }
}
