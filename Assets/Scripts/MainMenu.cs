using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    [SerializeField] private Text highScoreText;
    
    void Start() {
        var highScore = PlayerPrefs.GetInt(ScoreSystem.HighScore, 0);
        highScoreText.text = $"High score: {highScore.ToString()}";
    }
}
