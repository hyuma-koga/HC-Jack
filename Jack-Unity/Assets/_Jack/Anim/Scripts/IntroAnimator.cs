using System;
using System.Collections;
using UnityEngine;

public class IntroAnimator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform  cellRoot;
    [SerializeField] private Transform  originTransform;
    [SerializeField] private Sprite[]   cellSprites;
    [SerializeField] private int        boardSize = 8;
    [SerializeField] private float      cellSize = 0.5f;
    [SerializeField] private float      rowDelay = 0.05f;
  
    public Action OnAnimationComplete;

    public void Play()
    {
        StartCoroutine(AnimateBoardIntro());
    }

    private IEnumerator AnimateBoardIntro()
    {
        GameObject[,] grid = new GameObject[boardSize, boardSize];
        Vector3 origin = originTransform != null ? originTransform.position : Vector3.zero;

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

        yield return new WaitForSeconds(rowDelay * 2f);

        for (int y = 0; y < boardSize; y++)
        {
            for (int x = 0; x < boardSize; x++)
            {
                Destroy(grid[x, y]);
            }

            yield return new WaitForSeconds(rowDelay);
        }

        OnAnimationComplete?.Invoke();
    }
}