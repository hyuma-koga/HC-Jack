using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    [SerializeField] private GameObject blockUnitPrefab;
    [SerializeField] private Sprite defaultSprite;

    public Sprite DefaultSprite => defaultSprite;

    public GameObject CreateBlock(BlockData data)
    {
        var blockRoot = new GameObject("BlockPiece");

        Sprite selectedSprite = null;

        if (data.blockSprites != null && data.blockSprites.Length > 0)
        {
            selectedSprite = data.blockSprites[Random.Range(0, data.blockSprites.Length)];
        }
        else if (defaultSprite != null)
        {
            selectedSprite = defaultSprite;
        }

        if (data.shape == null)
        {
            Debug.LogWarning("BlockData.shape Ç™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
            return blockRoot;
        }

        for (int y = 0; y < data.size.y; y++)
        {
            for (int x = 0; x < data.size.x; x++)
            {
                if (!data.shape[x, y]) continue;

                var unit = Instantiate(blockUnitPrefab, blockRoot.transform);
                unit.transform.localPosition = new Vector3(x, -y, 0);

                var renderer = unit.GetComponent<SpriteRenderer>();
                if (renderer != null && selectedSprite != null)
                {
                    renderer.sprite = selectedSprite;
                }
            }
        }

        return blockRoot;
    }
}