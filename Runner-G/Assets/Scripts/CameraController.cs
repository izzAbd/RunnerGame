using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _MainCamera;
    [SerializeField] CinemachineVirtualCamera _EndCamera;
    Animator _FinishAnim;

    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SwitchCamera();
            Debug.Log("finished");
            _FinishAnim.SetBool("IsFinished", true);
        }
    }
    public void SwitchCamera()
    { 
            _MainCamera.Priority = 10;
            _EndCamera.Priority = 11;   
    }
}
