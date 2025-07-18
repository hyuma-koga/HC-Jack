using UnityEngine;
using System.Collections.Generic;

public class BlockRemover : MonoBehaviour
{
    [SerializeField] private Transform boardBlocksRoot;

    public int RemoveBlocksInRows(List<int> rows, BoardManager boardManager)
    {
        int removedCells = 0;

        foreach (Transform block in boardBlocksRoot)
        {
            var comp = block.GetComponent<BlockComponent>();
            if (comp == null)
            {
                continue;
            }

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
                    removedCells++;
                }
            }
        }

        return removedCells;
    }

    public int RemoveBlocksInColumns(List<int> columns, BoardManager boardManager)
    {
        int removedCells = 0;

        foreach (Transform block in boardBlocksRoot)
        {
            var comp = block.GetComponent<BlockComponent>();

            if (comp == null)
            {
                continue;
            }

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
                    removedCells++;
                }
            }
        }

        return removedCells;
    }
}