using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// �ش� ������Ʈ�� ��� �ڽĵ��� DFS ������� ����ϸ� ���� �̸��� ������Ʈ�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <param name="parent">������ ���� �θ� ������Ʈ</param>
    /// <param name="name">ã�� �̸�</param>
    /// <returns>ã�Ҵٸ� �ش� ������Ʈ��, �����ϸ� null��ȯ</returns>
    public static Transform GetGameObjectSameNameDFS(this Transform parent, string name)
    {
        if (parent.name == name) return parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform tempTransform = parent.GetChild(i).GetGameObjectSameNameDFS(name);
            if (tempTransform != null) return tempTransform;
        }

        return null;
    }


    /// <summary>
    /// �ش� �̸��� ������Ʈ�� ã�� ������Ʈ�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <typeparam name="T">ã�� ������Ʈ�� �̸�</typeparam>
    /// <param name="name">ã�� ������Ʈ�� �̸�</param>
    /// <param name="isDebug">Debug�� ���� ����, �⺻���� true</param>
    /// <param name="SearchDisable">��Ȱ��ȭ ������Ʈ�� ã�� ���� ���� ����, �⺻���� true</param>
    /// <returns>������Ʈ�� ã���� ��ȯ, ��ã���� null�̴�</returns>
    public static T GetComponentNameDFS<T>(this string name, bool isDebug = true, bool SearchDisable = true) where T : Component
    {
        Transform findTransform = GameObject.Find(name)?.transform;
        if(findTransform == null)
        {
            if(isDebug) Debug.LogError($"{name} is null");
            return null;
        }

        T tempT = findTransform.GetComponentInChildren<T>(SearchDisable);
        if (tempT == null && isDebug) Debug.LogError($"{typeof(T)} is null");
        return tempT;
    }


    /// <summary>
    /// FindFirstObjectByType�� �Ȱ����� �ڵ����� ������� ������ش�.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isDebug"></param>
    /// <returns></returns>
    public static T FindFirstObjectByTypeDebug<T>(bool isDebug = true) where T : Component
    {
        T tempT = Object.FindFirstObjectByType<T>();
        if (tempT == null && isDebug) Debug.LogError($"{typeof(T)} is null");
        return tempT;
    }


    /// <summary>
    /// Ư�� Ʈ������ ���� ������Ʈ �� ���ϴ� �̸��� ������Ʈ���� ������Ʈ�� ã�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <typeparam name="T">ã�� ���� ������Ʈ</typeparam>
    /// <param name="parent">ã�� ������ �θ� ������Ʈ</param>
    /// <param name="name">�θ� ������Ʈ���� ã�� �̸�</param>
    /// <param name="isDebug">����� ����, �⺻���� ���</param>
    /// <param name="SearchDisable">��Ȱ��ȭ ������Ʈ Ž�� ����, �⺻���� Ž��</param>
    /// <returns></returns>
    public static T TransformFindAndGetComponent<T>(this Transform parent, string name, bool isDebug = true, bool SearchDisable = true) where T : Component
    {
        Transform transform = parent.Find(name);
        if(transform == null)
        {
            if (isDebug) Debug.LogError($"{name} is null");
            return null;
        }

        T tempT = transform.GetComponentInChildren<T>(SearchDisable);
        if (tempT == null && isDebug) Debug.LogError($"{typeof(T)} is null");
        return tempT;
    }


    /// <summary>
    /// ���� GetComponent���� Debug�� �߰��� �޼���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parent"></param>
    /// <param name="isDebug"></param>
    /// <returns></returns>
    public static T GetComponentDebug<T>(this Transform parent, bool isDebug = true)
    {
        T tempT = parent.GetComponent<T>();
        if (tempT == null && isDebug) Debug.LogError($"{typeof(T)} is null");
        return tempT;
    }
}
