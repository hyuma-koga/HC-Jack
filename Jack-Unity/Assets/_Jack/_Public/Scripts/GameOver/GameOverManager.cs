using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private BoardManager boardManager;
    private BoardPlacer placer;
    private BlockSpawner spawner;

    private void Awake()
    {
        boardManager = FindFirstObjectByType<BoardManager>();
        placer = boardManager.GetPlacer();
        spawner = FindFirstObjectByType<BlockSpawner>();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void CheckGameOver(Transform[] spawnPoints)
    {
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
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    public void Retry()
    {
        Debug.Log("Retry!");
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    public void BackToTitle()
    {
        Debug.Log("Back to Title!");
    }
}
