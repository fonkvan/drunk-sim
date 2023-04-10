using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public GameObject ragdollPlayer;


    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = ragdollPlayer.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ragdollPlayer.transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, ragdollPlayer.transform.rotation, Time.deltaTime * 5f);
    }
}
