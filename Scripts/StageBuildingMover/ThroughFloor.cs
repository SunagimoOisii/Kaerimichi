using UnityEngine;

/// <summary>
/// �����炷�蔲�����鏰�̃N���X
/// </summary>
public class ThroughFloor : MonoBehaviour
{
    [Header("���Ƃ��Ă̔�������R���C�_�[")]
    [SerializeField] private Collider coll = null;

    //�g���K�[�ɓ��������̂͏o��܂�
    //�ʏ�R���C�_�[�ƏՓ˂��Ȃ��悤�ɂ���
    private void OnTriggerEnter(Collider col)
    {
        Physics.IgnoreCollision(coll, col, true);
    }
    private void OnTriggerExit(Collider col)
    {
        Physics.IgnoreCollision(coll, col, false);
    }
}