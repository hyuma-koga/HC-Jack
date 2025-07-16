using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private int comboCount = 0;

    public void AddCombo(int count = 1)
    {
        comboCount += count;
    }

    public void ResetCombo()
    {
        comboCount = 0;
    }

    public int GetComboCount()
    {
        return comboCount;
    }
}