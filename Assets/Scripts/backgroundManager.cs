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
    public float speed;
    [SerializeField] private float _numbertopick;

    


    void Start()
    {
        im = iconsManager.GetComponent<iconsManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        

       // _numbertopick = ((im.numActiveIcons+1) * speed);
        blue.GetComponent<WavySprite>().waveSpeed = speed;
        green.GetComponent<WavySprite>().waveSpeed = speed;
        orange.GetComponent<WavySprite>().waveSpeed = speed;
        purple.GetComponent<WavySprite>().waveSpeed = speed;
        teal.GetComponent<WavySprite>().waveSpeed = speed;
    }

    public IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            speed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        speed = v_end;
    }
}
