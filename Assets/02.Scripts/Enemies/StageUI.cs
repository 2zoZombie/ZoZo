﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public RectTransform stageTextTransform;
    public TMP_Text stageTitle;
    public TMP_Text stageText;
    Sequence currentSequence;

    EnemyManager enemyManager;
    [SerializeField] private TextMeshProUGUI SpawnCaout;
    public float deadCount;

    private void Awake()
    {
        UIManager.Instance.stageUI = this;
        StageManager.Instance.onStageStart.AddListener(ShowStageStart);
        StageManager.Instance.onStageStart.AddListener(SetStageTitle);
        StageManager.Instance.onStageComplete.AddListener(ShowStageClear);
        StageManager.Instance.onChapterStart.AddListener(SetStageTitle);
        stageTextTransform.gameObject.SetActive(false);
        stageTitle.gameObject.SetActive(false);
    }


    private void Start()
    {
        SetSpawnText();
    }

    public void SetStageTitle()
    {
        stageTitle.text = StageManager.Instance.currentStages[StageManager.Instance.currentStage].stageName;
        stageTitle.gameObject.SetActive(true);
    }
    private void Update()
    {
    }

    private void SetSpawnText()
    {
        if (SpawnCaout == null) return;
        SpawnCaout.text = GameManager.Instance.playerData.defeatedEnemyCount.ToString();

        
    }

    public void ShowStageText(string message)
    {
        if (currentSequence != null && currentSequence.IsActive())
        {
            currentSequence.Kill();
        }

        stageText.text = message;

        Vector2 rightStart = new Vector2(Screen.width + 600, stageTextTransform.anchoredPosition.y);
        Vector2 center = new Vector2(0, stageTextTransform.anchoredPosition.y);
        Vector2 leftExit = new Vector2(-Screen.width - 600, stageTextTransform.anchoredPosition.y);

        stageTextTransform.anchoredPosition = rightStart;
        stageTextTransform.gameObject.SetActive(true);
        currentSequence = DOTween.Sequence();
        currentSequence.Append(stageTextTransform.DOAnchorPos(center, 0.5f).SetEase(Ease.OutBack));
        currentSequence.AppendInterval(1.5f); 
        currentSequence.Append(stageTextTransform.DOAnchorPos(leftExit, 0.5f).SetEase(Ease.InBack).OnComplete(() => 
        { 
            stageTextTransform.gameObject.SetActive(false); 
        }));
    }

    public void ShowStageStart()
    {
        ShowStageText($"{StageManager.Instance.currentStages[StageManager.Instance.currentStage].stageName}\nSTAGE START!");
    }

    public void ShowStageClear()
    {
        ShowStageText("STAGE CLEAR!");
    }

    [SerializeField] private Slider stageSlider;
    private int currentstage;

    private void SetStage()
    {
        stageSlider.value = currentstage;
        stageSlider.minValue = 0;
        stageSlider.maxValue = 4;
    }
}
