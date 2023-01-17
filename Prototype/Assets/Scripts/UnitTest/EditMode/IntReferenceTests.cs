using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.TestTools;
using Debug = System.Diagnostics.Debug;

public class IntReferenceTests
{
    private bool referenceRecieved;
    
    [Test]
    public void Set_Call_Should_Notify()
    {
        IntReference intReference = ScriptableObject.CreateInstance<IntReference>();
        
        referenceRecieved = false;
        intReference.subscribers += NotificationReceived;
    }
    private void NotificationReceived()
    {
        referenceRecieved = true;
    }
    
    [Test]
    public void Set_Negative_Nine_Expect_Negative_Nine()
    {
        int valueToTest = -9;

        IntReference intReference = ScriptableObject.CreateInstance<IntReference>(); 

        intReference.Set(valueToTest);
        Assert.AreEqual(valueToTest, intReference.Get());
    }
    
    [Test]
    public void Set_Negative_One_Expect_Negative_One()
    {
        int valueToTest = -1;

        IntReference intReference = ScriptableObject.CreateInstance<IntReference>(); 

        intReference.Set(valueToTest);
        Assert.AreEqual(valueToTest, intReference.Get());
    }
    
    [Test]
    public void Set_Zero_Expect_Zero()
    {
        int valueToTest = 0;

        IntReference intReference = ScriptableObject.CreateInstance<IntReference>(); 

        intReference.Set(valueToTest);
        Assert.AreEqual(valueToTest, intReference.Get());
    }
    
    [Test]
    public void Set_Five_Expect_Five()
    {
        int valueToTest = 5;

        IntReference intReference = ScriptableObject.CreateInstance<IntReference>(); 

        intReference.Set(valueToTest);
        Assert.AreEqual(valueToTest, intReference.Get());
    }
    
    [Test]
    public void Set_Ten_Expect_Ten()
    {
        int valueToTest = 10;

        IntReference intReference = ScriptableObject.CreateInstance<IntReference>(); 

        intReference.Set(valueToTest);
        Assert.AreEqual(valueToTest, intReference.Get());
    }
}
