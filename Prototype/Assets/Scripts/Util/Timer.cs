using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Util
{
    /// <summary>
    /// Dispatch an event when time is up.
    /// Subscribers should subscribe to timeUpEvent.
    /// To start the timer call SetTimer()
    /// </summary>
    /// 
    /// <remarks>
    /// Requires a Monobehavior class to start the coroutine SetTimer()
    /// </remarks>
    public class Timer
    {
        public delegate void timeUp();
        public event timeUp timeUpEvent;

        /// <summary>
        /// Starts the timer that will dispatch an event when time is up.
        /// Note: This is a coroutine function so use StartCoroutine() to use.
        /// </summary>
        /// <param name="seconds"> time in seconds </param>
        public IEnumerator SetTimer(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (timeUpEvent == null) yield return null;
            timeUpEvent.Invoke();
        }
    }
}