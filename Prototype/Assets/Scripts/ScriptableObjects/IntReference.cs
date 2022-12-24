using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "DataReference/IntReference", fileName = "IntReference")]
[System.Serializable]
public class IntReference: ScriptableObject
{
    [Header("Debug")]
    [Tooltip("Changing value in the inspector doesn't do anything.")]
    [SerializeField] private int value;

    public delegate void notify();

    public event notify subscribers;

    public int Get()
    {
        return value;
    }

    public void Set(int value)
    {
        this.value = value;
        NotifySubscribers();
    }

    private void NotifySubscribers()
    {
        if (subscribers == null) return;
        subscribers.Invoke();
    }
}