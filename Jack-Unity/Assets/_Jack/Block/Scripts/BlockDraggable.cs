using UnityEngine;

public class BlockDraggable : MonoBehaviour
{
    private Vector3 offset;
    private Vector3 startPosition;
    private Camera  mainCamera;
    private bool    isLocked = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (isLocked)
        {
            return;
        }

        SetScale(1f);
        startPosition = transform.position;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }

    private void OnMouseDrag()
    {
        if (isLocked)
        {
            return;
        }

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0) + offset;
    }

    private void OnMouseUp()
    {
        if (isLocked)
        {
            return;
        }

        var boardManager = FindFirstObjectByType<BoardManager>();
        var placer = boardManager.GetPlacer();
        var remover = boardManager.GetComponent<BlockRemover>();
        var scoreManager = FindFirstObjectByType<ScoreManager>();

        if (boardManager == null || placer == null || remover == null || scoreManager == null)
        {
            return;
        }

        var blockData = GetComponent<BlockComponent>().data;

        Vector2Int gridPos = boardManager.WorldToGrid(transform.position);
        int gridX = gridPos.x;
        int gridY = gridPos.y;

        if (boardManager.TryPlaceBlock(blockData.shape, gridX, gridY, this.gameObject))
        {
            float snappedX = gridX * boardManager.CellSize + boardManager.BoardOrigin.x;
            float snappedY = -gridY * boardManager.CellSize + boardManager.BoardOrigin.y;
            transform.position = new Vector3(snappedX, snappedY, transform.position.z);

            // 親をボードのブロックルートに変更
            transform.SetParent(boardManager.BoardBlocksRoot);
            isLocked = true;

            // 行列チェック
            var fullRows = placer.GetFullRows();
            var fullCols = placer.GetFullColumns();

            int linesCleared = fullRows.Count + fullCols.Count;
            int blockCells = CountFilledCells(blockData.shape);

            scoreManager.AddPlaceScore(blockCells);

            if (fullRows.Count > 0)
            {
                placer.ClearRows(fullRows);
                remover.RemoveBlocksInRows(fullRows, boardManager);
            }

            if (fullCols.Count > 0)
            {
                placer.ClearColumns(fullCols);
                remover.RemoveBlocksInColumns(fullCols, boardManager);
            }

            scoreManager.AddLineClearScore(linesCleared);

            var spawner = FindFirstObjectByType<BlockSpawner>();
            if (spawner != null)
            {
                spawner.OnBlockPlaced();
            }
        }
        else
        {
            transform.position = startPosition;
            SetScale(0.5f);
        }
    }

    private int CountFilledCells(bool[,] shape)
    {
        int count = 0;
        int width = shape.GetLength(0);
        int height = shape.GetLength(1);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (shape[x, y])
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void SetScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}