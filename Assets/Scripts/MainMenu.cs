using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text energyText;
    [SerializeField] private Button playButton;
    [SerializeField] private int maxEnergy = 5;
    [SerializeField] private int energyRechargeDurationInMinutes = 1;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;

    private int energy;
    
    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";

    private void Start() {
        OnApplicationFocus(true);
    }

    private void OnApplicationFocus(bool hasFocus) {
        if (!hasFocus) return;
        CancelInvoke();

        var highScore = PlayerPrefs.GetInt(ScoreSystem.HighScore, 0);
        highScoreText.text = $"High score: {highScore.ToString()}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if (energy <= 0) {
            playButton.interactable = false;
            var energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            if (energyReadyString == string.Empty) return;

            var energyReady = DateTime.Parse(energyReadyString);
#if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
#endif
            if (DateTime.Now > energyReady) {
                EnergyRecharged();
            } else {
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }
        energyText.text = $"Play ({energy.ToString()})";
    }

    private void EnergyRecharged() {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        PlayerPrefs.Save();
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
