using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Util;

public class TimerTests
{
    private Stopwatch _stopwatch;
    private float MARGIN_OF_ERROR_IN_SECONDS = 0.3f; 
    
    [UnityTest]
    public IEnumerator OneSecond()
    {
        float oneSecond = 1.0f;
        
        Timer timer;
        MockTimer mockTimer;
        
        Initialize(out timer, out _stopwatch, out mockTimer);
        
        mockTimer.StartTime(timer, oneSecond);
        _stopwatch.Start();

        // Give enough time for the test
        float waitTime = oneSecond + (MARGIN_OF_ERROR_IN_SECONDS * 2);
        yield return new WaitForSeconds(waitTime);

        Assert.AreEqual(oneSecond, _stopwatch.Elapsed.Seconds, MARGIN_OF_ERROR_IN_SECONDS);
    }
    
    [UnityTest]
    public IEnumerator TwoSeconds()
    {
        float timeToTest = 2.0f;
        
        Timer timer;
        MockTimer mockTimer;
        
        Initialize(out timer, out _stopwatch, out mockTimer);
        
        mockTimer.StartTime(timer, timeToTest);
        _stopwatch.Start();

        // Give enough time for the test
        float waitTime = timeToTest + (MARGIN_OF_ERROR_IN_SECONDS * 2);
        yield return new WaitForSeconds(waitTime);

        Assert.AreEqual(timeToTest, _stopwatch.Elapsed.Seconds, MARGIN_OF_ERROR_IN_SECONDS);
    }
    
    [UnityTest]
    public IEnumerator TenSeconds()
    {
        float timeToTest = 10.0f;
        
        Timer timer;
        MockTimer mockTimer;
        
        Initialize(out timer, out _stopwatch, out mockTimer);
        
        mockTimer.StartTime(timer, timeToTest);
        _stopwatch.Start();

        // Give enough time for the test
        float waitTime = timeToTest + (MARGIN_OF_ERROR_IN_SECONDS * 2);
        yield return new WaitForSeconds(waitTime);

        Assert.AreEqual(timeToTest, _stopwatch.Elapsed.Seconds, MARGIN_OF_ERROR_IN_SECONDS);
    }

    private void Initialize(out Timer timer, out Stopwatch stopwatch, out MockTimer mockTimer)
    {
        // Creates Our timer
        timer = new Timer();
        // Creates a C# timer to check the time between our tests
        stopwatch = new Stopwatch();
        // Because our timer needs a monobehaviour, it needs a gameobject to add it as a component.
        mockTimer = new GameObject().AddComponent<MockTimer>();
        
        // Result of the time event is based on the subscriber's output
        // (It is using the observer pattern)
        timer.timeUpEvent += TimeUp;
    }
    
    private void TimeUp()
    {
        _stopwatch.Stop();
    }
    
    private class MockTimer : MonoBehaviour
    {
        public void StartTime(Timer timer, float seconds)
        {
            StartCoroutine(timer.SetTimer(seconds));
        }
    }
}
