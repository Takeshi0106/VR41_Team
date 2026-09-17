using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeWarning_Vignette : MonoBehaviour
{
    [SerializeField] private Volume volume;

    private Vignette vignette;
    [Header("ビネット効果のでかさ 0から1")]
    [SerializeField] private float maxIntensity = 0.4f;


    private void Start()
    {
        if (volume == null)
        {
            Debug.LogError("Volumeが設定されていません");
            return;
        }

        if (volume.profile.TryGet(out vignette))
        {
            Debug.Log("Vignette取得成功");

            vignette.color.overrideState = true;
            vignette.intensity.overrideState = true;
            vignette.smoothness.overrideState = true;

            vignette.color.value = Color.red;
            vignette.intensity.value = 0f;
            vignette.smoothness.value = 0.4f;//ふちのぼかし具合
        }
        else
        {
            Debug.LogError("VolumeのProfileにVignetteがありません");
        }
    }

    public void UpdateWarning()
    {
        if (vignette == null) return;

        vignette.intensity.value = maxIntensity;
                
    }
}
