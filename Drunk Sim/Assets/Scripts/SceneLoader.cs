using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneToLoad;
    public Animator animator;
    public Transform transformToReturnTo;
    private bool canChange = false;
    private bool fading = false;
    private float animStartTime = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canChange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canChange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canChange && Input.GetKeyDown(KeyCode.Space))
        {
            canChange = false;
            GameManager.Instance.SetSpawnTransform(transformToReturnTo);
            FadeToLevel();
        }

        if (fading)
        {
            if (Time.time - animStartTime >= 1f)
            {
                OnFadeComplete();
            }
        }
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
        animStartTime = Time.time;
        fading = true;
    }

    public void OnFadeComplete()
    {
        GameManager.Instance.displayedInitialGoal = false;
        SceneManager.LoadScene(sceneToLoad);
    }
}
