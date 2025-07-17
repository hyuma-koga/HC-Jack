using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text   currentScoreText;
    [SerializeField] private TMP_Text   bestScoreText;

    public void Show()
    {
        gameUI.SetActive(true);
    }
    public void Hide()
    {
        gameUI.SetActive(false); 
    }

    public void UpdateScore(int current, int best)
    {
        currentScoreText.text = $"{current}";
        bestScoreText.text = $"{best}";
    }
}
