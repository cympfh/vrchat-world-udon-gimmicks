
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System.Collections.Generic;


public class FollowingSystem : UdonSharpBehaviour
{
    public GameObject target;
    public GameObject[] followers;
    public Vector3 offset;

    private bool enabled = false;
    private Vector3[] original_pos = new Vector3[4];
    private Quaternion[] original_rot = new Quaternion[4];

    void Start()
    {
        for (int i = 0; i < followers.Length; ++i)
        {
            original_pos[i] = followers[i].transform.position;
            original_rot[i] = followers[i].transform.rotation;
        }
    }

    void Update()
    {
        if (!enabled) return;
        foreach (GameObject f in followers)
        {
            f.transform.position = target.transform.position + target.transform.rotation * offset;
            f.transform.rotation = target.transform.rotation;
        }
    }

    public void Enable() { enabled = true; }
    public void DisableAndReturn()
    {
        enabled = false;
        for (int i = 0; i < followers.Length; ++i)
        {
            followers[i].transform.position = original_pos[i];
            followers[i].transform.rotation = original_rot[i];
        }
    }

}

