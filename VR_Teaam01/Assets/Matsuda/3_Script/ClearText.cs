using TMPro;
using UnityEngine;

public class ClearText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Text.text = "Clear :" + GameResultData.ClearCount;
    }
}
