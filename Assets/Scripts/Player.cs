using System;
using DefaultNamespace;
using DefaultNamespace.IGameStates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : BaseDamageable, ILifeApply, IStartGame, IGamePause, IGameResume, IGameRestart
{
    [SerializeField] private Animator animator;
    [SerializeField] private Animation _animation;
    [SerializeField] private Camera camera;
    [Header("Shoot")] [SerializeField] Transform pivot;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Bullet Bullet;
    [SerializeField] private float viewAngle;
    public UnityEvent OnShoot { get; } = new UnityEvent();
    [Header("")] [SerializeField] public float defaultLifeAmount = 100f;
    [SerializeField] private CharacterController2D controller;
    [SerializeField] private Slider lifeSlider;
    public int coinCount;
    [SerializeField] private Text coinsCount;
    public float inputX;
    [SerializeField] private float rotationSpeed;
    public override float Life { get; protected set; }
    private float direction;
    private bool gameStarted;
    private bool pause;
    private Vector3 playerInitialPosition;

    private void Awake()
    {
        Life = defaultLifeAmount;
        playerInitialPosition = transform.position;

    }

    private void Update()
    {
        if (!gameStarted || pause)
        {
            return;
        }

        coinsCount.text = $"{coinCount}";
        lifeSlider.value = Life;

        setAnimation();
        playerDirection();
        PivotHolderRotation();
        Shoot();
    }

    [ContextMenu("die")]
    private void ddd()
    {
        Apply(-10000);
    }

    private void playerDirection()
    {
        var dir = camera.ScreenToWorldPoint(Input.mousePosition) - pivot.position;
        var scale = transform.localScale;

        direction = Mathf.Abs(scale.x) * (dir.x < 0f ? -1f : 1f);

        if (inputX == 0)
        {
            scale.x = direction;
        }
        else if (inputX != 0)
        {
            scale.x = Mathf.Abs(scale.x) * (inputX < 0f ? -1f : 1f);
        }

        transform.localScale = scale;
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
            DecreaseLife(Mathf.Abs(value));
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
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, 0f, direction * viewAngle);

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

    public void StartGame()
    {
        gameStarted = true;
    }

    public void PauseGame()
    {
        pause = true;
    }

    public void ResumeGame()
    {
        pause = false;
    }

    public void RestartGame()
    {
        gameObject.SetActive(true);
        transform.position = playerInitialPosition;
        Life = defaultLifeAmount;
        gameStarted = true;
        pause = false;
    }
}