using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private static Tooltip instance;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        tooltipText = transform.GetComponentInChildren<Text>();
        Hide();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }

    private void Show(string text)
    {
        gameObject.SetActive(true);

        tooltipText.text = text;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.gameObject.GetComponent<RectTransform>().sizeDelta.x + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip(string text)
    {
        instance.Show(text);
    }

    public static void HideTooltip()
    {
        instance.Hide();
    }
}
