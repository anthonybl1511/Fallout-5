using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectButton : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }
}
