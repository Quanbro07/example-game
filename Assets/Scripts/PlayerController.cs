using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 7f;
    [SerializeField] private float doubleTapThreshold = 0.3f;

    // Animation
    private Animator animator;

    private float lastTapTimeA = -1f;
    private float lastTapTimeD = -1f;

    private float lastAttackTime = -1f;
    private float attackThreshold = 0.6f;

    private bool isWalk = false;
    private bool isRun = false;


    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMoveMent();
        HandleAttack();
    }


    void HandleMoveMent()
    {
        
        float moveInputH = Input.GetAxisRaw("Horizontal");
        float moveInputV = Input.GetAxisRaw("Vertical");

        // Kiểm tra người chơi đang giữ phím
        bool holdingLeft = Input.GetKey(KeyCode.A);
        bool holdingRight = Input.GetKey(KeyCode.D);

        // Xử lý double tap để chạy
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTimeA < doubleTapThreshold)
                isRun = true;
            lastTapTimeA = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTimeD < doubleTapThreshold)
                isRun = true;
            lastTapTimeD = Time.time;
        }

        // Nếu thả phím thì dừng chạy
        if (holdingLeft || holdingRight)
        {
            // Nếu không phải chạy thì đi bộ
            if (!isRun)
            {
                isWalk = true;
            }
        }
        else
        {
            // Không giữ phím nào → idle
            isRun = false;
            isWalk = false;
        }

        // Di chuyển
        float moveSpeed = isRun ? runSpeed : walkSpeed;
        rb.linearVelocity = new Vector2(moveInputH * moveSpeed, moveInputV * moveSpeed);

        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));

        // Lật hướng
        if (moveInputH > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInputH < 0) transform.localScale = new Vector3(-1, 1, 1);

       
    }
    
    void HandleAttack()
    {
        if(Input.GetKeyDown(KeyCode.J)) { 
            animator.SetTrigger("AttackTrigger");
        }
    }

}
