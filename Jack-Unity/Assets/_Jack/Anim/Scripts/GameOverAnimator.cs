using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimator : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform  cellRoot;
    [SerializeField] private Transform  originTransform;
    [SerializeField] private Sprite[]   cellSprites;
    [SerializeField] private float      cellSize = 0.5f;
    [SerializeField] private float      rowDelay = 0.05f;
    [SerializeField] private float      waitAfterCover = 2f;
    [SerializeField] private int        boardSize = 8;

    public Action                       onAnimationComplete;
    private List<GameObject>            createdCells = new List<GameObject>();

    public void Play()
    {
        StartCoroutine(AnimateGameOverCover());
    }

    private IEnumerator AnimateGameOverCover()
    {
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

                createdCells.Add(cell);
            }

            yield return new WaitForSeconds(rowDelay);
        }

        yield return new WaitForSeconds(waitAfterCover);
        onAnimationComplete?.Invoke();
    }

    public void ClearAnimationCells()
    {
        foreach (var cell in createdCells)
        {
            if (cell != null)
            {
                Destroy(cell);
            }
        }

        createdCells.Clear();
    }
}