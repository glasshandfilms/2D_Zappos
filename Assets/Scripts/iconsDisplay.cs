using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class iconsDisplay : MonoBehaviour
{
    public icons_SO icon;    
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private CircleCollider2D cc;
    private Renderer m_Renderer;
    [SerializeField] private bool efb_bool;
    [SerializeField] private int efb;
    
       
    void Start()
    {
        
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        m_Renderer = GetComponent<Renderer>();

        efb_bool = icon.enableFlipbook;
        efb = Convert.ToInt32(efb_bool);
        SetFlipbookBool();

        sr.sprite = icon.spriteArtwork;
        rb.sharedMaterial = icon.physicsMaterial;
        cc.radius = icon.radius;

        m_Renderer.material.SetTexture("flipbook", icon.flipbook);
            
        m_Renderer.material.SetTexture("sprite", sr.sprite.texture);
                   
                
    }
    public void SetFlipbookBool()
    {
        m_Renderer.material.SetInt("enableFlipbook", efb);
        
    }
} 
