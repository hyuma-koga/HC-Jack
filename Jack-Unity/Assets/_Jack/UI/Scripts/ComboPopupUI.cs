using TMPro;
using UnityEngine;

public class ComboPopupUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Transform  popupRoot;

    public void ShowPopup(Vector3 worldPos, int combo, int score)
    {
        if (popupPrefab == null || popupRoot == null)
        {
            return;
        }

        GameObject popup = Instantiate(popupPrefab, popupRoot);
        Camera cam = Camera.main;

        if (cam == null)
        {
            return;
        }

        //ワールド座標からスクリーン座標へ
        Vector2 screenPos = cam.WorldToScreenPoint(worldPos);
        popup.transform.position = screenPos;

        TMP_Text scoreText = popup.transform.Find("Text_ScoreText")?.GetComponent<TMP_Text>();
        TMP_Text comboText = popup.transform.Find("Text_ComboText")?.GetComponent<TMP_Text>();

        if (scoreText != null)
        {
            scoreText.text = $"+{score}";
        }

        if (comboText != null)
        {
            comboText.text = combo switch
            {
                0 => "",
                1 => "Combo!",
                _ => $"Combo {combo}"
            };
        }

        Destroy(popup, 1.2f);
    }
}