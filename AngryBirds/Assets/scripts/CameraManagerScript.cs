using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManagerScript : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera idleCamera;
    [SerializeField] private CinemachineVirtualCamera followCamera;
    void Awake()
    {
        SwitchToIdleCamera();
    }
    public void SwitchToIdleCamera()
    {
        idleCamera.enabled = true;
        followCamera.enabled = false;
    }
    public void SwitchTofollowCamera(Transform followTrans)
    {
        followCamera.Follow = followTrans;
        idleCamera.enabled = false;
        followCamera.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
