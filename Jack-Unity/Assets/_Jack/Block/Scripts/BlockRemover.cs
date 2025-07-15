using UnityEngine;
using System.Collections.Generic;

public class BlockRemover : MonoBehaviour
{
    [SerializeField] private Transform boardBlocksRoot;

    public void RemoveBlocksInRows(List<int> rows, BoardManager boardManager)
    {
        foreach (Transform block in boardBlocksRoot)
        {
            var comp = block.GetComponent<BlockComponent>();

            if (comp == null)
            {
                continue;
            }

            Vector2Int gridPos = boardManager.WorldToGrid(block.position);

            var unitList = new List<Transform>();

            foreach (Transform child in block)
            {
                unitList.Add(child);
            }

            foreach (var child in unitList)
            {
                Vector3 childWorldPos = child.position;
                Vector2Int childGrid = boardManager.WorldToGrid(childWorldPos);

                if (rows.Contains(childGrid.y))
                {
                    Destroy(child.gameObject);
                    boardManager.GetPlacer().SetOccupied(childGrid.x, childGrid.y, false);
                }
            }
        }
    }

    public void RemoveBlocksInColumns(List<int> columns, BoardManager boardManager)
    {
        foreach (Transform block in boardBlocksRoot)
        {
            var comp = block.GetComponent<BlockComponent>();
            if (comp == null)
            {
                continue;
            }

            Vector2Int gridPos = boardManager.WorldToGrid(block.position);

            var unitList = new List<Transform>();

            foreach (Transform child in block)
            {
                unitList.Add(child);
            }

            foreach (var child in unitList)
            {
                Vector3 childWorldPos = child.position;
                Vector2Int childGrid = boardManager.WorldToGrid(childWorldPos);

                if (columns.Contains(childGrid.x))
                {
                    Destroy(child.gameObject);
                    boardManager.GetPlacer().SetOccupied(childGrid.x, childGrid.y, false);
                }
            }
        }
    }
}