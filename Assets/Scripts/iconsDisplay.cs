using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconsDisplay : MonoBehaviour
{
    public icons_SO icon;    
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
       
    void Start()
    {        
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        
        

        sr.sprite = icon.artwork;
        rb.sharedMaterial = icon.physicsMaterial;
        cc.radius = icon.radius;

        
    }

} 
