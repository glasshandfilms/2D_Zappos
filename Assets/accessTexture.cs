using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accessTexture : MonoBehaviour
{
    public bool CanScroll;

    private Renderer _renderer;
    public float _speed = 0.1f; //current scrolling speed
    [SerializeField]private float _desiredSpeed; //desired scrolling speed
    [SerializeField]private float _pos; //total texture offset
    [SerializeField]private float _lastChange; //just for demonstration

    protected void Awake()
    {
        _renderer = GetComponent<Renderer>();
        CanScroll = true;

        _desiredSpeed = _speed;
        _lastChange = 0; //just for demonstration
    }

    protected void Update()
    {
        if (!CanScroll) return;

        //just for demonstration: change desired speed
        if (Time.time > _lastChange + 2)
        {
            _desiredSpeed = Random.Range(-2f, 2f);
            _lastChange = Time.time;
            Debug.Log("Set Speed To " + _desiredSpeed);
        }

        //this is where the magic happens: easing by hand!
        //Play with the value 0.01f to speed up or slow down the easing. 
        _speed += (_desiredSpeed - _speed) * 0.01f;

        //add current speed to the position
        _pos += _speed * Time.deltaTime;

        //for me it works without Mathf.Repeat
        //float y = Mathf.Repeat(_pos, 1);
        Vector2 offset = new Vector2(0, _pos);
        _renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
