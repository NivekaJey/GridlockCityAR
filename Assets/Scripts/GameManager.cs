using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI statusText;
    public Button scanButton;
    public Button towButton;
    public Button backOutButton;

    [Header("3D Objects")]
    public GameObject carP1; // We will tow this car

    public CityAnimator cityAnimator;

    private bool isScanned = false;
    private bool isDeadlockResolved = false;

    void Start()
    {
        // Initial state: Buttons are locked until scan
        towButton.interactable = false;
        backOutButton.interactable = false;
        statusText.text = "Status: Unsafe (Deadlock)";
        statusText.color = Color.red;

        // Link button clicks to functions
        scanButton.onClick.AddListener(OnScanClicked);
        towButton.onClick.AddListener(OnTowClicked);
        backOutButton.onClick.AddListener(OnBackOutClicked);
    }

    void OnScanClicked()
    {
        if (isDeadlockResolved) return;

        isScanned = true;
        statusText.text = "Status: DEADLOCK DETECTED (Cycle in RAG)";
        statusText.color = Color.yellow;

        // Enable recovery options
        towButton.interactable = true;
        backOutButton.interactable = true;

        // Disable scan button to prevent spamming
        scanButton.interactable = false;

        cityAnimator.StartDeadlock();
    }

    void OnTowClicked()
    {
        if (!isScanned || isDeadlockResolved) return;

        // Recovery method: Process Termination (Tow P1)
        carP1.SetActive(false); // Hide the car
        ResolveDeadlock("Process Terminated (Tow Truck)");
    }

    void OnBackOutClicked()
    {
        if (!isScanned || isDeadlockResolved) return;

        // Recovery method: Resource Preemption (Back out P1)
        // Move the car backwards by 0.5 units on the Z axis
        carP1.transform.position -= new Vector3(0, 0, 0.5f);
        ResolveDeadlock("Resource Preempted (Car Backed Out)");
    }

    void ResolveDeadlock(string method)
    {
        isDeadlockResolved = true;
        statusText.text = "Status: SAFE\n" + method;
        statusText.color = Color.green;

        // Disable recovery buttons
        towButton.interactable = false;
        backOutButton.interactable = false;
    }
}