using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipBoy : MonoBehaviour
{
    private Animator pipBoyAnim;
    private Animator handAnim;
    private Animator radioMoletteAnim;
    private int menuIndex;
    private int radioIndex;
    private bool radioActive;

    private void Start()
    {
        pipBoyAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
        handAnim = GameObject.Find("R_wrist").GetComponent<Animator>();
        radioMoletteAnim = GameObject.Find("FrameDialTune").GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(menuIndex > 0)
            {
                pipBoyAnim.SetTrigger("changeTab");
                handAnim.SetTrigger("changeTab");

                menuIndex--;
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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (menuIndex < 4)
            {
                pipBoyAnim.SetTrigger("changeTab");
                handAnim.SetTrigger("changeTab");

                menuIndex++;
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
        if(radioActive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (radioIndex < 1)
                {
                    radioIndex++;
                    radioMoletteAnim.SetBool("right", true);
                    radioMoletteAnim.SetBool("left", false);
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (radioIndex > 0)
                {
                    radioIndex--;
                    radioMoletteAnim.SetBool("right", false);
                    radioMoletteAnim.SetBool("left", true);
                }
            }
        }
    }

    private void GoToStats()
    {
        radioActive = false;

        pipBoyAnim.SetBool("stats", true);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stats", true);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToInv()
    {
        radioActive = false;

        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", true);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stats", false);
        handAnim.SetBool("inv", true);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToData()
    {
        radioActive = false;

        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", true);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stats", false);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", true);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", false);
    }
    private void GoToMap()
    {
        radioActive = false;

        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", true);
        pipBoyAnim.SetBool("radio", false);

        handAnim.SetBool("stats", false);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", true);
        handAnim.SetBool("radio", false);
    }
    private void GoToRadio()
    {
        radioActive = true;

        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", true);

        handAnim.SetBool("stats", false);
        handAnim.SetBool("inv", false);
        handAnim.SetBool("data", false);
        handAnim.SetBool("map", false);
        handAnim.SetBool("radio", true);
    }
}
