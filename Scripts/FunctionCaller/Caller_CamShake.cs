using DG.Tweening;
using UnityEngine;

/// <summary>
/// �J�����h��@�\��񋟂���
/// </summary>
public class Caller_CamShake : MonoBehaviour
{
    [Header("�J�������w���MainCamera���g�p")]
    [SerializeField] private Camera cam      = null;
    [SerializeField] private float  strength = 1.0f;
    [SerializeField] private float  duration = 0.15f;

    public void SetShakeTargetCam(Camera c) { cam = c; }
    public void SetStrength(float stren) { strength = stren; }
    public void SetDuration(float dura) { duration = dura; }

/***************************************************************************
�O���ďo����p�֐�
***************************************************************************/
    public void Call_camShake()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }
        cam.transform.DOShakePosition(duration, strength);
    }
}