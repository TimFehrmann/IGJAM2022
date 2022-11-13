using System;
using System.Collections;
using UnityEngine;

public class OnExplodePrefab : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    public Action<OnExplodePrefab> OnDestroy;

    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        animator = transform.GetComponent<Animator>();

        if (audioSource == null && animator == null)
        {
            Debug.LogError($"OnExplosionPrefab \"{gameObject.name}\" audioSource and animator null");
        }
    }
    
    public void Init()
    {
        if (animator != null)
        {
            animator.SetTrigger("restart");
        }
    }

    public void Play()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }

        if (animator != null)
        {
            animator.SetTrigger("play");
        }

        StartCoroutine(DestroyAfterTime());
    }

    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSecondsRealtime(2);
        if (OnDestroy != null)
        {
            OnDestroy(this);
        }
    }

   
}