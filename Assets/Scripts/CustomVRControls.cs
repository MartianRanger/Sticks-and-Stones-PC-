using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class CustomVRControls : MonoBehaviour {
    //All of the main functions the player can do besides moving around (I wasn't going to touch that and instantly ruin the whole project)

    //public Rigidbody bullet;
    //private float nextFireTime;
    //public Transform fireTransform;

    public float launchForce = 20f;
    public GameObject bomb;
    public GameObject bombPrefab;
    public Rigidbody player;
    private bool speechBubble = true;
    private float bombForce = 5000.0f;
    public Transform bombSpawn;
    public GameObject speechPrefab;
    private GameObject temp;
    public Slider audioSlider;
    public Slider healthBar;
    public bool isSpeaking;
    public AudioSource audioSource;
    public AudioClip _audioClip;
    public AudioClip recording;
    private float startRecordingTime;
    public bool recorded;
    public GameObject bubble;
    public float speechBubbleSpeed = 500f;
    public Transform speechBubbleSpawn;
    public float speechSpeed = 0;
    public int currentHealth = maxHealth;
    public const int maxHealth = 100;

    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        recorded = false;
        healthBar.maxValue = maxHealth;
        gameManager = Object.FindObjectOfType<GameManager>();

    }


    /*public void Fire(float launchForce, float fireRate)
    {
        if(Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Rigidbody bulletInstance = Instantiate(bullet, fireTransform.position, fireTransform.rotation) as Rigidbody;
            bulletInstance.velocity = launchForce * fireTransform.forward;
        }
    }*/

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Current Health:" + currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            Debug.Log("Dead!");
            gameManager.GameOver();

        }
        healthBar.value = currentHealth;
        //enemyAnimator.SetInteger("currentHealth", currentHealth);
    }

    public void StartRecord() //Methods to start recording with the mic
    {
        int minFreq;
        int maxFreq;
        int freq = 44100;
        Microphone.GetDeviceCaps("", out minFreq, out maxFreq);

        if (maxFreq < 44100)
            freq = maxFreq;

        recording = Microphone.Start("", false, 300, 44100);
        startRecordingTime = Time.time;
        Debug.Log("Going Now!");
    }

    public void StopRecord() //Methods to stop recording with the mic and save the recording into an AudioClip
    {
        Microphone.End("");

        AudioClip recordingNew = AudioClip.Create(recording.name, (int)((Time.time - startRecordingTime) * recording.frequency), recording.channels, recording.frequency, false);
        float[] data = new float[(int)((Time.time - startRecordingTime) * recording.frequency)];
        recording.GetData(data, 0);
        recordingNew.SetData(data, 0);
        this.recording = recordingNew;

        audioSource.clip = recording;
        speechSpeed = audioSource.time;
        audioSlider.value = speechSpeed;

        ShowRecording(speechSpeed);
        Debug.Log("Temp Destroyed");
        Debug.Log("Audio clip length : " + audioSource.clip.length);
        audioSource.Play();
        bubble.GetComponent<PlayerSpeechBubble>().CreateSpeechBubble(recording);
    }
    public void ShowRecording(float currentRecordingTime)
    {
        currentRecordingTime = speechSpeed;
    }
    void Update () {
        // returns true if the primary button (typically “A”) is currently pressed.
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            isSpeaking = true;
            Debug.Log("Starting to Record");
            StartRecord();
        }
        if (OVRInput.GetUp(OVRInput.Button.One))
        {
            Debug.Log("Ending to Record");

            StopRecord();
        }
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            GameObject temp = new GameObject();
            temp = Instantiate(bubble, speechBubbleSpawn.position, speechBubbleSpawn.rotation);
            speechSpeed = 0;

            temp.GetComponent<Rigidbody>().AddForce(speechBubbleSpawn.forward * speechBubbleSpeed, ForceMode.Impulse);
            //Fire(launchForce, 1);
        }
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            bomb = Instantiate(bombPrefab, bombSpawn.position, bombSpawn.rotation);
            bomb.GetComponent<Rigidbody>().useGravity = true;
            bomb.GetComponent<Rigidbody>().AddForce(bombSpawn.forward * bombForce);
            Debug.Log(bomb.GetComponent<Rigidbody>().velocity);
            speechBubble = true;
            Debug.Log("BOMB!");
        }
        //audioSlider.maxValue = temp.GetComponent<AudioSource>().clip.length;
        //audioSlider.value = temp.GetComponent<AudioSource>().time;

        // returns true if the primary button (typically “A”) was pressed this frame.
        OVRInput.GetDown(OVRInput.Button.One);

        // returns true if the “X” button was released this frame.
        OVRInput.GetUp(OVRInput.RawButton.X);

        // returns a Vector2 of the primary (typically the Left) thumbstick’s current state. 
        // (X/Y range of -1.0f to 1.0f)
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // returns true if the primary thumbstick is currently pressed (clicked as a button)
        OVRInput.Get(OVRInput.Button.PrimaryThumbstick);

        // returns true if the primary thumbstick has been moved upwards more than halfway.  
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);

        // returns a float of the secondary (typically the Right) index finger trigger’s current state.  
        // (range of 0.0f to 1.0f)
        OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        // returns a float of the left index finger trigger’s current state.  
        // (range of 0.0f to 1.0f)
        OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

        // returns true if the left index finger trigger has been pressed more than halfway.  
        // (Interpret the trigger as a button).
        OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

        // returns true if the secondary gamepad button, typically “B”, is currently touched by the user.
        OVRInput.Get(OVRInput.Touch.Two);

        // returns true after a Gear VR touchpad tap
        OVRInput.GetDown(OVRInput.Button.One);

        // returns true on the frame when a user’s finger pulled off Gear VR touchpad controller on a swipe down
        OVRInput.GetDown(OVRInput.Button.DpadDown);

        // returns true the frame AFTER user’s finger pulled off Gear VR touchpad controller on a swipe right
        OVRInput.GetUp(OVRInput.RawButton.DpadRight);

        // returns true if the Gear VR back button is pressed
        OVRInput.Get(OVRInput.Button.Two);

        // Returns true if the the Gear VR Controller trigger is pressed down
        OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger);

        // Queries active Gear VR Controller touchpad click position 
        // (normalized to a -1.0, 1.0 range, where -1.0, -1.0 is the lower-left corner)
        OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, OVRInput.Controller.RTrackedRemote);

        // If no controller is specified, queries the touchpad position of the active Gear VR Controller
        OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

        // returns true if the Gear VR Controller back button is pressed
        OVRInput.Get(OVRInput.Button.Back);

        // recenters the active Gear VR Controller. Has no effect for other controller types.
        OVRInput.RecenterController();
    }
    
}
