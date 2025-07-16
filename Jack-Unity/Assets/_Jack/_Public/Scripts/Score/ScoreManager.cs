using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private ComboManager comboManager;

    private int currentScore = 0;
    private bool lineClearedThisTurn = false;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddPlaceScore(int blockCells)
    {
        currentScore += blockCells;
        UpdateScoreUI();
        Debug.Log($"配置スコア: +{blockCells}");
    }

    public void AddLineClearScore(int linesCleared)
    {
        if (linesCleared <= 0) return;

        int baseLineScore = linesCleared switch
        {
            1 => 10,
            2 => 20,
            3 => 60,
            4 => 120,
            5 => 200,
            6 => 300,
            _ => linesCleared * 100
        };

        int comboBonus = comboManager.GetComboCount() * baseLineScore;
        int totalScore = baseLineScore + comboBonus;

        currentScore += totalScore;

        lineClearedThisTurn = true;

        comboManager.AddCombo(linesCleared);

        UpdateScoreUI();

        Debug.Log($"行列スコア: Base: {baseLineScore}, Combo: {comboBonus}, Total: +{totalScore}");
    }

    public void StartTurn()
    {
        lineClearedThisTurn = false;
    }

    public void EndTurn()
    {
        if (!lineClearedThisTurn)
        {
            comboManager.ResetCombo();
        }

        lineClearedThisTurn = false;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            int displayCombo = Mathf.Max(comboManager.GetComboCount() - 1, 0);
            scoreText.text = $"{currentScore} {displayCombo}";
        }
    }
}