using TMPro;
using UnityEngine;

public class ComboPopupUI : MonoBehaviour
{
    [SerializeField] private GameObject popupPrefab; // ComboPopup�iText2�����j�̃v���n�u
    [SerializeField] private Transform popupRoot;

    public void ShowPopup(Vector3 worldPos, int combo, int score)
    {
        if (popupPrefab == null || popupRoot == null)
        {
            Debug.LogError("popupPrefab �܂��� popupRoot �����ݒ�");
            return;
        }

        GameObject popup = Instantiate(popupPrefab, popupRoot);

        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("MainCamera ��������܂���");
            return;
        }

        // ���[���h���W �� �X�N���[�����W
        Vector2 screenPos = cam.WorldToScreenPoint(worldPos);
        popup.transform.position = screenPos; // anchoredPosition ���g��Ȃ��I

        Debug.Log($"Popup screen position: {screenPos}");

        // �e�L�X�g�ݒ�
        TMP_Text scoreText = popup.transform.Find("Text_ScoreText")?.GetComponent<TMP_Text>();
        TMP_Text comboText = popup.transform.Find("Text_ComboText")?.GetComponent<TMP_Text>();

        if (scoreText != null)
            scoreText.text = $"+{score}";

        if (comboText != null)
            comboText.text = combo switch
            {
                0 => "",
                1 => "Combo�I",
                _ => $"Combo {combo}"
            };

        Destroy(popup, 1.2f);
    }

}