using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text energyText;
    [SerializeField] private int maxEnergy = 5;
    [SerializeField] private int energyRechargeDurationInMinutes = 1;

    private int energy;
    
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    
    void Start() {
        var highScore = PlayerPrefs.GetInt(ScoreSystem.HighScore, 0);
        highScoreText.text = $"High score: {highScore.ToString()}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if (energy <= 0) {
            var energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if (energyReadyString == string.Empty) return;

            var energyReady = DateTime.Parse(energyReadyString);
            if (DateTime.Now > energyReady) {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
                PlayerPrefs.Save();
            }
        }

        energyText.text = $"Play ({energy.ToString()})";
    }
    
    public void LoadLevel1() {
        if (energy <= 0) return;
        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);
        if (energy <= 0) {
            PlayerPrefs.SetString(EnergyReadyKey, DateTime.Now.AddMinutes(energyRechargeDurationInMinutes).ToString());
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level 1");
    }
}
