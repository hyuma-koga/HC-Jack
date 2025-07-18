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

    public void OnClickClose()
    {
        Hide();
    }

    public void OnClickSaveAndReturnToTitle()
    {
        gameFlowManager.OnSaveAndReturnToTitle();
        Hide();
    }

    public void OnClickRetry()
    {
        gameFlowManager.OnRetryRequested();
        Hide();
    }
}