using System;
using UnityEngine;

public class ClockAnimate : MonoBehaviour
{
    [Header("Clock Hands")]
    [SerializeField] private Transform seconds;
    [SerializeField] private Transform minutes;
    [SerializeField] private Transform hours;

    [Header("Sound")]
    [SerializeField] private AudioClip tickSound; // Sonido del tic
    private AudioSource audioSource;

    private float timeAccumulator = 0f;

    private int currentSecond = 0;
    private int currentMinute = 0;
    private int currentHour = 0;

    private bool isLowTick = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        // Initialize at current hour
        DateTime now = DateTime.Now;
        currentSecond = now.Second;
        currentMinute = now.Minute;
        currentHour = (now.Hour - 3) % 12; // Convert to 12-hour format
    }

    void Update()
    {
        timeAccumulator += Time.deltaTime;

        if (timeAccumulator >= 1f)
        {
            timeAccumulator -= 1f;
            
            // avanzar segundos
            currentSecond++;
            if (currentSecond >= 60)
            {
                currentSecond = 0;
                currentMinute++;

                if (currentMinute >= 60)
                {
                    currentMinute = 0;
                    currentHour++;

                    if (currentHour >= 12)
                        currentHour = 0;
                }
            }

            UpdateClockHands();
            PlayTickSound();
        }
    }

    void UpdateClockHands()
    {
        float secondsAngle = 6f * currentSecond;      // 360 / 60
        float minutesAngle = 6f * currentMinute;     // incluye progreso de segundos
        float hoursAngle = 30f * currentHour;       // incluye progreso de minutos

        if (seconds != null) seconds.localRotation = Quaternion.Euler(0, 0, secondsAngle);
        if (minutes != null) minutes.localRotation = Quaternion.Euler(0, 0, minutesAngle);
        if (hours != null) hours.localRotation = Quaternion.Euler(0, 0, hoursAngle);
    }

    void PlayTickSound()
    {
        if (tickSound == null) return;

        audioSource.pitch = isLowTick ? 0.85f : 1.0f;
        audioSource.PlayOneShot(tickSound);
        isLowTick = !isLowTick;
    }
}