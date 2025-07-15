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

        if (boardManager == null || placer == null || remover == null)
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
           
            //親をボードのブロックルートに変更
            transform.SetParent(boardManager.BoardBlocksRoot);

            isLocked = true;

            var fullRows = placer.GetFullRows();
            var fullCols = placer.GetFullColumns();

            if (fullRows.Count > 0)
            {
                placer.ClearRows(fullRows);
                remover.RemoveBlocksInRows(fullRows, boardManager);
                Debug.Log($"行を削除: {fullRows.Count}");
                //スコア加算処理
            }

            if (fullCols.Count > 0)
            {
                placer.ClearColumns(fullCols);
                remover.RemoveBlocksInColumns(fullCols, boardManager);
                Debug.Log($"列を削除: {fullCols.Count}");
                //スコア加算処理
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
        }
    }
}