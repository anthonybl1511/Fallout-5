using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using System;


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
    [SerializeField] private AudioClip subTabLeftSound;
    [SerializeField] private AudioClip subTabRightSound;
    [SerializeField] private AudioClip pibboyUp;
    [SerializeField] private AudioClip pibboyDown;

    [SerializeField] private InputManager inputManager;

    private GameObject pipboyScreenRef;
    private LensDistortion lensDistortion;

    private Vector3 localPos;

    private void Start()
    {
        instance = this;

        pipboyScreenRef = GameObject.Find("ScreenCamera");
        pipBoyAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
        handAnim = GameObject.Find("R_wrist").GetComponent<Animator>();
        radioMoletteAnim = GameObject.Find("FrameDialTune").GetComponent<Animator>();
        localPos = pipboyScreenRef.transform.GetChild(0).GetChild(0).transform.localPosition;

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

    private void Update()
    {
        if(Gamepad.current != null)
        {

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
                pipboyScreenRef.transform.GetChild(0).GetChild(0).transform.localPosition = new Vector3(localPos.x, randomF * 100, localPos.y);
            }
            yield return null;
        }
        ClampedFloatParameter finalIntensity = new ClampedFloatParameter(0, -1, 1);
        lensDistortion.intensity.SetValue(finalIntensity);
        pipboyScreenRef.transform.GetChild(0).GetChild(0).transform.localPosition = localPos;
    }

    public void Click()
    {
      

    }

    private void UpdateTab()
    {

        int playAnim = Random.Range(0, driveAndStaticsSounds.Length);

        if(playAnim >= 5 && playAnim <= 9)
        {
            StartCoroutine(FlickerScreen());
        }

        pipBoyAnim.SetTrigger("changeTab");
        handAnim.SetTrigger("changeTab");

        UISounds.clip = driveAndStaticsSounds[playAnim];
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

    public void SubTabSound(int index)
    {
        handAnim.SetTrigger("changeSubTab");
        if (index < 0)
        {
            UISounds.clip = subTabRightSound;
            UISounds.Play();
        }
        else if (index > 0)
        {
            UISounds.clip = subTabLeftSound;
            UISounds.Play();
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
