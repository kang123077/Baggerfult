using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    static ObjectPool _inst = null;
    public static ObjectPool inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<ObjectPool>();
                if (_inst == null)
                {
                    // 메모리 상에도 없으면 생성
                    GameObject obj = new GameObject();
                    obj.name = "ObjectPool";
                    _inst = obj.AddComponent<ObjectPool>();
                }
            }
            return _inst;
        }
    }

    Dictionary<string, Queue<GameObject>> myPool = new Dictionary<string, Queue<GameObject>>();
    Dictionary<string, Transform> myExplore = new Dictionary<string, Transform>();

    /// <summary>
    /// 게임 오브젝트 안의 오브젝트를 전달받고, 없으면 새로 생성한다.
    /// p의 디폴트는 null이고, 전달하면 해당 값이 된다.
    /// </summary>
    public GameObject GetObject<T>(GameObject org, Transform p = null)
    {
        // T라는 불명의 클래스의 이름을 검색한다
        string Name = typeof(T).Name;

        if (myPool.ContainsKey(Name))
        {
            if (myPool[Name].Count > 0)
            {
                GameObject obj = myPool[Name].Dequeue();
                obj.SetActive(true);
                obj.transform.SetParent(p);
                return obj;
            }
        }
        return Instantiate(org, p, true);
    }
    /// <summary>
    /// 오브젝트를 풀에 반납하고, 전용 큐가 없으면 생성한다
    /// </summary>
    public void ReleaseObject<T>(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);

        string Name = typeof(T).Name;
        if (!myPool.ContainsKey(Name))
        {
            myPool[Name] = new Queue<GameObject>();
            GameObject dir = new GameObject(Name + "Pool");
            dir.transform.SetParent(transform);
            myExplore[Name + "Pool"] = dir.transform;
        }
        obj.transform.SetParent(myExplore[Name + "Pool"]);
        myPool[Name].Enqueue(obj);
    }
}
