using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float timeLimitSeconds = 180f;
    [SerializeField] private float warningTime = 4f;
    private bool isWarningActive = false;

    [Header("数字画像")]
    [SerializeField] private Sprite[] numberSprites; // 0～9

    [Header("表示するImage")]
    [SerializeField] private Image minute10;
    [SerializeField] private Image minute1;
    [SerializeField] private Image second10;
    [SerializeField] private Image second1;

    [SerializeField] private BasketBallManager basketBallManager;
    [SerializeField] private TimeWarning_Vignette warningVignette;


    private float remainingTime;

    void Start()
    {
        remainingTime = timeLimitSeconds;
        if (basketBallManager == null)
        {
            Debug.LogError("BasketBallManager が設定されていません。Inspectorで設定してください。");
        }
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime < 0)
            {
                remainingTime = 0;
                basketBallManager.EndGame();

            }
        }

        UpdateTimerImage();

        if (remainingTime <= warningTime)
        {
            isWarningActive = true;
        }

        if (isWarningActive)
        {
            warningVignette.UpdateWarning();
        }

    }

    void UpdateTimerImage()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60f);
        int seconds = Mathf.FloorToInt(remainingTime % 60f);

        minute10.sprite = numberSprites[minutes / 10];
        minute1.sprite = numberSprites[minutes % 10];

        second10.sprite = numberSprites[seconds / 10];
        second1.sprite = numberSprites[seconds % 10];
    }
}
