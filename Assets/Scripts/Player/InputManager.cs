using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public void MoveInput(InputAction.CallbackContext context)
    {
        PlayerMovement.instance.MovePlayer(context.ReadValue<Vector2>());
    }

    public void Look(InputAction.CallbackContext context)
    {
        MouseLook.instance.CameraLook(context.ReadValue<Vector2>());
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.ReadValueAsButton())
        {
            PlayerMovement.instance.Jump();
        }
    }

    public void SwitchMenu(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PipBoy.instance.ChangeIndex((int)context.ReadValue<float>());
        }
    }

    public void SwitchSubTabs(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PipBoy.instance.ChangeSubTabs((int)context.ReadValue<float>());
        }
    }

    public void OpenPipBoy(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            PipBoy.instance.OpenClose();
        }
    }
         
    private void Update()
    {
        if (PipBoy.instance.getActive())
        {
            if(Gamepad.current == null)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
