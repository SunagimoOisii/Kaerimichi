using UnityEngine;

/// <summary>
/// CharacterActor�p���N���X�̃��C�t���[���ȉ���
/// �w��p�[�e�B�N���𐶐�����GameObject��j������
/// </summary>
public class DeadParticle : MonoBehaviour
{
    [Header("���ꎞ�̃p�[�e�B�N��")]
    [SerializeField] private ParticleSystem particle;
    private CharacterActor actor;

    private void Start()
    {
        actor = GetComponent<CharacterActor>();
    }

    private void Update()
    {
        if(actor.GetLife() <= 0)
        {
            SoundManager.instance.PlaySE("Dead");
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}