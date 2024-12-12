using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] float hitCooldown = 1f;
    [SerializeField] float changeMoveSpeedAmount = -1f;

    float cooldownTimer;
    const string hitString = "Hit";
    
    Animator animator;
    LevelGenerator levelGenerator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
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
        cooldownTimer = 0f;
    }
}
