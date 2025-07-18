using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int        boardSize = 7;
    [SerializeField] private float      cellSpacing = 1.0f;
    [SerializeField] private Transform  boardSpawnPoint;

    public float                        CellSpacing => cellSpacing;
    public int                          BoardSize => boardSize;
   
    public void GenerateBoard()
    {
        if (cellPrefab == null)
        {
            return;
        }

        if (boardSpawnPoint == null)
        {
            return;
        }

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                var cell = Instantiate(cellPrefab, boardSpawnPoint);
                cell.transform.localPosition = new Vector3(x * cellSpacing, -y * cellSpacing, 0);
            }
        }
    }
}