using UnityEngine;
using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    [SerializeField] private string word;
    private Text _text;

    private void Awake()
    {
        _text = gameObject.GetComponent<Text>();
    }

    public void InitRandom(int sz)
    {
        var aux = "";
        for (var i = 0; i < sz; i++) aux += Utils.GetRandomLetter();
        Word = aux;
    }

    private void UpdateText(string value)
    {
        word = value;
        _text.text = value;
    }

    public string Word
    {
        get => word;
        set => UpdateText(value);
    }
}