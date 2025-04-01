using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
public class DamageIndicator : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public CanvasGroup canvasGroup;
    public float floatUpSpeed = 50f;
    public float duration = 1f;
    public Vector3 moveOffset = new Vector3(0, 100, 0);
    public int normalSize = 56;
    public int critSize = 100;
    public Color critColor = Color.yellow;
    public Color normalColor = Color.white;
    TMP_Text tmp;
    Material newMat;
    private Vector3 startPos;
    private void Start()
    {
        tmp = GetComponent<TMP_Text>();
        newMat = Instantiate(tmp.fontMaterial);
        tmp.fontMaterial = newMat;

        newMat.SetFloat(ShaderUtilities.ID_OutlineWidth, 0.04f);
        newMat.SetColor(ShaderUtilities.ID_OutlineColor, Color.white);
    }

    public void Show(int damage, bool isCrit)
    {
        damageText.text = damage.ToString();
        damageText.fontSize = isCrit ? critSize : normalSize;
        damageText.color = isCrit ? critColor : normalColor;
        canvasGroup.alpha = 1;

        startPos = transform.localPosition;

        // DOTween으로 위로 올라가면서 페이드
        transform.DOLocalMove(startPos + moveOffset, duration).SetEase(Ease.OutCubic);
        canvasGroup.DOFade(0, duration).SetEase(Ease.InSine).OnComplete(() =>
        {
            GameManager.Instance.damageIndicatorPool.ReturnToPool(this.gameObject);
        });
    }
}