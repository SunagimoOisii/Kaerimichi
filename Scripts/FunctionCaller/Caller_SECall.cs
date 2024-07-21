using UnityEngine;

public class Caller_SECall : MonoBehaviour
{
    [SerializeField] private string SEName;

/***************************************************************************
外部呼出し専用関数
***************************************************************************/

    public void Call_SECall()
    {
        SoundManager.instance.PlaySE(SEName);
    }
}