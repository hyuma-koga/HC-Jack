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

    //����{�^��
    public void OnClickClose()
    {
        Hide();
    }

    //�Z�[�u���ă^�C�g���ɖ߂�{�^��
    public void OnClickSaveAndReturnToTitle()
    {
        gameFlowManager.OnSaveAndReturnToTitle();
        Hide();
    }

    //���g���C�{�^��
    public void OnClickRetry()
    {
        gameFlowManager.OnRetryRequested();
        Hide();
    }
}