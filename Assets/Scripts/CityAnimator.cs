using UnityEngine;

public class CityAnimator : MonoBehaviour
{
    [Header("Objects")]
    public Transform[] cars;
    public GameObject cycleRing;

    private Vector3[] initialPositions;
    private bool isDeadlocked = false;

    void Start()
    {
        // Save the starting positions of all cars
        initialPositions = new Vector3[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i] != null)
                initialPositions[i] = cars[i].localPosition;
        }
        if (cycleRing != null) cycleRing.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            if (cars[i] == null || !cars[i].gameObject.activeInHierarchy) continue;

            // 1. Idle Bob (Sine wave)
            float bob = Mathf.Sin(Time.time * 2f + i) * 0.02f;
            Vector3 targetPos = initialPositions[i] + new Vector3(0, bob, 0);

            // 2. Deadlock Shake (Rapid random movement)
            if (isDeadlocked)
            {
                targetPos += Random.insideUnitSphere * 0.02f;
            }

            cars[i].localPosition = targetPos;
        }

        // 3. Cycle Ring Pulse
        if (isDeadlocked && cycleRing != null && cycleRing.activeInHierarchy)
        {
            float pulse = 1.0f + Mathf.PingPong(Time.time * 0.5f, 0.1f);
            cycleRing.transform.localScale = new Vector3(1.6f * pulse, 0.01f, 1.6f * pulse);
        }
    }

    public void StartDeadlock()
    {
        isDeadlocked = true;
        if (cycleRing != null) cycleRing.SetActive(true);
    }

    public void StopDeadlock()
    {
        isDeadlocked = false;
        if (cycleRing != null) cycleRing.SetActive(false);
    }
}