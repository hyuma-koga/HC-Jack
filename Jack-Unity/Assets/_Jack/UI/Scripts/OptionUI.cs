using UnityEngine;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private GameObject      optionUI;
    [SerializeField] private GameFlowManager gameFlowManager;

    public void Show()
    {
        optionUI.SetActive(true);
    }

    public void Hide()
    {
        optionUI.SetActive(false);
    }

    //閉じるボタン
    public void OnClickClose()
    {
        Hide();
    }

    //セーブしてタイトルに戻るボタン
    public void OnClickSaveAndReturnToTitle()
    {
        gameFlowManager.OnSaveAndReturnToTitle();
        Hide();
    }

    //リトライボタン
    public void OnClickRetry()
    {
        gameFlowManager.OnRetryRequested();
        Hide();
    }
}