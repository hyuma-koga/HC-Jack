using UnityEngine;

public class BlockFactory : MonoBehaviour
{
    [SerializeField] private GameObject blockUnitPrefab;
    [SerializeField] private Sprite     defaultSprite;

    public Sprite                       DefaultSprite => defaultSprite;
    public float                        spacing = 0.5f;
   
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
            return blockRoot;
        }

        int width = data.size.x;
        int height = data.size.y;

        //子ブロック生成
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!data.shape[x, y])
                {
                    continue;
                }

                var unit = Instantiate(blockUnitPrefab, blockRoot.transform);
                unit.transform.localPosition = new Vector3(x * spacing, -y * spacing, 0);
                var renderer = unit.GetComponent<SpriteRenderer>();

                if (renderer != null && selectedSprite != null)
                {
                    renderer.sprite = selectedSprite;
                }

                //子にはColliderを追加しない(バラバラになる)
                var childCollider = unit.GetComponent<Collider2D>();

                if (childCollider != null)
                {
                    DestroyImmediate(childCollider);
                }
            }
        }

        var collider = blockRoot.AddComponent<BoxCollider2D>();

        float colliderWidth = (data.size.x) * spacing;
        float colliderHeight = (data.size.y) * spacing;

        collider.size = new Vector2(colliderWidth, colliderHeight);
        collider.offset = new Vector2(colliderWidth / 2f - 0.5f * spacing, -(colliderHeight / 2f) + 0.5f * spacing);

        var comp = blockRoot.AddComponent<BlockComponent>();
        comp.data = data;

        blockRoot.AddComponent<BlockDraggable>();
        return blockRoot;
    }
}