using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipBoy : MonoBehaviour
{
    private Animator pipBoyAnim;
    private Animator handAnim;
    private int menuIndex;

    private void Start()
    {
        pipBoyAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
        //handAnim = GameObject.Find("FrameDialMenu").GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(menuIndex > 0)
            {
                pipBoyAnim.SetTrigger("changeTab");

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
    }

    private void GoToStats()
    {
        pipBoyAnim.SetBool("stats", true);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);
    }
    private void GoToInv()
    {
        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", true);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);
    }
    private void GoToData()
    {
        pipBoyAnim.SetBool("stats", true);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", true);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", false);
    }
    private void GoToMap()
    {
        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", true);
        pipBoyAnim.SetBool("radio", false);
    }
    private void GoToRadio()
    {
        pipBoyAnim.SetBool("stats", false);
        pipBoyAnim.SetBool("inv", false);
        pipBoyAnim.SetBool("data", false);
        pipBoyAnim.SetBool("map", false);
        pipBoyAnim.SetBool("radio", true);
    }
}
