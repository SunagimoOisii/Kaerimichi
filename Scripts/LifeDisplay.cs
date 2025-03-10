using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Alien�̃��C�t�ɉ������n�[�g�摜�\�����s��
/// </summary>
public class LifeDisplay : MonoBehaviour
{
    [SerializeField] private Alien_Character alienScript = null;
    [SerializeField] private Sprite heartImg_full         = null;
    [SerializeField] private Sprite heartImg_empty        = null;
    [SerializeField] private Vector3 heartImgOriginPos   = Vector3.zero;
    [SerializeField] private float heartImgOffset        = 0.0f;
    private readonly List<Image> imgList = new();
    private int lifeMax = 0;

    private void Start()
    {
        //Alien�̃��C�t�����擾
        ParameterData alienParamD = Resources.Load<ParameterDatas>("ParameterDatas").�����肠��X�e�[�^�X;
        lifeMax = alienParamD.���C�t;

        //Image�R���|�[�l���g���������q�I�u�W�F�N�g�����C�t������
        for (int i = 0; i < lifeMax; i++)
        {
            GameObject imgObj   = new();
            Image imgComponent  = imgObj.AddComponent<Image>();
            imgComponent.sprite = heartImg_full;
            imgObj.transform.SetParent(transform, false);

            //�ʒu�𒲐����ĉ����тɕ\��
            Vector3 offset = Vector3.right * heartImgOffset;
            imgObj.transform.position = heartImgOriginPos + offset * i;

            imgList.Add(imgComponent);
        }

        //���C�t�����ɍ��킹�ĉ摜�\���ύX���s���C�x���g�n���h���[�o�^
        alienScript.LifeDecreaseEvent += ReflectLifeNumToImgs;
    }

    /// <summary>
    /// Alien�̃��C�t�����C�x���g�̃n���h���[�Ɏg�p����
    /// </summary>
    private void ReflectLifeNumToImgs()
    {
        int lifeNum = alienScript.GetLife();

        //�������C�t,�c�胉�C�t�ɉ������摜�ɍ����ւ�
        //�c�胉�C�t�摜�\��
        for (int i = 0; i < lifeMax; i++)
        {
            if(i < lifeNum)
            {
                imgList[i].sprite = heartImg_full;
            }
            else
            {
                imgList[i].sprite = heartImg_empty;
            }
        }
    }
}