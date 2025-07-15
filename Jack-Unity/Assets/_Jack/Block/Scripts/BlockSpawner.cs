using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [SerializeField] private BlockFactory factory;
    [SerializeField] private BlockData[] blockDataList;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        SpawnBlocks();
    }

    private void SpawnBlocks()
    {
        var shapes = ShapeGenerator.GenerateAllShapes();

        for (int i = 0; i < spawnPoints.Length && i < shapes.Count && i < blockDataList.Length; i++)
        {
            // BlockData をコピー
            var data = ScriptableObject.Instantiate(blockDataList[i]);

            // 形状をランダム設定
            var randomShape = shapes[Random.Range(0, shapes.Count)];
            data.SetShape(randomShape);

            // Sprite は blockDataList[i] からそのまま使う（ここでは何もしない！）

            var block = factory.CreateBlock(data);
            block.transform.SetParent(spawnPoints[i], false);
            block.transform.localPosition = Vector3.zero;
        }
    }
}