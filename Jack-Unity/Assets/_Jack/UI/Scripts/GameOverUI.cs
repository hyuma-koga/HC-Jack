using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMP_Text   currentScoreText;
    [SerializeField] private TMP_Text   bestScoreText;

    public void Show()
    {
        gameOverUI.SetActive(true);
    }

    public void Hide()
    {
        gameOverUI.SetActive(false);
    }

    public void UpdateScore(int current, int best)
    {
        Debug.Log($"GameOverUI.UpdateScore() é¿çs: {current} / {best}");
        currentScoreText.text = $"{current}";
        bestScoreText.text = $"{best}";
    }
}
