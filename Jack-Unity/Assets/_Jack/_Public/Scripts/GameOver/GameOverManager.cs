using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameOverUI       gameOverUI;
    [SerializeField] private GameFlowManager  gameFlowManager;
    [SerializeField] private GameOverAnimator gameOverAnimator;

    private BoardManager                      boardManager;
    private BoardPlacer                       placer;
    private BlockSpawner                      spawner;
    private bool                              isGameOver = false;


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
            if (spawnPoint.childCount == 0) 
            {
                continue;
            } 

            var block = spawnPoint.GetChild(0);
            var data = block.GetComponent<BlockComponent>().data;

            //Note: 1つでも置けるならゲームを続行.
            if (placer.CanPlaceBlockAnywhere(data.shape))
            {
                return; 
            }
        }

        TriggerGameOver();
    }

    public void TriggerGameOver()
    {
        isGameOver = true;

        gameOverAnimator.Play();
        gameOverAnimator.onAnimationComplete = () =>
        {
            gameFlowManager?.OnGameOver();
            gameOverUI?.Show();
            gameOverAnimator.ClearAnimationCells();
        };
    }

    public void Retry()
    {
        isGameOver = false;
        gameOverUI?.Hide();
    }
}