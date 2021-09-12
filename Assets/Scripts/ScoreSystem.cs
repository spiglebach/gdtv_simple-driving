using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    public const string HighScore = "HighScore";
    
    [SerializeField] private Text scoreText;
    [SerializeField] private float scoreGain = 10f;
    private float score;

    void Update() {
        score += Time.deltaTime * scoreGain;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy() {
        var currentHighScore = PlayerPrefs.GetInt(HighScore);
        if (score > currentHighScore) {
            PlayerPrefs.SetInt(HighScore, Mathf.FloorToInt(score));
            PlayerPrefs.Save();
        }
    }
}
