using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_IngredientSound : MonoBehaviour
{
    // Start is called before the first frame update
    //public List<IngredientType> stackedIngredients = new List<IngredientType>();

    public AudioClip Put_Sound;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
    }
    public void playSound()
    {
        audioSource.PlayOneShot(Put_Sound);
    }
}
