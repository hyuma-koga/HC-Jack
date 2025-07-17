using System;
using System.Collections;
using UnityEngine;

public class IntroAnimator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private int boardSize = 8;
    [SerializeField] private float cellSize = 0.5f;
    [SerializeField] private float rowDelay = 0.05f;
    [SerializeField] private Transform cellRoot;
    [SerializeField] private Transform originTransform;
    [SerializeField] private Sprite[] cellSprites;

    public Action OnAnimationComplete;

    public void Play()
    {
        StartCoroutine(AnimateBoardIntro());
    }

    private IEnumerator AnimateBoardIntro()
    {
        GameObject[,] grid = new GameObject[boardSize, boardSize];
        Vector3 origin = originTransform != null ? originTransform.position : Vector3.zero;

        // ★ 1. 下から上に生成
        for (int y = boardSize - 1; y >= 0; y--)
        {
            for (int x = 0; x < boardSize; x++)
            {
                Vector3 pos = origin + new Vector3(x * cellSize, -y * cellSize, 0);
                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity, cellRoot);

                var sr = cell.GetComponent<SpriteRenderer>();
                if (sr != null && cellSprites.Length > 0)
                {
                    sr.sprite = cellSprites[UnityEngine.Random.Range(0, cellSprites.Length)];
                }

                grid[x, y] = cell;
            }
            yield return new WaitForSeconds(rowDelay);
        }

        // 小さな待機
        yield return new WaitForSeconds(rowDelay * 2f);

        // ★ 2. 上から下に削除
        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                Destroy(grid[x, y]);
            }
            yield return new WaitForSeconds(rowDelay);
        }

        // ★ 3. アニメーション完了通知
        OnAnimationComplete?.Invoke();
    }
}