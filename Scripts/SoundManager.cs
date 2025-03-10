using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [System.Serializable]
    public struct SoundData
    {
        public string name;
        public bool canOverlap;
        public AudioClip clip;
    }

    [Header("�����Ǘ�")]
    [SerializeField] private SoundData[] soundDatas_BGM = null;
    [SerializeField] private SoundData[] soundDatas_SE  = null;

    [Header("�`�����l���Ǘ�")]
    [SerializeField] private int channelNum_BGM = 1;
    [SerializeField] private int channelNum_SE  = 4;
    [SerializeField] private List<AudioSource> audioSources_BGM   = null;
    [SerializeField] private List<AudioSource> audioSources_SE    = null;
    private Dictionary<string, SoundData> soundDataDictionary     = new Dictionary<string, SoundData>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);

        //BGM,SE���ƂɃ`�����l�����AAudioSource��������GameObject�𐶐�,���X�g�Ɋi�[����
        //AudioSource�P�̂ł͂Ȃ�������A�^�b�`����GameObject�𐶐����闝�R��
        //����GameObject�ɓ����R���|�[�l���g�𕡐��A�^�b�`�ł��Ȃ�����

        for (int i = 0; i < channelNum_BGM; i++)
        {
            GameObject obj = new GameObject();
            AudioSource aSource = obj.AddComponent<AudioSource>();
            aSource.loop = true;
            obj.name = "sourceObj_BGM_" + i.ToString();
            obj.transform.parent = transform;
            audioSources_BGM.Add(obj.GetComponent<AudioSource>());
        }
        for (int i = 0; i < channelNum_SE; i++)
        {
            GameObject obj = new GameObject();
            AudioSource aSource = obj.AddComponent<AudioSource>();
            obj.name = "sourceObj_SE_" + i.ToString();
            obj.transform.parent = transform;
            audioSources_SE.Add(obj.GetComponent<AudioSource>());
        }

        //BGM,SE���ƂɊe�����̖��O�ƃf�[�^�̃y�A��o�^
        for (int i = 0; i < soundDatas_BGM.Length; i++)
        {
            soundDataDictionary.Add(soundDatas_BGM[i].name, soundDatas_BGM[i]);
        }
        for (int i = 0; i < soundDatas_SE.Length; i++)
        {
            soundDataDictionary.Add(soundDatas_SE[i].name, soundDatas_SE[i]);
        }
    }

    //���g�p��AudioSource��T������
    //�Ȃ��ꍇ��null��Ԃ�
    private AudioSource FindUnusedAudioSource_BGM() => audioSources_BGM.FirstOrDefault(aSource => aSource.isPlaying == false);
    private AudioSource FindUnusedAudioSource_SE() => audioSources_SE.FirstOrDefault(aSource => aSource.isPlaying == false);

    private SoundData? FindAudioClipByName(string name)
    {
        if (soundDataDictionary.TryGetValue(name, out SoundData value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("�w�艹��" + name + "�͑��݂��܂���");
            return null;
        }
    }

    public void PlayBGM(string name)
    {
        //��������
        SoundData? sData = (SoundData)FindAudioClipByName(name);
        if (sData == null) return;

        //�󂫂�AudioSource���m��(�󂫂��Ȃ���΃`�����l��0���g�p)
        AudioSource aSource = FindUnusedAudioSource_BGM();
        if (aSource == null) aSource = audioSources_BGM[0];

        aSource.clip = sData.Value.clip;
        aSource.Play();
    }

    public void PlaySE(string name)
    {
        //��������
        SoundData? sData = (SoundData)FindAudioClipByName(name);
        if (sData == null) return;

        //�󂫂�AudioSource���m�ۂ��Đ�(�󂫂��Ȃ����SE�炳�Ȃ�)
        //�d���Đ�������SE�̏ꍇ�̓`�����l��0�̂��̂��m��
        AudioSource aSource = null;
        if(sData.Value.canOverlap)
        {
            aSource = audioSources_SE[0];
            aSource.PlayOneShot(sData.Value.clip);
        }
        else
        {
            aSource = FindUnusedAudioSource_SE();
            if (aSource == null) return;

            aSource.clip = sData.Value.clip;
            aSource.Play();
        }
    }
}