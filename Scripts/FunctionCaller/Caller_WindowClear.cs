using UnityEngine;

public class Caller_WindowClear : MonoBehaviour
{
    [Header("�I��������Window")]
    [SerializeField] private Window_director windowDirector;

/***************************************************************************
�O���ďo����p�֐�
***************************************************************************/

    public void Call_windowClear()
    {
        windowDirector.EndWindow();
    }
}