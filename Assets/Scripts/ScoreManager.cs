using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    public void Increase() => Score++;

    [SerializeField] private Text _textUI;

    public Text Text { get; private set; }

    private void Start()
    {
        Text = _textUI;
    }

    public void UpdateText()
    {
        Text.text = $"Score: {Score}";
    }
}
