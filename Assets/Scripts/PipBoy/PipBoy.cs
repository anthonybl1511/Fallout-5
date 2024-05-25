using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PipBoy : MonoBehaviour
{
    public static PipBoy instance;
    private Animator pipBoyAnim;
    private Animator handAnim;
    private Animator radioMoletteAnim;
    private GameObject[] menuTabs;
    private GameObject[] menuScreens;

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


    private void Start()
    {
        instance = this;


        pipBoyAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
        handAnim = GameObject.Find("R_wrist").GetComponent<Animator>();
        radioMoletteAnim = GameObject.Find("FrameDialTune").GetComponent<Animator>();
        menuTabs = GameObject.FindGameObjectsWithTag("tabs");
        menuScreens = GameObject.FindGameObjectsWithTag("screens");

        ResetToNeutral();

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != menuTabs.Length - 1)
            {
                menuTabs[i].SetActive(false);
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

    public void Click()
    {

    }

    private void UpdateTab()
    {
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
    }

    public void ChangeIndex(int index)
    {
        if(pipboyActive)
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
