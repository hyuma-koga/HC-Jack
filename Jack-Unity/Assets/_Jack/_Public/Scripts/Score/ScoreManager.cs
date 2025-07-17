using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text     scoreText;
    [SerializeField] private ComboManager comboManager;

    public int                            CurrentScore => currentScore;
    public int                            BestScore => bestScore;
    private int                           currentScore = 0;
    private int                           bestScore = 0;
    private bool                          lineClearedThisTurn = false;



    private void Start()
    {
        LoadBestScore();
        UpdateScoreUI();
    }

    public void AddPlaceScore(int blockCells)
    {
        currentScore += blockCells;
        UpdateBestScore();
        UpdateScoreUI();
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

        UpdateBestScore();
        UpdateScoreUI();
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

    public void ResetScore()
    {
        currentScore = 0;
        comboManager.ResetCombo();
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

    private void UpdateBestScore()
    {
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            SaveBestScore();
        }
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }
}