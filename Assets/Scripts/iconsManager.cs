using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconsManager : MonoBehaviour
{

    public GameObject[] zapposIcons;
    public Vector3 size;
         


    // Start is called before the first frame update
    void Start()
    {
        ReInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReInit()
    {
        for (int i = 0; i < zapposIcons.Length; i++)
        {
            zapposIcons[i].gameObject.transform.localScale = size;
            zapposIcons[i] = Instantiate(zapposIcons[i]) as GameObject;
        }
    }
}
