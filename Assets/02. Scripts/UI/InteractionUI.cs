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
        _titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        _infoText = transform.Find("InfoText").GetComponent<TextMeshProUGUI>();
    }


    //��ȣ�ۿ��ϴ� ������Ʈ�� ������ ItemData�� �����Ͽ� �ؽ�Ʈ�� �����ϴ� �޼���
    public void OnLoadText(ItemObject itemObject)
    {
        _titleText.text = itemObject.data.nameItem;
        _infoText.text = itemObject.data.infoItem;
    }
}
