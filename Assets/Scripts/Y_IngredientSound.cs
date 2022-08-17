using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_IngredientSound : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<IngredientType> stackedIngredients = new List<IngredientType>();

    private float timer;
    public AudioClip Put_Sound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void playSound()
    {
        audioSource.PlayOneShot(Put_Sound);
        if (timer <= 0)
        {
            audioSource.PlayOneShot(Put_Sound);
            timer = .5f;
        }

    }
}
