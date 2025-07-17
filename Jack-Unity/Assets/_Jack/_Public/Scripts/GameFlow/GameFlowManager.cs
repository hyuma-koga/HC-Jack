using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    [SerializeField] private TitleUI titleUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private BlockSpawner spawner;

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

        StartGame();
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
        Debug.Log("�Q�[���J�n�I");
    }

    public void OnGameOver()
    {
        Debug.Log($" OnGameOver() �Ăяo��: Score = {scoreManager.CurrentScore}, Best = {scoreManager.BestScore}");
        gameUI.Hide();
        gameOverUI.Show();
        gameOverUI.UpdateScore(scoreManager.CurrentScore, scoreManager.BestScore);
    }

    public void OnReturnToTitle()
    {
        if (boardManager != null)
        {
            Debug.Log($"Board ��̃u���b�N���폜: {boardManager.BoardBlocksRoot.childCount} ��");
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