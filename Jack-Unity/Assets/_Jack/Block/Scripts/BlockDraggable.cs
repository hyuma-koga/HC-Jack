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

            //親オブジェクトをボードのブロックルートに変更
            transform.SetParent(boardManager.BoardBlocksRoot);
            isLocked = true;

            //行列をチェック
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

            if (linesCleared > 0)
            {
                Vector3 popupPos = CalculatePopupPosition(boardManager, fullRows, fullCols);
                scoreManager.AddLineClearScore(linesCleared, popupPos);
            }

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

    private Vector3 CalculatePopupPosition(BoardManager boardManager, System.Collections.Generic.List<int> fullRows, System.Collections.Generic.List<int> fullCols)
    {
        Vector3 totalPos = Vector3.zero;
        int count = 0;

        float cellSize = boardManager.CellSize;
        Vector3 origin = boardManager.BoardOrigin;

        //行の中心を加算
        foreach (int y in fullRows)
        {
            for (int x = 0; x < 8; x++)  //boardSizeが8
            {
                float px = origin.x + x * cellSize;
                float py = origin.y - y * cellSize;
                totalPos += new Vector3(px, py, 0);
                count++;
            }
        }

        //列の中心を加算
        foreach (int x in fullCols)
        {
            for (int y = 0; y < 8; y++)
            {
                float px = origin.x + x * cellSize;
                float py = origin.y - y * cellSize;
                totalPos += new Vector3(px, py, 0);
                count++;
            }
        }

        if (count == 0)
        {
            return origin;
        }

        return totalPos / count;
    }
}