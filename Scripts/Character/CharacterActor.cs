using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class CharacterActor : MonoBehaviour
{
    public ParameterData pData;
    public int GetLife() { return pData.���C�t; }
    public void AdjustLife(int amount) { pData.���C�t += amount; }

    public void InstantDeath()
    {
        pData.���C�t = 0;
    }

    //�ړ��͈͂̐����͈�(�X�e�[�W�̍��E�[�̍��W�ɍ��킹��)
    private readonly float minPos_x = -9.5f;
    private readonly float maxPos_x = 9.5f;

    //���@��footPoint,�G��headPoint���Z�b�g����
    //���@�̓��݂��U������Ɏg�p
    protected Transform edgePoint;
    public Transform GetEdgePoint() { return edgePoint; }

    protected Rigidbody rb;

    //JumpAtRegularInterval()�Ŏg�p
    private float timeFromLanding = 0.0f;

    protected void Update()
    {
        SetLimitPos_x();
    }

    private void SetLimitPos_x()
    {
        //X�ɂ��Ĉړ��͈͂̐������s��
        if(transform.position.x < minPos_x)
        {
            transform.position = new Vector3(minPos_x,
                                             transform.position.y,
                                             transform.position.z);
        }
        if(transform.position.x > maxPos_x)
        {
            transform.position = new Vector3(maxPos_x,
                                             transform.position.y,
                                             transform.position.z);
        }
    }

    protected void Start_RoundTrip_x(float startPos_x, float endPos_x)
    {
        if (pData.�ړ����邩�ǂ���)
        {
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            RoundTrip_x(startPos_x, endPos_x, token).Forget();
        }
    }
    private async UniTask RoundTrip_x(float startPos_x, float endPos_x, CancellationToken token)
    {
        bool isMovingToEndPos = true;
        while (true)
        {
            if(isMovingToEndPos)
            {
                transform.Translate(pData.�������� * Time.deltaTime, 0, 0);
            }
            else
            {
                transform.Translate(-pData.�������� * Time.deltaTime, 0, 0);
            }
            Debug.Log(isMovingToEndPos);

            //�n�_�܂��͏I�_�ɓ��B�����ہA�ړ������𔽓]������
            if (transform.position.x > endPos_x)
            {
                isMovingToEndPos = false;
                transform.position = new(endPos_x, transform.position.y, transform.position.z);
            }
            else if (transform.position.x < startPos_x)
            {
                isMovingToEndPos = true;
                transform.position = new(startPos_x, transform.position.y, transform.position.z);
            }

            await UniTask.Yield(token);
        }
    }

    protected void Start_JumpAtRegularInterval() 
    {
        if(pData.�W�����v���邩�ǂ��� && gameObject.CompareTag("Enemy"))
        {
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            JumpAtRegularInterval(token).Forget();
        }
    }
    private async UniTask JumpAtRegularInterval(CancellationToken token)
    {
        //�W�����v�̕b���Ԋu���A���Ԃ��o�߂����ۂɃW�����v���s��
        while(true)
        {
            timeFromLanding += Time.deltaTime;

            if (timeFromLanding > pData.�G��p_�W�����v�̕b���Ԋu &&
                pData.IsOnFloor)
            {
                //�W�����v�̑����̒l�����̂܂܎g�p����Ƒ�������̂�delayRatio���|���Z
                float delayRatio = 0.25f;
                Vector3 force    = new(0, pData.�W�����v�̑��� * delayRatio, 0);
                rb.AddForce(force, ForceMode.VelocityChange);

                timeFromLanding = 0.0f;
            }

            await UniTask.Yield(token);
        }
    }

    protected void OnCollisionEnter(Collision col)
    {
        //�W�����v���ɃW�����v�̕b���Ԋu���̎��Ԃ��o�߂���
        //���n�̏u�Ԃɂ܂��W�����v���邱�Ƃ�h������
        //���n����timeFromLanding�����Z�b�g����
        string colTag = col.gameObject.tag;
        if (colTag != "Floor") return;

        timeFromLanding = 0.0f;
        pData.IsOnFloor = true;
    }

    protected void OnCollisionExit(Collision col)
    {
        string colTag = col.gameObject.tag;
        if (colTag != "Floor") return;

        pData.IsOnFloor = false;
    }
}