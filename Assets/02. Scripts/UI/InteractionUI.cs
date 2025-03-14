using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VInspector;

public class InteractionUI : MonoBehaviour
{
    [ShowInInspector, ReadOnly]
    private TextMeshProUGUI _titleText;
    [ShowInInspector, ReadOnly]
    private TextMeshProUGUI _infoText;


    private void OnValidate()
    {
        InIt();
    }

    private void InIt()
    {
        if (_titleText == null) _titleText = transform.TransformFindAndGetComponent<TextMeshProUGUI>("TitleText");
        if (_infoText == null) _infoText = transform.TransformFindAndGetComponent<TextMeshProUGUI>("InfoText");
    }

    private void Awake()
    {
        InIt();
    }


    //상호작용하는 오브젝트를 가지고 ItemData를 추출하여 텍스트에 적용하는 메서드 
    public void OnLoadText(ItemObject itemObject)
    {
        _titleText.text = itemObject.data.nameItem;
        _infoText.text = itemObject.data.infoItem;
    }
}
