using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticles;
    [SerializeField] AudioSource boulderAudioSource;
    [SerializeField] float shakeModifier = 1f;
    [SerializeField] float collisionCooldown = 1f;

    float collisionTimer = 1f;
    CinemachineImpulseSource cinemachineImpulseSource;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        if (collisionTimer < collisionCooldown) return;

        ScreenShake();
        CollisionFX(other);
        collisionTimer = 0;
    }

    private void ScreenShake()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1 / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticles.transform.position = contactPoint.point;
        collisionParticles.Play();
        boulderAudioSource.Play();
    }
}
