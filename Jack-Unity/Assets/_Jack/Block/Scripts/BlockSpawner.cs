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

    public void OnBlockPlaced()
    {
        if (AllPointsEmpty())
        {
            SpawnBlocks();
        }
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

    private void SpawnBlocks()
    {
        var shapes = ShapeGenerator.GenerateAllShapes();

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
    }
}