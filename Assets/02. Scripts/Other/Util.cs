using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// 깊이 우선 탐색(DFS)의 컴포넌트를 반환하는 메서드
    /// </summary>
    /// <typeparam name="T">찾고자 하는 컴포넌트의 이름</typeparam>
    /// <param name="parent">탐색을 시작할 오브젝트</param>
    /// <returns>찾은 컴포넌트를 반환. 없으면 null반환</returns>
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
    /// 너비 우선 탐색(BFS)의 컴포넌트를 반환하는 메서드
    /// </summary>
    /// <typeparam name="T">찾고자 하는 컴포넌트의 이름</typeparam>
    /// <param name="parent">탐색을 시작할 오브젝트</param>
    /// <returns>찾은 컴포넌트를 반환. 없다면 null반환</returns>
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
    /// 해당 오브젝트의 모든 자식들을 DFS 방식으로 재귀하며 같은 이름의 오브젝트를 반환하는 메서드
    /// </summary>
    /// <param name="parent">기준을 잡을 부모 오브젝트</param>
    /// <param name="name">찾을 이름</param>
    /// <returns>찾았다면 해당 오브젝트를, 실패하면 null반환</returns>
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
