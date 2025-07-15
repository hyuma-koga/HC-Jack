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
        if (isLocked) return;

        startPosition = transform.position;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }

    private void OnMouseDrag()
    {
        if (isLocked) return;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0) + offset;
    }

    private void OnMouseUp()
    {
        if (isLocked) return;

        var boardManager = FindFirstObjectByType<BoardManager>();
        if (boardManager == null)
        {
            return;
        }

        var blockData = GetComponent<BlockComponent>().data;

        Vector2Int gridPos = boardManager.WorldToGrid(transform.position);
        int gridX = gridPos.x;
        int gridY = gridPos.y;

        if (boardManager.TryPlaceBlock(blockData.shape, gridX, gridY, this.gameObject))
        {
            Debug.Log("”z’uŠ®—¹");

            float snappedX = gridX * boardManager.CellSize + boardManager.BoardOrigin.x;
            float snappedY = -gridY * boardManager.CellSize + boardManager.BoardOrigin.y;
            transform.position = new Vector3(snappedX, snappedY, transform.position.z);

            transform.SetParent(boardManager.BoardBlocksRoot);

            isLocked = true;

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