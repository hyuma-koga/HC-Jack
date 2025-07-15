using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private BoardGenerator generator;
    [SerializeField] private BoardPlacer    placer;
    [SerializeField] private Transform      boardSpawnPoint;
    [SerializeField] private float          cellSize = 0.5f;
    [SerializeField] private Transform      boardBlocksRoot;

    public Transform BoardBlocksRoot => boardBlocksRoot;
    public Vector3   BoardOrigin => boardSpawnPoint != null ? boardSpawnPoint.position : Vector3.zero;
    public float     CellSize => cellSize;

    private void Awake()
    {
        if (placer == null)
        {
            placer = GetComponent<BoardPlacer>();
        }

        if (generator == null)
        {
            generator = GetComponent<BoardGenerator>();
        }
    }

    private void Start()
    {
        generator.GenerateBoard();
    }

    // �z�u����C���^�[�t�F�[�X
    public bool TryPlaceBlock(bool[,] shape, int startX, int startY, GameObject blockObj)
    {
        if (placer.CanPlaceBlock(shape, startX, startY))
        {
            placer.PlaceBlock(shape, startX, startY, blockObj);
            return true;
        }

        return false;
    }

    // ���[���h���W���{�[�h�O���b�h���W�ɕϊ�
    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        Vector3 localPos = worldPos - BoardOrigin;

        int gridX = Mathf.RoundToInt(localPos.x / cellSize);
        int gridY = Mathf.RoundToInt(-localPos.y / cellSize);

        return new Vector2Int(gridX, gridY);
    }
}