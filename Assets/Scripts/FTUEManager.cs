using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FTUEManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject ftuePanel;
    public Button openFTUEButton;
    public Button closeFTUEButton;

    [Header("Carousel")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public int slideCount = 5;

    [Header("Dots")]
    public Image[] dotImages;
    public Color activeDotColor = Color.white;
    public Color inactiveDotColor = new Color(1, 1, 1, 0.3f);

    [Header("Snap Settings")]
    public float snapSpeed = 10f;

    private float[] slidePositions;
    private int currentSlide = 0;
    private bool isDragging = false;
    private bool isSnapping = false;
    private float targetPosition;

    
    void Start()
    {
        openFTUEButton.onClick.AddListener(OpenFTUE);
        closeFTUEButton.onClick.AddListener(CloseFTUE);

        ftuePanel.SetActive(false);
        CalculateSlidePositions();
        UpdateDots();
    }

    void CalculateSlidePositions()
    {
        slidePositions = new float[slideCount];
        for (int i = 0; i < slideCount; i++)
        {
            slidePositions[i] = (float)i / (slideCount - 1);
        }
    }

    public void OpenFTUE()
    {
        ftuePanel.SetActive(true);
        currentSlide = 0;
        scrollRect.horizontalNormalizedPosition = 0f;
        UpdateDots();
    }

    public void CloseFTUE()
    {
        ftuePanel.SetActive(false);
    }

    void Update()
    {
        if (ftuePanel.activeSelf)
        {
            DetectClosestSlide();

            if (!isDragging && !scrollRect.velocity.Equals(Vector2.zero))
            {
                // let it settle from inertia, then snap
            }

            if (!isDragging && scrollRect.velocity.magnitude < 50f)
            {
                SnapToSlide();
            }
        }
    }

    void DetectClosestSlide()
    {
        float currentPos = scrollRect.horizontalNormalizedPosition;
        float closestDistance = Mathf.Infinity;
        int closestIndex = currentSlide;

        for (int i = 0; i < slidePositions.Length; i++)
        {
            float distance = Mathf.Abs(currentPos - slidePositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        if (closestIndex != currentSlide)
        {
            currentSlide = closestIndex;
            UpdateDots();
        }
    }

    void SnapToSlide()
    {
        float target = slidePositions[currentSlide];
        scrollRect.horizontalNormalizedPosition = Mathf.Lerp(
            scrollRect.horizontalNormalizedPosition,
            target,
            Time.deltaTime * snapSpeed
        );
    }

    public void OnDragStart()
{
    Debug.Log("Drag Started");
    isDragging = true;
}

public void OnDragEnd()
{
    Debug.Log("Drag Ended");
    isDragging = false;
}

    void UpdateDots()
    {
        for (int i = 0; i < dotImages.Length; i++)
        {
            dotImages[i].color = (i == currentSlide) ? activeDotColor : inactiveDotColor;
        }
    }

    // Optional: call these from arrow buttons if you add them
    public void NextSlide()
    {
        if (currentSlide < slideCount - 1)
        {
            currentSlide++;
            UpdateDots();
        }
    }
    
    public void PreviousSlide()
    {
        if (currentSlide > 0)
        {
            currentSlide--;
            UpdateDots();
        }
    }
}