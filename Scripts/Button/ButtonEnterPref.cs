using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ���߂ŃJ�[�\������v�����{�^�����L�^����
/// </summary>
public class ButtonEnterPref : MonoBehaviour,IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayerPrefs.SetString("RecentEnteredButtonName", gameObject.name); 
    }
}