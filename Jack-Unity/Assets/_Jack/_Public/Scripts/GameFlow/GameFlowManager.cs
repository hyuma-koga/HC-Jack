using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        ShowTitle();
    }

    public void ShowTitle()
    {
        titleUI.Show();
        gameUI.Hide();
        gameOverUI.Hide();
    }

    public void OnStartButtonPressed()
    {
        titleUI.Hide();
        gameOverUI.Hide();
        scoreManager.ResetScore();
        gameUI.Show();
        gameUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);

        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("ゲーム開始！");
    }

    public void OnGameOver()
    {
        Debug.Log($" OnGameOver() 呼び出し: Score = {scoreManager.CurrentScore}, Best = {scoreManager.BestScore}");
        gameUI.Hide();
        gameOverUI.Show();
        gameOverUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }

    public void OnReturnToTitle()
    {
        ShowTitle();
    }

    public void OnScoreChanged()
    {
        gameUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }
}