using UnityEngine;

public class Caller_SECall : MonoBehaviour
{
    [SerializeField] private string SEName;

/***************************************************************************
�O���ďo����p�֐�
***************************************************************************/

    public void Call_SECall()
    {
        SoundManager.instance.PlaySE(SEName);
    }
}