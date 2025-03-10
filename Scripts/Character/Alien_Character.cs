using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using UnityEngine;

public class Alien_Character : CharacterActor
{
    [Header("�G�Ƃ̓����蔻��Ɏg�p���鑫�̍��W�I�u�W�F�N�g")]
    [SerializeField] private GameObject footPoint;

    public event Action LifeDecreaseEvent = null;

    //BeInvincible()�Ŏg�p
    private bool isInvincible = false;
    private readonly float flashingInterval = 0.25f;
    private readonly int invincibleSecond   = 4;

    private Caller_CamShake camShake = null;

    public new void InstantDeath()
    {
        base.InstantDeath();
        LifeDecreaseEvent?.Invoke();
    }

    private void Start()
    {
        //�p�����[�^�Ɗe��R���|�[�l���g���擾����
        ParameterDatas paramD = Resources.Load<ParameterDatas>("ParameterDatas");
        rb       = GetComponent<Rigidbody>();
        pData    = paramD.�����肠��X�e�[�^�X;
        camShake = GetComponent<Caller_CamShake>();

        //�W�����v�U������Ɏg�p����edgePoint���擾
        edgePoint = transform.GetChild(0);
    }

    private new void Update()
    {
        base.Update();
        MoveByInput();
    }

    private void MoveByInput()
    {
        if (pData.�ړ����邩�ǂ��� &&
           (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)))
        {
            //���E�ړ�
            int direction   = (Input.GetKey(KeyCode.LeftArrow)) ? -1 : 1;
            Vector3 movePos = new(direction * pData.��������, 0, 0);
            rb.velocity     = new(movePos.x, rb.velocity.y, 0);
        }

        //�W�����v
        if (pData.�W�����v���邩�ǂ��� &&
            pData.IsOnFloor            &&
            Input.GetKeyDown(KeyCode.Space)) 
        {
            Vector3 force = new(0, pData.�W�����v�̑���, 0);
            rb.AddForce(force, ForceMode.VelocityChange);
            SoundManager.instance.PlaySE("Jump");
            pData.IsOnFloor = false;
        }
    }

    private async UniTask BeInvincible()
    {
        isInvincible = true;

        //���G���̓_�ŏ���
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        Color original  = sRenderer.color;
        Color invisible = new(255, 255, 255, 0); //������
        Sequence sequence = DOTween.Sequence();
        sequence.Append(sRenderer.DOColor(invisible, 0.01f)); //�A���t�@�l��Z���Ԃōŏ��ő�ɐݒ肷�邱�Ƃœ_�łƂ�����
        sequence.AppendInterval(flashingInterval);
        sequence.Append(sRenderer.DOColor(original, 0.01f));
        sequence.AppendInterval(flashingInterval);
        sequence.SetLoops(-1);
        sequence.Play();

        await UniTask.Delay(invincibleSecond * 1000);

        sequence.Kill();
        sRenderer.color = original;
        isInvincible = false;
    }   

    private void OnTriggerEnter(Collider col)
    {
        string objTag = col.gameObject.tag;
        if (objTag != "Enemy") return;

        //���Y���W�ȏ㑊���荂���ꍇ�͓��݂��U���Ƃ݂Ȃ��A
        //����̃��C�t������ď��W�����v����
        //�U������,���ꂽ�Ɍ��炸�J�����h������s
        CharacterActor enemyActor = col.GetComponent<CharacterActor>();
        Transform enemyEdgePoint  = enemyActor.GetEdgePoint();
        if (edgePoint.position.y >= enemyEdgePoint.position.y)
        {
            enemyActor.AdjustLife(-1);
            Vector3 jumpForce = new(rb.velocity.x,
                                    pData.�W�����v�̑���,
                                    rb.velocity.z);
            rb.velocity = jumpForce;
            camShake.Call_camShake();
            return;
        }

        if (isInvincible) return;

        pData.���C�t--;
        LifeDecreaseEvent?.Invoke();
        BeInvincible().Forget();
        camShake.Call_camShake();
        return;
    }

    private void OnCollisionStay(Collision col)
    {
        string colTag = col.gameObject.tag;
        if (colTag != "Floor") return;

        pData.IsOnFloor = true;
    }
}