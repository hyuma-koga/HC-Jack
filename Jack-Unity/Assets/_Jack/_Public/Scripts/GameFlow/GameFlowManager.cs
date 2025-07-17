using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private TitleUI            titleUI;
    [SerializeField] private GameUI             gameUI;
    [SerializeField] private GameOverUI         gameOverUI;
    [SerializeField] private ScoreManager       scoreManager;
    [SerializeField] private BlockSpawner       spawner;
    [SerializeField] private IntroAnimator      introAnimator;

    private BoardManager boardManager;
    private BlockSpawner blockSpawner;

    private void Awake()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        blockSpawner = FindFirstObjectByType<BlockSpawner>();
    }

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
        if (blockSpawner != null)
        {
            blockSpawner.ClearSpawnedBlocks();
            blockSpawner.SpawnBlocks();
        }

        if (boardManager != null)
        {
            boardManager.GetPlacer()?.ResetOccupied();
        }
    }

    public void OnGameOver()
    {
        gameUI.Hide();
        gameOverUI.Show();
        gameOverUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }

    public void OnReturnToTitle()
    {
        if (boardManager != null)
        {
            boardManager.ClearAllBlocks();
        }

        scoreManager.ResetScore();
        ShowTitle();
    }

    public void OnScoreChanged()
    {
        gameUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }
}