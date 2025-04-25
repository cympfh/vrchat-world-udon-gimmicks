using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ReturnSystem : UdonSharpBehaviour
{
    private int triggerPressCount = 0;
    private float lastTriggerPressTime = 0f;
    private const float doublePressThreshold = 0.3f;
    private bool pressing = false;
    private Vector3 original_pos;
    private Quaternion original_rot;

    public GameObject returnItem;
    public UdonBehaviour hook_udon;
    public string hook_name;

    void Start()
    {
        original_pos = returnItem.transform.position;
        original_rot = returnItem.transform.rotation;
    }

    void Update()
    {
        // Desktop
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReturnItem();
        }

        // VR: Trigger buttons
        string name = "Oculus_CrossPlatform_SecondaryIndexTrigger";
        if (Input.GetAxis(name) > 0.35f)
        {
            HandleTriggerPress();
        }
        else if (Input.GetAxis(name) < 0.3f)
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
            ReturnItem();
            triggerPressCount = 0;
        }
    }

    void HandleTriggerUnPress()
    {
        pressing = false;
    }

    void ReturnItem()
    {
        returnItem.transform.position = original_pos;
        returnItem.transform.rotation = original_rot;

        if (hook_udon != null)
        {
            hook_udon.SendCustomEvent(hook_name);
        }
    }
}
