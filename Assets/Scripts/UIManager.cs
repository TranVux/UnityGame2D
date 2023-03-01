using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplayCoin;
    void Start()
    {
        if (textDisplayCoin != null)
        {
            if (PlayerConstantManager.GetInstance().coins < 0)
            {
                textDisplayCoin.SetText("x0");
            }
            else
            {
                textDisplayCoin.SetText("x" + PlayerConstantManager.GetInstance().coins.ToString());
            }
        }
    }

    public void UpdateUI(int coins)
    {
        //update coin
        if (coins < 0)
        {
            textDisplayCoin.SetText("x0");
        }
        textDisplayCoin.SetText("x" + coins.ToString());
    }
}
