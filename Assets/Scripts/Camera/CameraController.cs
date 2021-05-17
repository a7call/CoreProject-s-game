using UnityEngine;

//namespace CoreProjectGame.CameraController
//{
//}

public class CameraController : MonoBehaviour
{
    # region Variables

    public static CameraController instance;

    private Transform player;

    private Vector3 target, mousePos, refVel, shakeOffset;

    // Distance à laquelle la caméra se situe lorsque notre souris est sur un bord de l'écran
    [SerializeField] private float cameraDist = 3.5f;

    [SerializeField] private float smoothTime = 0.2f;

    private float zStart;

    private float shakeMag, shakeTimeEnd;

    private Vector3 shakeVector;

    private bool isShaking;

    #endregion

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;

    }

    private void Start()
    {
        target = player.transform.position;
        zStart = transform.position.z;
    }

    private void Update()
    {
        mousePos = CaptureMousePos();
        target = UpdateTargetPos();
        shakeOffset = UpdateShake();
        UpdateCameraPosition();
    }

    private Vector3 CaptureMousePos()
    {
        Vector2 ret = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        ret *= 2;
        ret -= Vector2.one;
        float max = 0.9f;

        if(Mathf.Abs(ret.x) > max || Mathf.Abs(ret.y) > max)
        {
            ret = ret.normalized;
        }

        return ret;
    }

    private Vector3 UpdateTargetPos()
    {
        Vector3 mouseOffset = mousePos * cameraDist;
        Vector3 ret = player.position + mouseOffset;
        ret += shakeOffset;
        ret.z = zStart;
        return ret;
    }

    private void UpdateCameraPosition()
    {
        Vector3 tempPos;
        tempPos = Vector3.SmoothDamp(transform.position, target, ref refVel, smoothTime);
        transform.position = tempPos;
    }

    public void Shake(Vector3 direction, float magnitude, float length)
    {
        isShaking = true;
        shakeVector = direction;
        shakeMag = magnitude;
        shakeTimeEnd = Time.time + length;
    }

    private Vector3 UpdateShake()
    {
        if(!isShaking || shakeTimeEnd < Time.time)
        {
            isShaking = false;
            return Vector3.zero;
        }
        Vector3 tempOffset = shakeVector;
        tempOffset *= shakeMag;
        return tempOffset;
    }

}

