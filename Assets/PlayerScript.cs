using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerScript : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private GameManager gameManager;

    public AudioSource audioSource;
    public AudioClip _audioClip;
    public AudioClip recording;
    private float startRecordingTime;
    public bool recorded;
    public bool speaking;

    public GameObject bubble;
    public float speechBubbleSpeed = 500f;
    public Transform speechBubbleSpawn;

    public float speechSpeed = 0;
    public int currentHealth = maxHealth;
    public const int maxHealth = 100;

    public Slider audioSlider;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        recorded = false;
        healthBar.maxValue = maxHealth;
        gameManager = Object.FindObjectOfType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        /*
                 if (Input.GetMouseButtonDown(1))
        {
            isSpeaking = true;
            Debug.Log("Starting to Record");
            StartRecord();
        }
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Ending to Record");

            StopRecord();
        }
        if (Input.GetMouseButtonDown(0) && recorded = true)
        {
            GameObject temp = new GameObject();
            temp = Instantiate(bubble, speechBubbleSpawn.position, speechBubbleSpawn.rotation);
            speechSpeed = 0;

            temp.GetComponent<Rigidbody>().AddForce(speechBubbleSpawn.forward * speechBubbleSpeed, ForceMode.Impulse);
            recorded = false;
            //Fire(launchForce, 1);
        }

         */
    }

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
        bubble.GetComponent<PlayerSpeechBubble>().CreateSpeechBubble(recording);
        recorded = true;

    }

    public void ShowRecording(float currentRecordingTime)
    {
        currentRecordingTime = speechSpeed;
    }


}
