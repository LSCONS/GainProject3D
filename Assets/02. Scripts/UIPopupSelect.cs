using DG.Tweening;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPopupSelect : UIPopup
{
    [field : SerializeField] private Button BtnYes { get; set; }
    [field : SerializeField] private Button BtnNo { get; set; }
    [field : SerializeField] private TextMeshProUGUI TextTitle { get; set; }
    [field:  SerializeField] private TextMeshProUGUI TextDescription { get; set; }
    private Vector3 closeScaleVector = Vector3.zero;

    public void Init(UnityAction yesAction, UnityAction noAction, Vector3? closeVec = null)
    {
        closeScaleVector = closeVec ?? Vector3.zero;
        UIOpen();
        BtnYes.onClick.AddListener(() => CloseGUI(yesAction));
        BtnNo.onClick.AddListener(() => CloseGUI(noAction));
    }


    public override void UIOpen()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 1f);
    }


    public override void UIClose()
    {
        BtnYes.onClick.RemoveAllListeners();
        BtnNo.onClick.RemoveAllListeners();
    }


    private void CloseGUI(UnityAction action)
    {
        Sequence temp= null;
        temp.AppendCallback(() => action());
        temp.Append(transform.DOScale(closeScaleVector, 1f));
        temp.AppendCallback(() => gameObject.SetActive(false));
        UIClose();
    }
}
