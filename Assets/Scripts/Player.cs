using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Player : BaseDamageable, ILifeApply
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animation _animation;
    [SerializeField] private Camera camera;
    [SerializeField] Transform pivot;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Bullet Bullet;
    [SerializeField] public float defaultLifeAmount = 100f;
    public CharacterController2D controller;
    [SerializeField] private float viewAngle;
    
    public UnityEvent OnShoot { get; } = new UnityEvent();
    public float inputX;
    [SerializeField]private float rotationSpeed;
    public override float Life { get; protected set; }
    private bool flag = true;
    private float direction;
    private void Awake()
    {
        Life = defaultLifeAmount;
    }

    private void Update()
    {
       setAnimation();
        var dir = camera.ScreenToWorldPoint(Input.mousePosition) - pivot.position;

        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var scale = transform.localScale;
        direction = Mathf.Abs(scale.x) * (dir.x < 0f ? -1f : 1f);
        scale.x = direction;
        transform.localScale = scale;

        PivotHolderRotation();
        Shoot();

    }


    private void setAnimation()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _animation.Play("Rotateanim");
        }

        inputX = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", inputX);
        controller.Move(inputX);

        animator.SetBool("IsJumping", !controller.IsGrounded && controller.rigidbody2D.velocity.y > 0f);
        animator.SetBool("IsFalling", controller.rigidbody2D.velocity.y < 0f);
        animator.SetFloat("Velocity", controller.rigidbody2D.velocity.y);

        animator.SetBool("IsMoveing", inputX != 0f);

    }

    public void Apply(float value)
    {
        if (value < 0f)
        {
            DecreaseLife(value);
        }
        else
        {
            IncreaseLife(value);
        }

        Debug.Log(Life);
    }

    public bool CanApply()
    {
        return Life < maxLife;
    }

    private void PivotHolderRotation()
    {
        var mouse = camera.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        var dir = (mouse - pivot.position);
        var angle =  Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, direction*viewAngle);  

        pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(Vector3.forward * angle),
            Time.deltaTime * rotationSpeed);

    }

    private void shoot()
    {
        var currentPos = camera.ScreenToWorldPoint(Input.mousePosition);
        var dir = currentPos - transform.position;
        var bullet = Instantiate(Bullet, shootPoint.position, Quaternion.identity);
        bullet.Shoot(dir);

        OnShoot?.Invoke();
    }

    private void Shoot()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        shoot();
    }
}