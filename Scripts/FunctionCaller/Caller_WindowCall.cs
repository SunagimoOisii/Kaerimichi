using UnityEngine;

public class Caller_WindowCall : MonoBehaviour
{
    [Header("�Ăяo��Window")]
    [SerializeField] private Window_director windowDirector;

/***************************************************************************
�O���ďo����p�֐�
***************************************************************************/

    public void Call_windowCall()
    {
        windowDirector.StartWindow();
    }
}