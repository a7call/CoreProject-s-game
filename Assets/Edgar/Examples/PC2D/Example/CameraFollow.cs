using System.Collections;
using UnityEngine;


    public class CameraFollow : MonoBehaviour
    {
        public Transform target;

        // Update is called once per frame
        void Update()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;

            transform.position = pos;

            if (Input.GetKeyDown("p"))
            {
                StartShake(Duration, Power);
            }
        }

        protected float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;

        [SerializeField] protected float rotationMultiplier;
        [SerializeField] protected float Power;
        [SerializeField] protected float Duration;
        protected bool Shake = false;
        protected bool Translation = false;
        protected Vector3 direction;

        public void StartShake(float length, float power)
        {
        
            shakeTimeRemaining = length;
            shakePower = power;

            shakeFadeTime = power / length;

            shakeRotation = power * rotationMultiplier;

            Shake = true;
        }

        public void StartTranslation(float length, float power, Vector3 shakeDirection)
        {

            shakeTimeRemaining = length;
            shakePower = power;

            shakeFadeTime = power / length;

            shakeRotation = power * rotationMultiplier;

            direction = shakeDirection;

            Translation = true;
    }

    private void LateUpdate()
        {
        if (shakeTimeRemaining > 0 && Shake)
            {

             shakeTimeRemaining -= Time.deltaTime;

             float x = Random.Range(-1f, 1f) * shakePower;
             float y = Random.Range(-1f, 1f) * shakePower;

             transform.localPosition = transform.localPosition + new Vector3(x,y);

             shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

             shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);

            if (shakeTimeRemaining <= 0)
            {
                Shake = false;
            }
            transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
        }
            
         
        if (shakeTimeRemaining > 0 && Translation)
        {

            shakeTimeRemaining -= Time.deltaTime;

            float x = Random.Range(0.2f,1f) * shakePower;
                
            transform.localPosition = transform.localPosition + direction * x;

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

            if(shakeTimeRemaining <= 0)
            {
                Translation = false;
            }
        }
       

    }

    }
