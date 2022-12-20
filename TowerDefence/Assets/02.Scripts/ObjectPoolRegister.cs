using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPool;

public class ObjectPoolRegister : MonoBehaviour
{
    [SerializeField] private List<ObjectPool.Element> _elements;

    private void Awake()
    {
        foreach (ObjectPool.Element element in _elements)
        {
            if (string.IsNullOrEmpty(element.Name))
                ObjectPool.Instance.AddElement(new Element(element.Name, element.Prefab, element.Num));
            else
                Instance.AddElement(element);
        }
    }
}
