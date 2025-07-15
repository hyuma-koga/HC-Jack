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
            // BlockData ���R�s�[
            var data = ScriptableObject.Instantiate(blockDataList[i]);

            // �`��������_���ݒ�
            var randomShape = shapes[Random.Range(0, shapes.Count)];
            data.SetShape(randomShape);

            // Sprite �� blockDataList[i] ���炻�̂܂܎g���i�����ł͉������Ȃ��I�j

            var block = factory.CreateBlock(data);
            block.transform.SetParent(spawnPoints[i], false);
            block.transform.localPosition = Vector3.zero;
        }
    }
}