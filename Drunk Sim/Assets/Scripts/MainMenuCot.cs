using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCot : MonoBehaviour
{

    private Rigidbody hips;

    // Start is called before the first frame update
    void Start()
    {
        hips = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Random.Range(0, 100) < 10)
        {
            hips.AddForce(new Vector3(Random.Range(0, 1), Random.Range(0, 1), Random.Range(0, 1)) * Time.deltaTime);
        }
    }
}
