using TMPro;
using UnityEngine;

public class ComboPopupUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab; // ComboPopup（Text2つだけ）のプレハブ
    [SerializeField] private Transform popupRoot;

    public void ShowPopup(Vector3 worldPos, int combo, int score)
    {
        if (popupPrefab == null || popupRoot == null)
        {
            Debug.LogError("popupPrefab または popupRoot が未設定");
            return;
        }

        GameObject popup = Instantiate(popupPrefab, popupRoot);

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("MainCamera が見つかりません");
            return;
        }

        // ワールド座標 → スクリーン座標
        Vector2 screenPos = cam.WorldToScreenPoint(worldPos);
        popup.transform.position = screenPos; // anchoredPosition を使わない！

        Debug.Log($"Popup screen position: {screenPos}");

        // テキスト設定
        TMP_Text scoreText = popup.transform.Find("Text_ScoreText")?.GetComponent<TMP_Text>();
        TMP_Text comboText = popup.transform.Find("Text_ComboText")?.GetComponent<TMP_Text>();

        if (scoreText != null)
            scoreText.text = $"+{score}";

        if (comboText != null)
            comboText.text = combo switch
            {
                0 => "",
                1 => "Combo！",
                _ => $"Combo {combo}"
            };

        Destroy(popup, 1.2f);
    }

}