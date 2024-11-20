using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandeler : MonoBehaviour
{
   [SerializeField] float WaitTime = 1.5f;
   [SerializeField] AudioClip CrashSFX;
   [SerializeField] AudioClip SucesSFX;
   [SerializeField] ParticleSystem SucessParticles;
   [SerializeField] ParticleSystem CrashParticles;

   AudioSource audioSource;
   bool isControllable = true;
   bool isCollidable = true;
   void Start()
   {
    audioSource = GetComponent<AudioSource>();
   }
   private void Update() 
   {
        RespondToDebugKeys(); 
   }
   private void OnCollisionEnter(Collision other) 
   {
        if(!isControllable || !isCollidable){return;}
        
        switch(other.gameObject.tag)
        {
            case "Obstacle":
                StartCrashSequence();
                break;
            case "Finish":
                StartFinishSequence();
                break;
        }
   }

    void StartFinishSequence()
    {
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(SucesSFX);
        SucessParticles.Play();
        Invoke("NextLevel",WaitTime);
    }

    void StartCrashSequence()
    {
        isControllable = false;
        GetComponent<Movement>().enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(CrashSFX);
        CrashParticles.Play();
        Invoke("ReloadLevel",WaitTime);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    } 
    void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
    void RespondToDebugKeys()
    {
        if(Keyboard.current.nKey.wasPressedThisFrame)
        {
            NextLevel();
        }
        else if(Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }
    
}
