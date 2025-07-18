using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private TitleUI       titleUI;
    [SerializeField] private GameUI        gameUI;
    [SerializeField] private GameOverUI    gameOverUI;
    [SerializeField] private BlockSpawner  blockSpawner;
    [SerializeField] private IntroAnimator introAnimator;
    [SerializeField] private ScoreManager  scoreManager;
    [SerializeField] private BoardManager  boardManager;

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
        gameUI.Show();
        gameUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);

        if (introAnimator != null)
        {
            introAnimator.OnAnimationComplete = () =>
            {
                StartGame();
            };

            introAnimator.Play();
        }
        else
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        //すでにボード上にブロックがある場合何もしない
        if (boardManager.HasAnyPlacedBlock())
        {
            return;
        }

        blockSpawner.ClearSpawnedBlocks();
        blockSpawner.SpawnBlocks();
        boardManager.GetPlacer()?.ResetOccupied();
    }

    public void OnGameOver()
    {
        gameUI.Hide();
        gameOverUI.Show();
        gameOverUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }

    public void OnReturnToTitle()
    {
        boardManager.ClearAllBlocks();
        boardManager.GetPlacer()?.ResetOccupied();
        scoreManager.ResetScore();
        ShowTitle();
    }

    public void OnSaveAndReturnToTitle()
    {
        ShowTitle();
    }

    public void OnScoreChanged()
    {
        gameUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }

    public void OnRetryRequested()
    {
        scoreManager.ResetScore();
        boardManager.ClearAllBlocks();
        boardManager.GetPlacer()?.ResetOccupied();
        blockSpawner.ClearSpawnedBlocks();
        blockSpawner.SpawnBlocks();
        gameUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }
}