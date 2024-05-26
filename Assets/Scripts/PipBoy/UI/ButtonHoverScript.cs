using System.Collections;
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
    private Animator radioAnim;

    [SerializeField] private AudioSource uiSounds;
    [SerializeField] private AudioSource uiSounds2;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip unSelectSound;
    [SerializeField] private AudioClip radioSelectSound;
    [SerializeField] private AudioClip radioUnselectSound;
    [SerializeField] private AudioSource radioMusic;

    private string initalText;
    void Start()
    {
        radioAnim = GameObject.Find("FrameDialTune").GetComponent<Animator>();
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

    public void ToggleRadio()
    {
        if (isEquiped)
        {
            radioAnim.SetBool("left", false);
            radioAnim.SetBool("right", true);
            handAnim.SetBool("rightRadio", false);
            handAnim.SetBool("leftRadio", true);

            uiSounds.clip = radioUnselectSound;
            uiSounds.Play();

            isEquiped = false;
            text.text = initalText;

            StartCoroutine(AudioFade(radioMusic, 0.2f, 0));
        }
        else
        {
            radioAnim.SetBool("left", true);
            radioAnim.SetBool("right", false);
            handAnim.SetBool("rightRadio", true);
            handAnim.SetBool("leftRadio", false);

            uiSounds.clip = radioSelectSound;
            uiSounds.Play();

            isEquiped = true;
            text.text = "■ " + initalText;

            StartCoroutine(AudioFade(radioMusic, 0.4f, 0.25f));
        }
    }

    public static IEnumerator AudioFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
