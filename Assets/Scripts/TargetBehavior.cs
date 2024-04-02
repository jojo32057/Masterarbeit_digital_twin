using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JohannesMasterarbeit


{
    public class TargetBehavior : MonoBehaviour
    {
        public Vector3 direction;
        public Vector3 startPosition;
        public float speed; 
        public Vector3 current; 


        private float startTime;

        private void Start()
        {
            startTime = Time.time;
        }

        private void Awake()
        {
            startPosition = transform.position;
            current = transform.position + current;
        }

        void Update()
        {
            //just move the object from a to b and back
            //transform.position = Start + Dir * Mathf.Sin(Time.timeSinceLevelLoad);

            float distanceCovered = (Time.time - startTime) * speed;
            float journeyLength = Vector3.Distance(startPosition, current);
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, current, fractionOfJourney) + (direction * Mathf.Sin(Time.time * speed));


        }
    }
}