using UnityEngine;


public class CameraFollow : MonoBehaviour
{

    #region Variables

    // Shake
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    private bool isShakingD = false, isShakingG = false;
    [SerializeField] private float rotationMultiplier;
    private Vector3 direction;

    // Follow
    private Transform playerTransform;
    private Vector3 target, mousePos, refVel;
    [SerializeField] private float cameraDist = 2f, smoothTime = 0.1f;
    private float zStart;

    #endregion

    #region MonoBehaviour

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        target = playerTransform.position;
        zStart = transform.position.z;
    }

    void Update()
    {
        // Follow
        mousePos = CaptureMousePos();
        target = UpdateTargetPos();
        UpdateCameraPosition();

        // Shake 
        if (shakeTimeRemaining > 0 && isShakingG) GlobalShake();

        if (shakeTimeRemaining > 0 && isShakingD) DirectionnalShake();
    }

    #endregion

    #region Follow 
    private Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;

        if (Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized;
        }

        return ret;
    }

    private Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = playerTransform.position + mouseOffset;
        ret.z = zStart;
        return ret;
    }

    private void UpdateCameraPosition()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
    }

    #endregion

    #region Shake

    public void StartShakeG(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;

        isShakingG = true;
    }

    public void StartShakeD(float length, float power, Vector3 shakeDirection)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        direction = shakeDirection;

        isShakingD = true;
    }

    private void GlobalShake()
    {
        shakeTimeRemaining -= Time.deltaTime;

        float x = Random.Range(-1f, 1f) * shakePower;
        float y = Random.Range(-1f, 1f) * shakePower;

        transform.position = transform.position + new Vector3(x, y);
        transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));

        shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

        shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFadeTime * rotationMultiplier * Time.deltaTime);

        if (shakeTimeRemaining <= 0)
        {
            transform.rotation = Quaternion.identity;
            isShakingG = false;
        }
    }

    private void DirectionnalShake()
    {
        shakeTimeRemaining -= Time.deltaTime;

        transform.localPosition = transform.localPosition + direction * shakePower;

        shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);

        if (shakeTimeRemaining <= 0) isShakingD = false;
    }

    #endregion

}
