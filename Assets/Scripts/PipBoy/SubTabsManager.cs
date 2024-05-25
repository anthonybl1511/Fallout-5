using TMPro;
using UnityEngine;

public class SubTabsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] subTabsText;
    [SerializeField] private GameObject[] subTabsContent;
    [SerializeField] private float posOffset;

    private Vector3 initialPos;
    private int index = 0;

    private void Start()
    {
        initialPos = subTabsText[0].transform.position;
        initialPos.x -= subTabsText[0].GetComponent<RectTransform>().rect.width / 30;
    }

    public void switchIndex(int _index)
    {
        initialPos.x += subTabsText[index].GetComponent<RectTransform>().rect.width / 30;
        index += _index;

        if (index < 0)
        {
            index = 2;
        }
        else if(index > 2) {
            index = 0;
        }

        initialPos.x -= subTabsText[index].GetComponent<RectTransform>().rect.width / 30;
    }

    private void Update()
    {
        for(int i = 0; i < subTabsText.Length; i++)
        {
            subTabsText[i].transform.position = Vector3.Lerp(subTabsText[i].transform.position , new Vector3(initialPos.x + ((i - index) * posOffset) + subTabsText[i].GetComponent<RectTransform>().rect.width / 30, subTabsText[i].transform.position.y, subTabsText[i].transform.position.z), 5*Time.deltaTime);
            
            if(i - index == 0)
            {
                subTabsText[i].GetComponent<TextMeshProUGUI>().alpha = Mathf.Lerp(subTabsText[i].GetComponent<TextMeshProUGUI>().alpha, 1f, 5 * Time.deltaTime);
            }
            else if(i - index == 1 || i - index == -1)
            {
                subTabsText[i].GetComponent<TextMeshProUGUI>().alpha = Mathf.Lerp(subTabsText[i].GetComponent<TextMeshProUGUI>().alpha , 0.5f, 5* Time.deltaTime);
            }
            else
            {
                subTabsText[i].GetComponent<TextMeshProUGUI>().alpha = Mathf.Lerp(subTabsText[i].GetComponent<TextMeshProUGUI>().alpha, 0.2f, 5 * Time.deltaTime);
            }
        }
    }
}
