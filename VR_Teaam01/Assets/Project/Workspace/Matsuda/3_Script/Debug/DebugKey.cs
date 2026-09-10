using UnityEngine;
using UnityEngine.InputSystem;

public class DebugKey : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame)
        {
            Debug.Log("F1キーが押されました");
            SoundManager.GetInstance().PlayBgm(BGMType.Title, 1.0f, 1.0f);
        }
        if (Keyboard.current.f2Key.wasPressedThisFrame)
        {
            Debug.Log("F2キーが押されました");
            SoundManager.GetInstance().PlayBgm(BGMType.Game, 1.0f, 1.0f);
        }
        if(Keyboard.current.f3Key.wasPressedThisFrame)
        {
            Debug.Log("F3キーが押されました");
            SoundManager.GetInstance().PlayBgm(BGMType.Result, 1.0f, 1.0f);
        }

        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            Debug.Log("1キーが押されました");
            SoundManager.GetInstance().PlaySe(SEType.Decision);
        }
        if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            Debug.Log("2キーが押されました");
            SoundManager.GetInstance().PlaySe(SEType.Buzzer);
        }
        if(Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            Debug.Log("3キーが押されました");
            SoundManager.GetInstance().PlaySe(SEType.Cheers);
        }
    }
}
