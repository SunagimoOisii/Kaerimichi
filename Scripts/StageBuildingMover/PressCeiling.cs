using UnityEngine;

/// <summary>
/// �ォ��L�����������Ԃ��V��M�~�b�N<para></para>
/// ���̃N���X���������I�u�W�F�N�g���L�����N�^�[�ɐG�ꂽ�Ƃ�
/// </summary>
public class PressCeiling : MonoBehaviour
{
    private void OnCollisionStay(Collision col)
    {
        //CharacterActor�p���N���X�ɐG�ꂽ�Ƃ��A���̃I�u�W�F�N�g��
        //�n�ʂɐڂ��Ă���Ή����Ԃ����Ƃ݂Ȃ��A�L�����N�^�[�𑦎�������
        if (col.gameObject.TryGetComponent(out CharacterActor cActor) == false ||
            cActor.pData.IsOnFloor == false)
        {
            return;
        }

        //��������
        if (col.gameObject.TryGetComponent(out Alien_Character alienActor))
        {
            alienActor.InstantDeath();
        }
        else
        {
            cActor.InstantDeath();
        }
    }
}