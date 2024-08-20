using System.Collections;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    public static TextMeshProUGUI hint;
    void Start()
    {
        hint = GetComponent<TextMeshProUGUI>();
    }

    internal static IEnumerator HintCoroutine(string text, int duration)
    {
        hint.text = text;
        yield return new WaitForSeconds(duration);
        hint.text = "";
    }
    internal static void SetHint(string text)
    {
        hint.text = text;
    }
    internal static void EraseHint()
    {
        hint.text = "";
        hint.fontSize = 16;
    }
}
