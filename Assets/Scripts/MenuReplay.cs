using TMPro;
using UnityEngine;

public class MenuReplay : MonoBehaviour
{
    public TextMeshProUGUI textCoin;
    public TextMeshProUGUI textTitle;
    public GameObject buttonResume;

    private void Awake()
    {
        transform.LeanScale(Vector2.zero, 0f);
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.LeanScale(Vector2.zero, 0f);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    public void Open(int amountOfCoin, string _textTitle)
    {
        textTitle.SetText(_textTitle);
        textCoin.SetText("x" + amountOfCoin);
        transform.LeanScale(Vector2.one, 0.6f).setEaseInOutBack();
    }

    public void Open(string _textTitle)
    {
        textTitle.SetText(_textTitle);
        transform.LeanScale(Vector2.one, 0.6f).setEaseInOutBack();
    }

    public void Close()
    {
        transform.LeanScale(Vector2.zero, 0.6f).setEaseInOutBack();
    }

    public void DisableResumeButton()
    {
        buttonResume.SetActive(false);
    }
}
