using UnityEngine;

public class UFO_Character : CharacterActor
{
    [Header("���W�̒l���傫������end_x�ɐݒ肷�邱��")]
    [SerializeField] private float startPos_x = 0.0f;
    [SerializeField] private float endPos_x   = 0.0f;

    private void Start()
    {
        //�p�����[�^�Ɗe��R���|�[�l���g���擾����
        ParameterDatas paramD = Resources.Load<ParameterDatas>("ParameterDatas");
        pData = paramD.UFO�X�e�[�^�X;
        rb    = GetComponent<Rigidbody>();

        //�W�����v�U������Ɏg�p����edgePoint���擾
        edgePoint = transform.GetChild(0);

        //�W�����v�@�\�쓮
        Start_JumpAtRegularInterval();

        //�ړ��@�\�쓮
        Start_RoundTrip_x(startPos_x, endPos_x);
    }
}