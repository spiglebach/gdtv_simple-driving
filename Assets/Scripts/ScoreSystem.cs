using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour {
    [SerializeField] private Text scoreText;
    [SerializeField] private float scoreGain = 10f;
    private float score;

    void Update() {
        score += Time.deltaTime * scoreGain;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
