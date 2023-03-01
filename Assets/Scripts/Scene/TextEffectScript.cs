using UnityEngine;
using TMPro;

public class TextEffectScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxScale;
    public float minScale;
    public float currentScale = 5f;
    public enum StateScale { Increase, Decrease }
    public StateScale currentStateScale;

    [SerializeField] private TextMeshProUGUI textPlay;

    void Start()
    {
        currentStateScale = StateScale.Increase;
        Invoke("AttempChangeFontSize", 0.1f);
    }

    void ChangeFontSize()
    {
        if (currentStateScale == StateScale.Increase && currentScale <= maxScale)
        {
            currentScale += 0.1f;
        }
        else
        {
            currentStateScale = StateScale.Decrease;
        }

        if (currentStateScale == StateScale.Decrease&& currentScale >= minScale)
        {
            currentScale -= 0.1f;
        }
        else
        {
            currentStateScale = StateScale.Increase;
        }

        textPlay.fontSize = currentScale;

    }

    void AttempChangeFontSize()
    {
        ChangeFontSize();
        Invoke("AttempChangeFontSize", 0.1f);
    }
}
