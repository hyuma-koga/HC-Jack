using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private BlockFactory factory;
    [SerializeField] private BlockData[]  blockDataList;
    [SerializeField] private Transform[]  spawnPoints;

    private void Start()
    {
        SpawnBlocks();
    }

    private bool AllPointsEmpty()
    {
        foreach (var point in spawnPoints)
        {
            if (point.childCount != 0)
            {
                return false;
            }
        }

        return true;
    }

    public void SpawnBlocks()
    {
        var shapes = ShapeGenerator.GenerateAllShapes();

        // スコアマネージャにターン開始通知
        var scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.StartTurn();
        }

        for (int i = 0; i < spawnPoints.Length && i < blockDataList.Length; i++)
        {
            var data = ScriptableObject.Instantiate(blockDataList[i]);
            var randomShape = shapes[Random.Range(0, shapes.Count)];
            data.SetShape(randomShape);

            var block = factory.CreateBlock(data);
            block.transform.SetParent(spawnPoints[i], false);
            block.transform.localPosition = Vector3.zero;

            var draggable = block.GetComponent<BlockDraggable>();
            if (draggable != null)
            {
                draggable.SetScale(0.5f);
            }
        }
        // 🔽 新しく追加：Spawn後すぐチェック
        Invoke(nameof(CheckGameOverAfterSpawn), 0.1f); // 少し遅延してから確認（生成が完了してから）
    }

    private void CheckGameOverAfterSpawn()
    {
        var boardManager = FindFirstObjectByType<BoardManager>();
        var placer = boardManager.GetPlacer();
        var gameOverManager = FindFirstObjectByType<GameOverManager>();

        bool canPlaceAny = false;

        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.childCount == 0) continue;

            var block = spawnPoint.GetChild(0);
            var data = block.GetComponent<BlockComponent>().data;

            if (placer.CanPlaceBlockAnywhere(data.shape))
            {
                canPlaceAny = true;
                break;
            }
        }

        if (!canPlaceAny && gameOverManager != null)
        {
            gameOverManager.TriggerGameOver(); // ★ アニメーション付きでゲームオーバー
        }
    }

    public void OnBlockPlaced()
    {
        var boardManager = FindFirstObjectByType<BoardManager>();
        var placer = boardManager.GetPlacer();
        var gameOverManager = FindFirstObjectByType<GameOverManager>();

        // すべて使い切った場合は判定スキップ
        if (AllPointsEmpty())
        {
            var scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.EndTurn();
            }

            SpawnBlocks();
            return; // ← これ重要！！ ここで以降の処理を止める
        }

        // 残りブロック確認（ターン中）
        bool canPlaceAny = false;
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.childCount == 0) continue;

            var block = spawnPoint.GetChild(0);
            var data = block.GetComponent<BlockComponent>().data;

            if (placer.CanPlaceBlockAnywhere(data.shape))
            {
                canPlaceAny = true;
                break;
            }
        }

        // 残りがすべて置けないならゲームオーバー
        if (!canPlaceAny)
        {
            if (gameOverManager != null)
            {
                gameOverManager.TriggerGameOver();
            }
            return;
        }
    }

    public void ClearSpawnedBlocks()
    {
        foreach (Transform child in transform) // BlockSpawnerの子（SpawnerPoint_xxx）に対して
        {
            if (child.name.StartsWith("SpawnerPoint"))
            {
                if (child.childCount > 0)
                {
                    Destroy(child.GetChild(0).gameObject);
                }
            }
        }
    }
}