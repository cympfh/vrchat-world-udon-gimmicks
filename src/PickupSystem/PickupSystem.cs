using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupSystem : UdonSharpBehaviour
{
    private int triggerPressCount = 0;
    private float lastTriggerPressTime = 0f;
    private const float doublePressThreshold = 0.3f;
    private bool pressing = false;

    public GameObject pickupItem;
    public UdonBehaviour hook_udon;
    public string hook_name;
    public float item_distance = 0.45f;  // < 0.5f

    void Start()
    {
    }

    void Update()
    {
        // Desktop: E key
        if (Input.GetKeyDown(KeyCode.E))
        {
            MoveItemToFront();
        }

        // VR: Trigger buttons
        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") > 0.35f)
        {
            HandleTriggerPress();
        }

        if (Input.GetAxis("Oculus_CrossPlatform_PrimaryIndexTrigger") < 0.3f)
        {
            HandleTriggerUnPress();
        }
    }

    void HandleTriggerPress()
    {
        float currentTime = Time.time;
        // continuous pressing to be ignored
        if (pressing)
        {
            return;
        }
        pressing = true;
        if (currentTime - lastTriggerPressTime <= doublePressThreshold)
        {
            triggerPressCount++;
        }
        else
        {
            triggerPressCount = 1;
        }
        lastTriggerPressTime = currentTime;
        // 3 triggers
        if (triggerPressCount >= 3)
        {
            MoveItemToFront();
            triggerPressCount = 0;
        }
    }

    void HandleTriggerUnPress()
    {
        pressing = false;
    }

    void MoveItemToFront()
    {
        VRCPlayerApi player = Networking.LocalPlayer;
        if (player == null) { return; }
        Vector3 position = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).position;
        Vector3 forwardDirection = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation * Vector3.forward;
        Vector3 upDirection = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head).rotation * Vector3.up;

        pickupItem.transform.position = position + forwardDirection * item_distance;
        pickupItem.transform.rotation = Quaternion.LookRotation(forwardDirection, upDirection);

        if (hook_udon != null)
        {
            hook_udon.SendCustomEvent(hook_name);
        }
    }
}
