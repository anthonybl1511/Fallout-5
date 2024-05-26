using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverScript : MonoBehaviour
{
    private Image bgImage;
    private TextMeshProUGUI text;
    private bool isEquiped;
    private bool mouseHover;

    private string initalText;
    void Start()
    {
        bgImage = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        initalText = text.text;

        bgImage.enabled = false;
        text.color = Color.green;
    }

    public void ChangeButtonState(bool state)
    {
        mouseHover = state;
    }

    private void Update()
    {
        if(mouseHover || EventSystem.current.currentSelectedGameObject == gameObject)
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

    public void ButtonClicked()
    {

        if(isEquiped)
        {
            isEquiped = false;

            text.text = initalText;
        }
        else
        {
            isEquiped = true;
            text.text = "■ " + initalText;
        }
    }
}
