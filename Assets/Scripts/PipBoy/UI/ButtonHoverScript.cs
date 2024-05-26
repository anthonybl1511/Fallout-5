using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverScript : MonoBehaviour
{
    private Image bgImage;
    private Button button;
    private TextMeshProUGUI text;
    void Start()
    {
        bgImage = GetComponent<Image>();
        button = GetComponent<Button>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject)
        {
            bgImage.enabled = true;
            text.color = Color.black;
        }
        else
        {
            bgImage.enabled = false;
            text.color = Color.green;
        }
    }
}
