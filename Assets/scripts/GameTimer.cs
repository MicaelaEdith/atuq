using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float timeRemaining;
    private float startTime;
    private bool isRunning;

    [SerializeField]
    private Sprite clock;
    [SerializeField]
    private Transform clockTransform;
    [SerializeField]
    private float rotationSpeed = 2f;
    private bool isClockAnimating;

    [SerializeField]
    private RectTransform fillRect;
    [SerializeField]
    private Image fillColor;
    [SerializeField]
    private Gradient gradient;

    private void Awake()
    {
        Instance = this;
        SetAnchorToLeft();
    }

    public void StartTimer(float duration)
    {
        startTime = duration;
        timeRemaining = duration;
        isRunning = true;

        ShowTimerUI(true);
    }

    public void StopTimer()
    {
        isRunning = false;
        ShowTimerUI(false);
    }

    private void Update()
    {
        if (isClockAnimating)
            AnimateClock();

        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerUI();

        if (timeRemaining <= 0)
        {
            isRunning = false;
            StopTimer();
            GameManager.Instance.GameOver();
        }
    }

    private void UpdateTimerUI()
    {
        float factor = Mathf.Clamp01(timeRemaining / startTime);
        fillRect.localScale = new Vector3(factor, 1, 1);
        fillColor.color = gradient.Evaluate(factor);
    }

    private void ShowTimerUI(bool show)
    {
        fillRect.gameObject.SetActive(show);
        isClockAnimating = show;
    }

    private void AnimateClock()
    {
        float angle = Mathf.Sin(Time.time * rotationSpeed) * 30f;
        clockTransform.localRotation = Quaternion.Euler(0, 0, angle);
    }


    private void SetAnchorToLeft()
    {
        RectTransform rectTransform = fillRect.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0.5f);
        rectTransform.anchorMax = new Vector2(0, 0.5f);
        rectTransform.pivot = new Vector2(0, 0.5f);

    }

}