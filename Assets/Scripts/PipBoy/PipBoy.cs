using UnityEngine;
using UnityEngine.InputSystem;

public class PipBoy : MonoBehaviour
{
    public static PipBoy instance;
    private Animator pipBoyAnim;
    private Animator handAnim;
    private Animator radioMoletteAnim;
    private GameObject[] menuTabs;
    private int menuIndex;
    private int radioIndex;
    private bool radioActive;
    private bool pipboyActive;
    
    [SerializeField] private InputManager InputManager;


    private void Start()
    {
        instance = this;
        pipBoyAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
        handAnim = GameObject.Find("R_wrist").GetComponent<Animator>();
        radioMoletteAnim = GameObject.Find("FrameDialTune").GetComponent<Animator>();
        menuTabs = GameObject.FindGameObjectsWithTag("tabs");

        ResetToNeutral();

        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != menuTabs.Length - 1)
            {
                menuTabs[i].SetActive(false);
            }
        }

    }

    public void Click()
    {

    }

    public void ChangeIndex(int index)
    {
        if(pipboyActive)
        {
            menuIndex += index;

            menuIndex = Mathf.Clamp(menuIndex, 0, 4);

            pipBoyAnim.SetTrigger("changeTab");
            handAnim.SetTrigger("changeTab");

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
        
    }

    public bool getActive()
    {
        return pipboyActive;
    }

    private void Update()
    {

        if(pipboyActive)
        {
            if (radioActive)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
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
                if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(pipboyActive)
            {
                GameObject.Find("Hands").GetComponent<Animator>().enabled = true;
                GameObject.Find("Hands").GetComponent<Animator>().SetTrigger("close");
                pipboyActive = false;

                ResetToNeutral();
            }
            else
            {
                GameObject.Find("Hands").GetComponent<Animator>().enabled = true;
                GameObject.Find("Hands").GetComponent<Animator>().SetTrigger("open");
                pipboyActive = true;
            }
        }
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
