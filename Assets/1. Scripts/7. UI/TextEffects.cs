using UnityEngine;
using DG.Tweening;

public class TextEffects : MonoBehaviour
{
    public enum EffectType { Blink, Scale }
    
    public EffectType effectType;
    public float duration = 1f;
    public float scaleMultiplier = 1.5f;  // 확대 시 사용할 크기
    public Color blinkColor = Color.white;  // 깜빡임 시 색상 변경
    public int loopCount = -1; // 무한 루프

    private TMPro.TextMeshProUGUI text;  // TMP 텍스트 컴포넌트

    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();

        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        switch (effectType)
        {
            case EffectType.Blink:
                ApplyBlinkEffect();
                break;
            case EffectType.Scale:
                ApplyScaleEffect();
                break;
        }
    }

    void ApplyBlinkEffect()
    {
        text.DOColor(blinkColor, duration).SetLoops(loopCount, LoopType.Yoyo);
    }

    void ApplyScaleEffect()
    {
        text.rectTransform.DOScale(scaleMultiplier, duration).SetLoops(loopCount, LoopType.Yoyo);
    }
}