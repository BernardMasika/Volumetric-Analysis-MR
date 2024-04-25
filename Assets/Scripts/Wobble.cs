using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Wobble : MonoBehaviour
{
    Renderer rend;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;  
    Vector3 angularVelocity;
    public float MaxWobble = 0.03f;
    public float WobbleSpeed = 1f;
    public float Recovery = 1f;
    float wobbleAmountX;
    float wobbleAmountZ;
    float wobbleAmountToAddX;
    float wobbleAmountToAddZ;
    float pulse;
    float time = 0.5f;
    float initialFillValue;
    bool triggerValue;
    private InputDevice m_InputDevices;
    List<InputDevice> inputDevicesList = new List<InputDevice>();
    private bool isGrabbed = false;

    // Use this for initialization
    void Start()
    {
        InitialiseINputReader();
        rend = GetComponent<Renderer>();
        
        
    }
    private void Update()
    {
        initialFillValue = rend.material.GetFloat("_Fill");

        time += Time.deltaTime;
        // decrease wobble over time
        wobbleAmountToAddX = Mathf.Lerp(wobbleAmountToAddX, 0, Time.deltaTime * (Recovery));
        wobbleAmountToAddZ = Mathf.Lerp(wobbleAmountToAddZ, 0, Time.deltaTime * (Recovery));

        // make a sine wave of the decreasing wobble
        pulse = 2 * Mathf.PI * WobbleSpeed;
        wobbleAmountX = wobbleAmountToAddX * Mathf.Sin(pulse * time);
        wobbleAmountZ = wobbleAmountToAddZ * Mathf.Sin(pulse * time);

        // send it to the shader
        rend.material.SetFloat("_WobbleX", wobbleAmountX);
        rend.material.SetFloat("_WobbleZ", wobbleAmountZ);

        // velocity
        velocity = (lastPos - transform.position) / Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;


        // add clamped velocity to wobble
        wobbleAmountToAddX += Mathf.Clamp((velocity.x + (angularVelocity.z * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);
        wobbleAmountToAddZ += Mathf.Clamp((velocity.z + (angularVelocity.x * 0.2f)) * MaxWobble, -MaxWobble, MaxWobble);

        // keep last position
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;


        if (inputDevicesList.Count < 2)
        {
            InitialiseINputReader();
        }

        m_InputDevices.TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue);

        if (triggerValue && isGrabbed)
        {
            Debug.Log("suck button pressed..");
            float finalFillValue = initialFillValue + 0.005f;
            if (finalFillValue <= 0.028f)
            {
                rend.material.SetFloat("_Fill", finalFillValue);
            }
           
        }


        // fill the up the portion with pressed button
       // if (Input.GetKey(KeyCode.Space))
       // {
        //    Debug.Log("pressed..");
        //    float finalFillValue = initialFillValue + 0.01f;
        //    rend.material.SetFloat("_Fill", finalFillValue);
       // }
    }

    public void EnableGrabbed()
    {
       isGrabbed = true;
        
    }

  public  void DisableIsGrabbed()
    {
        isGrabbed = false;
    }

    void InitialiseINputReader()
    {

        InputDevices.GetDevices(inputDevicesList);
        InputDeviceCharacteristics rightControllerCharacterists = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacterists, inputDevicesList);

        if (inputDevicesList.Count > 0)
        {
            m_InputDevices = inputDevicesList[0];
        }


    }


}