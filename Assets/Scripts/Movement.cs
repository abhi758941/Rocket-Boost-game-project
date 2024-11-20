using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float ThrustPower = 10f;
    [SerializeField] float RotationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem LeftBoosterParticle;
    [SerializeField] ParticleSystem RightBoosterParticle;
    [SerializeField] ParticleSystem MainBoosterParticle;
    Rigidbody rb;
    AudioSource audioSource;
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable() 
    {
        thrust.Enable();
        rotation.Enable();
    }
    private void FixedUpdate()
    {
        Thrust();
        Rotation();
    }
    private void Thrust()
    {
        if(thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            audioSource.Stop();
            MainBoosterParticle.Stop();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * ThrustPower);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!MainBoosterParticle.isPlaying)
        {
            MainBoosterParticle.Play();
        }
    }

    private void Rotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(rotationInput < 0)
        {
            RotateLeft();
        }
        else if(rotationInput > 0)
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopRotating()
    {
        RightBoosterParticle.Stop();
        LeftBoosterParticle.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-RotationSpeed);
        if (!LeftBoosterParticle.isPlaying)
        {
            RightBoosterParticle.Stop();
            LeftBoosterParticle.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(RotationSpeed);
        if (!RightBoosterParticle.isPlaying)
        {
            LeftBoosterParticle.Stop();
            RightBoosterParticle.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.fixedDeltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }
}
