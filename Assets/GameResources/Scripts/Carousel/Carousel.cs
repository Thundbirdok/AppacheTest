using System;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    [Header("Настройки")]

    [SerializeField]
    private int panOffset = 25;

    [SerializeField]
    private float snapSpeed = 5;

    [SerializeField]
    private float scaleOffset = 5;

    [SerializeField]
    private float scaleSpeed = 5;

    [Header("Объекты")]

    [SerializeField]
    private GameObject panPrefab = default;

    [SerializeField]
    private ScrollRect scrollRect = default;

    [SerializeField]
    private CardData[] cards = default;

    private const int MINIMAL_SCROLL_SPEED = 400;
    private const float MINIMAL_SCALE = 0.5f;
    private const float MAXIMAL_SCALE = 1f;

    private GameObject[] pans;
    private Vector2[] pansPositions;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanIndex;

    private bool isScrolling;

    private float panWidth = 0;

    private void Start()
    {
        panWidth = panPrefab.GetComponent<RectTransform>().sizeDelta.x;

        contentRect = GetComponent<RectTransform>();

        SpawnPans();
    }

    private void Update()
    {
        if (contentRect.anchoredPosition.x >= pansPositions[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPositions[pansPositions.Length - 1].x && !isScrolling)
        {
            scrollRect.inertia = false;
        }

        float nearestPos = float.MaxValue;

        for (int i = 0; i < pans.Length; ++i)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPositions[i].x);

            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanIndex = i;
            }

            Scale(i, distance);
        }

        ScrollCheck();
    }

    private void ScrollCheck()
    {
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

        if (scrollVelocity < MINIMAL_SCROLL_SPEED && !isScrolling)
        {
            scrollRect.inertia = false;
        }

        if (isScrolling || scrollVelocity > MINIMAL_SCROLL_SPEED)
        {
            return;
        }

        Snap();
    }

    private void Snap()
    {
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPositions[selectedPanIndex].x, snapSpeed * Time.deltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    private void Scale(int index, float distance)
    {
        float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, MINIMAL_SCALE, MAXIMAL_SCALE);

        pansScale[index].x = Mathf.SmoothStep(pans[index].transform.localScale.x, scale, scaleSpeed * Time.deltaTime);
        pansScale[index].y = Mathf.SmoothStep(pans[index].transform.localScale.y, scale, scaleSpeed * Time.deltaTime);
        pans[index].transform.localScale = pansScale[index];
    }

    public void Scrolling(bool isScrolling)
    {
        this.isScrolling = isScrolling;

        if (isScrolling)
        {
            scrollRect.inertia = true;
        }
    }

    private void SpawnPans()
    {
        pans = new GameObject[cards.Length];
        pansPositions = new Vector2[cards.Length];
        pansScale = new Vector2[cards.Length];

        for (int i = 0; i < cards.Length; ++i)
        {
            pans[i] = Instantiate(panPrefab, transform, false);

            pans[i].transform.localPosition = new Vector2(i * (panWidth + panOffset),
                pans[i].transform.localPosition.y);

            pansPositions[i] = -pans[i].transform.localPosition;

            CardController cardController = pans[i].GetComponent<CardController>();

            cardController.Data = cards[i];
        }
    }
}
