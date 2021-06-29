using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Player : BaseDamageable, ILifeApply
{
    [SerializeField] private Animator animator;
    [SerializeField] public float defaultLifeAmount = 100f;
    [SerializeField] private Animation _animation;
    [SerializeField] private Camera camera;
    [SerializeField] Transform pivot;
    [SerializeField] private Transform shootPoint;
    public CharacterController2D controller;
    [SerializeField] private Bullet shootingItem;
    public UnityEvent OnShoot { get; } = new UnityEvent();
    public float inputX;
    public override float Life { get; protected set; }

    private void Awake()
    {
        
        Life = defaultLifeAmount;
    }

    private void Update()
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


        if (inputX != 0)
        {
            var scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (inputX < 0f ? -1f : 1f);
            transform.localScale = scale;
        }

        PivotHolderRotation();
        Shoot();
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
    }

    public bool CanApply()
    {
        return Life < maxLife;
    }

    private void PivotHolderRotation()
    {
        var mouse = camera.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0f;
        var pos = transform.position;
        var clampValue = Mathf.Clamp(mouse.x, -30f, 30f);

        pos.x = clampValue;
        shootPoint.transform.position = Vector3.Lerp(shootPoint.transform.position, pos, Time.deltaTime * 50f);
        var dir = mouse - pivot.position;

        var angle = -90f + Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, 30f);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, Quaternion.Euler(Vector3.forward * angle),
            Time.deltaTime * 10f);
    }

    private void shoot()
    {
        var currentPos = camera.ScreenToWorldPoint(Input.mousePosition);
        var dir = currentPos - transform.position;
        var bullet = Instantiate(shootingItem, shootPoint.position, Quaternion.identity);
        bullet.Shoot(dir);

        OnShoot?.Invoke();
    }

    private void Shoot() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }

        shoot();
    }
}