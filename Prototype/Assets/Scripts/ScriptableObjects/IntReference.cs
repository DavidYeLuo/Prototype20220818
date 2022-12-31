using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// Integer Reference which allows users to store data like a "file".<br/>
/// Optional: Notify to subscribers when value changes
/// </summary>
[CreateAssetMenu(menuName = "DataReference/IntReference", fileName = "IntReference")]
[System.Serializable]
public class IntReference: ScriptableObject
{
    [Header("Debug")]
    [Tooltip("Changing value in the inspector doesn't do anything.")]
    [SerializeField] private int value;

    public delegate void notify();

    public event notify subscribers;

    /// <summary>Get the value.</summary>
    /// <returns>value of the integer.</returns>
    public int Get()
    {
        return value;
    }

    /// <summary>Sets the value and notify subscribers.</summary>
    /// <param name="value">value of the integer.</param>
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