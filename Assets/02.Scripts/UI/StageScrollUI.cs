using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class StageScrollUI : MonoBehaviour
{
    public RectTransform contentsPanel; // 이동할 대상 (자식이 여러 개 있는 패널)
    public float itemWidth = 199f;   
    public float slideDuration = 1f;
    public List<StageData> currentStages;

    private int currentIndex = 0;

    public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject middlePrefab;
    public Sprite normalSprite;
    public Sprite bossSprite;
    public Sprite treasureSprite;

    Tween slideTween;

    private void Awake()
    {
        if (middlePrefab != null) itemWidth = middlePrefab.GetComponent<RectTransform>().rect.width;

        StageManager.Instance.onChapterStart.AddListener(OnGenerateChapter);
        StageManager.Instance.onStageComplete.AddListener(SlideToNext);
    }

    public void SlideToNext()
    {
        if (currentIndex >= currentStages.Count - 1) return;

        currentIndex++;

        if (slideTween != null && slideTween.IsActive())
        {
            slideTween.Kill();
        }

        float targetX = -itemWidth * currentIndex;

        slideTween = contentsPanel.DOAnchorPos(new Vector2(targetX, contentsPanel.anchoredPosition.y), slideDuration)
                                .SetEase(Ease.OutCubic);
    }

    public void SlideToStart()
    {
        currentIndex = 0;

        if (slideTween != null && slideTween.IsActive())
        {
            slideTween.Kill();
        }

        slideTween = contentsPanel.DOAnchorPos(new Vector2(0, contentsPanel.anchoredPosition.y), 1f)
                                .SetEase(Ease.OutCubic);
    }

    public void OnGenerateChapter()
    {
        DeleteAllChildren();
        currentStages = StageManager.Instance.currentStages;

        for (int i = 0; i < currentStages.Count; i++)
        {
            WoodenScroll scroll;

            if (i == 0)
            {
                scroll = Instantiate(startPrefab, contentsPanel).GetComponent<WoodenScroll>();
            }
            else if (i == currentStages.Count - 1)
            {
                scroll = Instantiate(endPrefab, contentsPanel).GetComponent<WoodenScroll>();
            }
            else
            {
                scroll = Instantiate(middlePrefab, contentsPanel).GetComponent<WoodenScroll>();
            }

            if (scroll.stageTypeImage != null) scroll.stageTypeImage.sprite = GetScrollSprite(currentStages[i].stageType);
        }

        SlideToStart();
    }

    public void DeleteAllChildren()
    {
        foreach (Transform child in contentsPanel)
        {
            Destroy(child.gameObject);
        }
    }

    Sprite GetScrollSprite(StageType type)
    {
        switch (type)
        {
            case StageType.Normal:
                return normalSprite;
            case StageType.Boss:
                return bossSprite;
            case StageType.Treasure:
                return treasureSprite;
            default:
                return normalSprite;
        }
    }
}