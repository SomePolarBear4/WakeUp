using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FmodPlayer : MonoBehaviour
{
    public Transform groundCheck;
    private float distance = 0.1f;
    private float Material;
    private bool wasGrounded = true;
    //private bool isGrounded;
    RaycastHit2D hit;

    private float Height, oldHeight, Height_Difference;
    FMOD.Studio.EventInstance Footsteps;

    private void Start()
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footsteps");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MaterialCheck();
        Debug.DrawRay(transform.position, Vector3.down * distance, Color.blue);
        PlayerJump();
        PlayerLanded();
        wasGrounded = isGrounded();
        isGrounded();
       
        PlayerFallingCheck();
        
        
        
    }
    void MaterialCheck()
    {
        
        hit = Physics2D.Raycast(transform.position, Vector3.down, distance, 1 << 6);
        if (hit.collider)
         {
            
            if (hit.collider.CompareTag("Floor"))
            {
                Material = 0f;
            }
            else if (hit.collider.CompareTag("Gear"))
            {
                Material = 1f;
            }
            else Material = 0f;
         }
    }

    void PlayFootstepsEvent()
    {
       // FMOD.Studio.EventInstance Footsteps = FMODUnity.RuntimeManager.CreateInstance(path);
        Footsteps.setParameterByName("Material", Material);
        Footsteps.start();
        Footsteps.release();
    }
    bool isGrounded()
    {
       return hit = Physics2D.Raycast(groundCheck.position, Vector3.down, distance, 1 << 6);

    }
    void PlayerJump()
    {
        if (!isGrounded() && wasGrounded)
        {
            FMOD.Studio.EventInstance Jumping = FMODUnity.RuntimeManager.CreateInstance("event:/Jumping");
            Jumping.setParameterByName("Velocity", Height_Difference);
            Footsteps.setParameterByName("LowVolume", Height_Difference);
            Jumping.start();
            Jumping.release();
            Debug.Log(Height_Difference);

        }
    }
    void PlayerLanded()
    {
        if(isGrounded()&&!wasGrounded)
        {
           FMOD.Studio.EventInstance Landing = FMODUnity.RuntimeManager.CreateInstance("event:/Landing");
            Landing.setParameterByName("Velocity", Height_Difference);
            Footsteps.setParameterByName("LowVolume", Height_Difference);
            Landing.start();
            Landing.release();
            Debug.Log(Height_Difference);

        }
    }
    void PlayerFallingCheck()
    {
        oldHeight = Height;
        Height = transform.position.y;
        Height_Difference = Height - oldHeight;
        if(Height_Difference>0)
        {
            Height_Difference *= -1;
        }
    }
}
