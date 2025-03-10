using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// �V�[���ǂݍ��ݎ��ɖ��](�t�F�[�h�C��)�������s��<para></para>
/// �V�[�����܂����ł��j������Ȃ�
/// </summary>
public class Fader : MonoBehaviour
{
    public static Fader instance;

    [Header("�t�F�[�h�C�������܂ł̎��Ԑݒ�")]
    [SerializeField] private float duration_fadeIn = 0.25f;
    public float GetDuration_fadeIn() { return duration_fadeIn; }

    [Header("�t�F�[�h�A�E�g�����܂ł̎��Ԑݒ�")]
    [SerializeField] private float duration_fadeOut = 0.25f;
    public float GetDuration_fadeOut() { return duration_fadeOut; }

    private Image fadeImg;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            fadeImg  = GetComponent<Image>();
            DontDestroyOnLoad(transform.parent);
            SceneManager.sceneLoaded += FadeInAfterSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FadeInAfterSceneLoaded(Scene nextScene, LoadSceneMode mode) 
    {
        //��u�ō��w�i�������������
        //�t�F�[�h�C�����s��
        Sequence sequence = DOTween.Sequence();
        sequence.Append(fadeImg.DOFade(1.0f, 0.0f));
        sequence.Append(fadeImg.DOFade(0.0f, duration_fadeIn));
	}

    public void FadeOut()
    {
        fadeImg.DOFade(1.0f, duration_fadeOut);
    }
}