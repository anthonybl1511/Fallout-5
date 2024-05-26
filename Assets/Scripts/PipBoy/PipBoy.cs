using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class PipBoy : MonoBehaviour
{
    public static PipBoy instance;
    private Animator pipBoyAnim;
    private Animator handAnim;
    private Animator radioMoletteAnim;
    [SerializeField] private GameObject[] menuTabs;
    [SerializeField] private GameObject[] menuScreens;
    

    private SubTabsManager activeSubTab;

    private int menuIndex;
    private int radioIndex;
    private bool radioActive;
    private bool pipboyActive;


    [SerializeField] private AudioSource UISounds;
    [SerializeField] private AudioSource navigationSounds;

    [SerializeField] private AudioClip[] driveAndStaticsSounds;
    [SerializeField] private AudioClip tabLeftSound;
    [SerializeField] private AudioClip tabRightSound;
    [SerializeField] private AudioClip pibboyUp;
    [SerializeField] private AudioClip pibboyDown;

    [SerializeField] private InputManager inputManager;

    private GameObject pipboyScreenRef;
    private LensDistortion lensDistortion;

    private void Start()
    {
        instance = this;

        pipboyScreenRef = GameObject.Find("ScreenCamera");
        pipBoyAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
        handAnim = GameObject.Find("R_wrist").GetComponent<Animator>();
        radioMoletteAnim = GameObject.Find("FrameDialTune").GetComponent<Animator>();

        if (pipboyScreenRef.GetComponent<Volume>().profile.TryGet<LensDistortion>(out LensDistortion ld))
        {
            lensDistortion = ld;
        }

        ResetToNeutral();

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != menuTabs.Length - 1)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                if (menuScreens[i].transform.GetChild(0).gameObject.name == "horizontalSubTabs")
                {
                    activeSubTab = menuScreens[i].transform.GetChild(0).gameObject.GetComponent<SubTabsManager>();
                }
                else
                {
                    activeSubTab = null;
                }
            }
        }

        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i != menuScreens.Length - 1)
            {
                menuScreens[i].SetActive(false);
            }
        }

    }
    IEnumerator FlickerScreen()
    {
        float flickerTime = 0;
        int frameCounter = 0;

        while (flickerTime < 0.15f)
        {
            flickerTime += Time.deltaTime;
            frameCounter++;

            if (frameCounter >= 3)
            {
                frameCounter = 0;
                float randomF = Random.Range(-1f, 1f);
                ClampedFloatParameter intensityParameter = new ClampedFloatParameter(randomF, -0.5f, 0.5f);
                lensDistortion.intensity.SetValue(intensityParameter);
                pipboyScreenRef.transform.GetChild(0).GetChild(0).transform.localPosition = new Vector3(0, randomF * 100, 0);
            }
            yield return null;
        }
        ClampedFloatParameter finalIntensity = new ClampedFloatParameter(0, -1, 1);
        lensDistortion.intensity.SetValue(finalIntensity);
        pipboyScreenRef.transform.GetChild(0).GetChild(0).transform.localPosition = Vector3.zero;
    }

    public void Click()
    {
        //Rect deadZone = new Rect(0, 0, Screen.width / 2, Screen.height / 2);
        //float scaleFactor = 5;
        //// Get the mouse position in screen space
        //Vector2 mousePosition = Input.mousePosition;

        //// Calculate the scale factor
        //float scaleFactorX = 512 / Screen.width;
        //float scaleFactorY = 512 / Screen.height;
        // scaleFactor = Mathf.Min(scaleFactorX, scaleFactorY);

        //// Divide the mouse position by the scale factor and center it
        //mousePosition = (mousePosition - new Vector2(Screen.width / 2, Screen.height / 2)) * scaleFactor + new Vector2(512 / 2, 512 / 2);

        //// Check if the mouse position is inside the pipboy screen and outside the dead zone
        //if (pipboyScreenRef.transform.GetChild(0).GetComponent<RectTransform>().rect.Contains(mousePosition) && !deadZone.Contains(mousePosition))
        //{
        //    // Create a temporary PointerEventData object
        //    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        //    pointerEventData.position = mousePosition;

        //    // Get all UI elements under the mouse cursor
        //    List<RaycastResult> results = new List<RaycastResult>();
        //    EventSystem.current.RaycastAll(pointerEventData, results);

        //    // Check if any UI element is on the pipboy screen camera
        //    foreach (RaycastResult result in results)
        //    {
        //        if (result.gameObject.GetComponent<Canvas>() != null &&
        //            result.gameObject.GetComponent<Canvas>().worldCamera == pipboyScreenRef.GetComponent<Camera>())
        //        {
        //            // Invoke the button's onClick method
        //            if (result.gameObject.GetComponent<Button>() != null)
        //            {
        //                result.gameObject.GetComponent<Button>().onClick.Invoke();
        //            }
        //        }
        //        print(result.worldPosition);
        //    }
        //}

    }

    private void UpdateTab()
    {

        int playAnim = Random.Range(0, 2);

        if(playAnim == 0)
        {
            StartCoroutine(FlickerScreen());
        }

        pipBoyAnim.SetTrigger("changeTab");
        handAnim.SetTrigger("changeTab");

        UISounds.clip = driveAndStaticsSounds[Random.Range(0, driveAndStaticsSounds.Length)];
        UISounds.Play();

        navigationSounds.clip = tabRightSound;
        navigationSounds.Play();

        switch (menuIndex)
        {
            case 0:
                GoToStats();
                break;
            case 1:
                GoToInv();
                break;
            case 2:
                GoToData();
                break;
            case 3:
                GoToMap();
                break;
            case 4:
                GoToRadio();
                break;
        }
        if (menuScreens[(menuScreens.Length - 1) - menuIndex].transform.GetChild(0).gameObject.GetComponent<SubTabsManager>() != null)
        {
            activeSubTab = menuScreens[(menuScreens.Length - 1) - menuIndex].transform.GetChild(0).gameObject.GetComponent<SubTabsManager>();
            
        }
        else
        {
            activeSubTab = null;
        }
    }

    public void ChangeSubTabs(int index)
    {
        if (activeSubTab != null)
        {
            activeSubTab.switchIndex(index);
        }
    }

    public void ChangeIndex(int index)
    {
        if (pipboyActive)
        {
            menuIndex += index;
            if(menuIndex > 4 || menuIndex < 0)
            {
                menuIndex = Mathf.Clamp(menuIndex, 0, 4);
            }
            else
            {
                UpdateTab();
            }
        }
    }


    public void SetIndex(int index)
    {
        if (pipboyActive)
        {
            if(index != menuIndex)
            {
                menuIndex = index;
                UpdateTab();
            }
        }
    }

    public void navigate(int index)
    {
        if (pipboyActive) { 
            if(radioActive)
            {
                if (index > 0)
                {
                    if (radioIndex < 1)
                    {
                        radioIndex++;
                        radioMoletteAnim.SetBool("right", true);
                        radioMoletteAnim.SetBool("left", false);

                        handAnim.SetBool("rightRadio", false);
                        handAnim.SetBool("leftRadio", true);
                    }
                }
                if (index < 0)
                {
                    if (radioIndex > 0)
                    {
                        radioIndex--;
                        radioMoletteAnim.SetBool("right", false);
                        radioMoletteAnim.SetBool("left", true);

                        handAnim.SetBool("rightRadio", true);
                        handAnim.SetBool("leftRadio", false);
                    }
                }
            }
            else
            {
                 
            }
        }
    }

    public void OpenClose()
    {
        if (pipboyActive)
        {
            navigationSounds.clip = pibboyDown;
            navigationSounds.Play();

            GameObject.Find("Hands").GetComponent<Animator>().enabled = true;
            GameObject.Find("Hands").GetComponent<Animator>().SetTrigger("close");
            pipboyActive = false;

            ResetToNeutral();
        }
        else
        {
            navigationSounds.clip = pibboyUp;
            navigationSounds.Play();

            GameObject.Find("Hands").GetComponent<Animator>().enabled = true;
            GameObject.Find("Hands").GetComponent<Animator>().SetTrigger("open");
            pipboyActive = true;
        }
    }

    public bool getActive()
    {
        return pipboyActive;
    }

    private void ResetToNeutral()
    {
        radioMoletteAnim.SetBool("right", true);
        radioMoletteAnim.SetBool("left", false);
        handAnim.SetBool("rightRadio", false);
        handAnim.SetBool("leftRadio", true);
        pipBoyAnim.SetBool("stat", true);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);
        handAnim.SetBool("stat", true);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToStats()
    {
        radioActive = false;
        for (int i = 0; i < menuTabs.Length; i++)
        {
            if(i != 4)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                menuTabs[i].SetActive(true);
            }
        }
        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i != 4)
            {
                menuScreens[i].SetActive(false);
            }
            else
            {
                menuScreens[i].SetActive(true);
            }
        }
        pipBoyAnim.SetBool("stat", true);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stat", true);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToInv()
    {
        radioActive = false;

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != 3)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                menuTabs[i].SetActive(true);
            }
        }

        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i != 3)
            {
                menuScreens[i].SetActive(false);
            }
            else
            {
                menuScreens[i].SetActive(true);
            }
        }
        pipBoyAnim.SetBool("stat", false);
        pipBoyAnim.SetBool("inv", true);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stat", false);
        handAnim.SetBool("inv", true);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToData()
    {
        radioActive = false;

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != 2)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                menuTabs[i].SetActive(true);
            }
        }
        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i != 2)
            {
                menuScreens[i].SetActive(false);
            }
            else
            {
                menuScreens[i].SetActive(true);
            }
        }

        pipBoyAnim.SetBool("stat", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", true);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stat", false);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", true);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToMap()
    {
        radioActive = false;

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != 1)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                menuTabs[i].SetActive(true);
            }
        }
        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i !=1)
            {
                menuScreens[i].SetActive(false);
            }
            else
            {
                menuScreens[i].SetActive(true);
            }
        }
        pipBoyAnim.SetBool("stat", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", true);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stat", false);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", true);
        handAnim.SetBool("radio", false);
    }
    private void GoToRadio()
    {
        radioActive = true;

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != 0)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                menuTabs[i].SetActive(true);
            }
        }
        for (int i = 0; i < menuScreens.Length; i++)
        {
            if (i != 0)
            {
                menuScreens[i].SetActive(false);
            }
            else
            {
                menuScreens[i].SetActive(true);
            }
        }

        pipBoyAnim.SetBool("stat", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", true);

        handAnim.SetBool("stat", false);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", true);
    }
}
