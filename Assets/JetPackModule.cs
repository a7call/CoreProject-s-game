using UnityEngine;

public class JetPackModule : ActiveObjects
{

    [SerializeField] protected GameObject Flaque;
    private bool isAlreadyFlying;
    public Transform packGFX;
    private Rigidbody2D rbPackGFX;
    private Rigidbody2D rbPlayer;
    private GameObject player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        rbPackGFX = packGFX.GetComponent<Rigidbody2D>();
        rbPlayer = player.GetComponent<Rigidbody2D>();
        rbPackGFX.gravityScale = 0;
    }
    protected override void Update()
    {
        if (isAlreadyFlying)
        {
            isAlreadyFlying = false;
        }
        if (Input.GetKey(KeyCode.U) && readyToUse)
        {
            StartCoroutine(CdToReUse());
            readyToUse = false;
            StartFlying();
        }
    }

    private void StartFlying()
    {
        isAlreadyFlying = true;
        rbPackGFX.isKinematic = false;
        rbPackGFX.gravityScale = 0;
        rbPlayer.gravityScale = 0;
        rbPackGFX.AddForce(new Vector2(0, 50));
        rbPlayer.AddForce(new Vector2(0, 50));
        rbPackGFX.isKinematic = false;
        rbPlayer.gravityScale = 0;
    }
}
