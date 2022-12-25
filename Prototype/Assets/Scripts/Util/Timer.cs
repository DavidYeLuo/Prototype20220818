using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Util
{
    public class Timer
    {
        public delegate void timeUp();
        public event timeUp timeUpEvent;

        public IEnumerator SetTimer(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (timeUpEvent == null) yield return null;
            timeUpEvent.Invoke();
        }
    }
}