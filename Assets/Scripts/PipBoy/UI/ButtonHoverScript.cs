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
    private Animator handAnim;

    [SerializeField] private AudioSource uiSounds;
    [SerializeField] private AudioSource uiSounds2;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip unSelectSound;

    private string initalText;
    void Start()
    {
        handAnim = GameObject.Find("R_wrist").GetComponent<Animator>();

        bgImage = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        initalText = text.text;

        bgImage.enabled = false;
        text.color = Color.green;
    }

    public void ChangeButtonState(bool state)
    {
        mouseHover = state;

        if(state)
        {
            uiSounds2.clip = hoverSound;
            uiSounds2.Play();
        }
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
            handAnim.SetTrigger("validate");

            uiSounds.clip = unSelectSound;
            uiSounds.Play();

            isEquiped = false;
            text.text = initalText;
        }
        else
        {
            handAnim.SetTrigger("validate");

            uiSounds.clip = selectSound;
            uiSounds.Play();

            isEquiped = true;
            text.text = "■ " + initalText;
        }
    }
}
