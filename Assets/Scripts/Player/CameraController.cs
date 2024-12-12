using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ParticleSystem speedUpParitcles;
    [Header("Zoom Settings")]
    [SerializeField] float minFOV = 40;
    [SerializeField] float maxFOV = 80;
    [SerializeField] float zoomDuration = 1f;
    [SerializeField] float zoomSpeedModifier = 2f;

    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }
    public void ChangeCameraFOV(float amount)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeFOVRoutine(amount));

        if (amount > 0) speedUpParitcles.Play();
    }

    IEnumerator ChangeFOVRoutine(float amount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + amount * zoomSpeedModifier, minFOV, maxFOV);

        float elapsedTime = 0f;

        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null;
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
