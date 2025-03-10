using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffectManager : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [Header("�J�[�\����v,�s��v���ɕ\������X�v���C�g")]
    [SerializeField] private Sprite sprite_enter;
    [SerializeField] private Sprite sprite_exit;
    [Header("�N���b�N���̋����\���ɂ���")]
    [SerializeField] private float zoomInScale           = 1.05f;
    [SerializeField] private float zoomInDuration        = 0.1f;
    [SerializeField] private float beOriginScaleDuration = 0.2f;
    private Vector3 originScale = Vector3.one;
    private Image img = null;

    private void Start()
    {
        originScale = transform.localScale;
        img         = GetComponent<Image>();
    }

    //�J�[�\����v���̊g�剉�o
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (sprite_enter == null) return;
        img.sprite = sprite_enter;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (sprite_exit == null) return;
        img.sprite = sprite_exit;
    }

    //�N���b�N���̏k�����o
    public void OnPointerClick(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(originScale * zoomInScale, zoomInDuration))
            .OnComplete(() => transform.DOScale(originScale, beOriginScaleDuration));
    }
}