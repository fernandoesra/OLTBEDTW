using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogAreaController : MonoBehaviour
{
    public static LogAreaController Instance;
    [SerializeField] TextMeshProUGUI TextLog;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private RectTransform contentRectTransform;

    void Awake()
    {
        LogAreaController.Instance = this;
        TextLog.text = "";
    }

    public void Reset()
    {
        TextLog.text = "";
        float contentHeight = contentRectTransform.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float newPosition = (contentHeight - viewportHeight) * -1;

        scrollRect.verticalNormalizedPosition = Mathf.InverseLerp(0, contentHeight - viewportHeight, newPosition);
    }

    public void AddText(string text)
    {
        TextLog.text += text + "*\n";
        Canvas.ForceUpdateCanvases();

        float contentHeight = contentRectTransform.rect.height;
        float viewportHeight = scrollRect.viewport.rect.height;
        float newPosition = (contentHeight - viewportHeight) * -1;

        scrollRect.verticalNormalizedPosition = Mathf.InverseLerp(0, contentHeight - viewportHeight, newPosition);
    }

}
