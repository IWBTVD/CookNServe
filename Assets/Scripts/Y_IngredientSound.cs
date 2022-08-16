using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_IngredientSound : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<IngredientType> stackedIngredients = new List<IngredientType>();

    public AudioClip Put_Sound;
    private float timer;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void playSound()
    {
        if(timer <= 0)
        {
            audioSource.PlayOneShot(Put_Sound);
            timer = .5f;
        }

    }
}
