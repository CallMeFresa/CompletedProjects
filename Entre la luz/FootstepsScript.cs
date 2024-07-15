using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    public AudioSource Footsteps_Walking;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            Footsteps_Walking.enabled = true;
        }
        else
        {
            Footsteps_Walking.enabled = false;
        }
    }
}
