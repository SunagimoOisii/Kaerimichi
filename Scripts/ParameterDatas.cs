using UnityEngine;

/// <summary>
/// �{�Q�[���ł͑S�ẴL�����N�^�[�p�����[�^����������Q�Ƃ���<para></para>
/// �܂��A�C�x���g�Q���҂��p�����[�^��c�����₷�����邽�߂ɕϐ�����{��Ŗ�������
/// </summary>
[CreateAssetMenu(fileName = "ParameterDatas", menuName = "ParameterDatas")]
public class ParameterDatas : ScriptableObject
{
    [Header("�����肠�񂿂��̃X�e�[�^�X")]
    public ParameterData �����肠��X�e�[�^�X;

    [Header("UFO�̃X�e�[�^�X")]
    public ParameterData UFO�X�e�[�^�X;

    [Header("�j�ZUFO�̃X�e�[�^�X")]
    public ParameterData �j�ZUFO�X�e�[�^�X;
}

[System.Serializable]
public struct ParameterData
{
    [Range(0, 5)] public int ���C�t;
    public bool �ړ����邩�ǂ���;
    [Range(1, 100)] public float ��������;
    public bool �W�����v���邩�ǂ���;
    [Range(1, 100)] public float �W�����v�̑���;
    [Range(1, 20)]  public float �G��p_�W�����v�̕b���Ԋu;

    [HideInInspector] public bool IsOnFloor;
}