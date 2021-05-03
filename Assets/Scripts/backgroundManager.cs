using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundManager : MonoBehaviour
{

    [SerializeField] private GameObject iconsManager;
    private iconsManager im;
    [SerializeField] private GameObject blue;
    [SerializeField] private GameObject green;
    [SerializeField] private GameObject orange;
    [SerializeField] private GameObject purple;
    [SerializeField] private GameObject teal;
    [Range(0, 4)]
    public float speed = 0.1f;
    public float previousIcons;
    public float duration;
    public float newIconCount;
    
   
    void Start()
    {
        im = iconsManager.GetComponent<iconsManager>();
        
    }

    void Update()
    {
        previousIcons = im.numCurrentIcons - 1;
        blue.GetComponent<WavySprite>().textureSpeed = speed * newIconCount;
        green.GetComponent<WavySprite>().textureSpeed = speed * newIconCount;
        orange.GetComponent<WavySprite>().textureSpeed = speed * newIconCount;
        purple.GetComponent<WavySprite>().textureSpeed = speed * newIconCount;
        teal.GetComponent<WavySprite>().textureSpeed = speed * newIconCount;
    }

    public IEnumerator calculateSpeed()
    {
        Debug.Log("Calculated Speed");
        var t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            newIconCount = Mathf.Lerp(previousIcons, im.numCurrentIcons, t / duration);
            yield return null;
        }
    }
}
