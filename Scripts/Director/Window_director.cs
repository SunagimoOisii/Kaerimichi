using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Window_director : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private RectTransform windowRectT;

    [Header("�Ó]�ɂ��Ă̐ݒ�")]
    [SerializeField] private float maxBackgroundAlpha = 0.75f;
    [SerializeField] private float duration_blackout  = 0.25f;

    [Header("�E�B���h�E�ɂ��Ă̐ݒ�")]
    [SerializeField] private bool canAppearAsStartup = false;
    [SerializeField][Range(0, 1)] private float startScale = 0.0f;
    [SerializeField][Range(0, 1)] private float maxScale   = 1.0f;
    [SerializeField] private float duration_winAppear      = 1.0f;

    private void Start()
    {
        if (canAppearAsStartup)
        {
            StartWindow();
        }
        else
        {
            EndWindow();
        }
    }

    private void OnEnable()
    {
        windowRectT.localScale = new Vector3(startScale, startScale, startScale);
        Window_background.OnBackgroundClicked += HandleBackgroundClicked;
    }

    private void OnDisable()
    {
        Window_background.OnBackgroundClicked -= HandleBackgroundClicked;
    }

    private void HandleBackgroundClicked()
    {
        Debug.Log("Window�𖳌���");
        EndWindow();
    }

    //����ŊO������Window���n��������
    public void StartWindow()
    {
        gameObject.SetActive(true); 
        Animator anim = GetComponent<Animator>();
        anim.SetBool("canAppear", true);
    }

    //����ŊO������Window���I��������
    //�A�j���[�V�����C�x���g�ƍ��w�i�N���b�N���Ɏg�p
    public void EndWindow()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("canAppear", false);
        gameObject.SetActive(false); 
    }

/***************************************************************************
�A�j���[�V�����C�x���g�p�֐�
***************************************************************************/

    public void BlockoutBackground_animEvent()
    {
        background.DOFade(maxBackgroundAlpha, duration_blackout);
    }

    public void AppearWindows_animEvent()
    {
        windowRectT.DOScale(maxScale, duration_winAppear).SetEase(Ease.OutBounce);
    }
}