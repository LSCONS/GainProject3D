using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// ���� �켱 Ž��(DFS)�� ������Ʈ�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <typeparam name="T">ã���� �ϴ� ������Ʈ�� �̸�</typeparam>
    /// <param name="parent">Ž���� ������ ������Ʈ</param>
    /// <returns>ã�� ������Ʈ�� ��ȯ. ������ null��ȯ</returns>
    public static T GetComponentDFS<T>(this Transform parent) where T : Component
    {
        T tempT = parent.GetComponent<T>();
        if (tempT == null)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                tempT = parent.transform.GetChild(i).GetComponentDFS<T>();
                if (tempT != null) return tempT;

            }
        }
        return tempT;
    }


    /// <summary>
    /// �ʺ� �켱 Ž��(BFS)�� ������Ʈ�� ��ȯ�ϴ� �޼���
    /// </summary>
    /// <typeparam name="T">ã���� �ϴ� ������Ʈ�� �̸�</typeparam>
    /// <param name="parent">Ž���� ������ ������Ʈ</param>
    /// <returns>ã�� ������Ʈ�� ��ȯ. ���ٸ� null��ȯ</returns>
    public static T GetComponentBFS<T>(this Transform parent) where T : Component
    {
        Queue<Transform> transforms = new Queue<Transform>();
        transforms.Enqueue(parent);

        while(transforms.Count > 0)
        {
            Transform curTransform = transforms.Dequeue();

            T tempT = curTransform.GetComponent<T>();
            if (tempT != null) return tempT;

            for(int i = 0; i < curTransform.childCount; i++)
            {
                transforms.Enqueue(curTransform.GetChild(i));
            }
        }
        return null;
    }


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
}
