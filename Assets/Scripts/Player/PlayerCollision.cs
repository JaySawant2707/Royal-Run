using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float hitCooldown = 1f;
    [SerializeField] float changeMoveSpeedAmount = -1f;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip appleSound;
    [SerializeField] AudioClip hurtSound;

    float cooldownTimer;
    const string hitString = "Hit";

    AudioSource audioSource;
    Animator animator;
    LevelGenerator levelGenerator;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (cooldownTimer < hitCooldown) return;

        levelGenerator.ChangeChunkMoveSpeed(changeMoveSpeedAmount);
        animator.SetTrigger(hitString);
        PlayHurtSound();
        cooldownTimer = 0f;
    }

    void PlayHurtSound()
    {
        audioSource.clip = hurtSound;
        audioSource.Play();
    }

    public void PlayCoinSound()
    {
        audioSource.clip = coinSound;
        audioSource.Play();
    }

    public void PlayAppleSound()
    {
        audioSource.clip = appleSound;
        audioSource.Play();
    }
}
