using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameOverUI      gameOverUI;
    [SerializeField] private GameFlowManager gameFlowManager;

    private BoardManager boardManager;
    private BoardPlacer  placer;
    private BlockSpawner spawner;
    private bool         isGameOver = false;


    private void Awake()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        placer = boardManager.GetPlacer();
        spawner = FindFirstObjectByType<BlockSpawner>();
        gameOverUI?.Hide();
    }

    public void CheckGameOver(Transform[] spawnPoints)
    {
        if (isGameOver)
        {
            return;
        }

        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.childCount == 0) continue;

            var block = spawnPoint.GetChild(0);
            var data = block.GetComponent<BlockComponent>().data;

            if (placer.CanPlaceBlockAnywhere(data.shape))
            {
                return; // 1Ç¬Ç≈Ç‡íuÇØÇÈÇ»ÇÁÉQÅ[ÉÄë±çs
            }
        }

        TriggerGameOver();
    }

    public void TriggerGameOver()
    {
        Debug.Log("Game Over!");

        isGameOver = true;

        gameFlowManager?.OnGameOver();
        gameOverUI?.Show();
    }

    public void Retry()
    {
        Debug.Log("Retry!");

        isGameOver = false;
        gameOverUI?.Hide();
    }
}
