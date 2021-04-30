using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class iconsManager : MonoBehaviour
{

    public GameObject[] zapposIcons;
    public Vector3 scale;
    private TapGesture gesture;
    private int currentIcon;
    
    
               
    private void OnEnable()
    {
        gesture = GetComponent<TapGesture>();
        gesture.Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        gesture.Tapped -= tappedHandler;
    }

    void Start()
    {      

        for (int i = 0; i < zapposIcons.Length; i++)
        {
            zapposIcons[i] = Instantiate(zapposIcons[i]) as GameObject;
            zapposIcons[i].gameObject.transform.localScale = scale;
            zapposIcons[i].SetActive(true);
        }

        
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
                
        var ray = Camera.main.ScreenPointToRay(gesture.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {

            zapposIcons[currentIcon].transform.position = hit.point;

            Vector2 direction = new Vector2((float)Random.Range(-10f, 10f), (float)Random.Range(-10f, 10f));
            var rb = zapposIcons[currentIcon].GetComponent<Rigidbody2D>();
            rb.AddForce(direction, ForceMode2D.Impulse);
            rb.AddTorque(2, ForceMode2D.Impulse);

            currentIcon++;

            if (currentIcon >= zapposIcons.Length)
            {
                currentIcon = 0;
                Debug.Log("reset to 0");
            }

            

            
        }

        
        
    }

    
    
    void Update()
    {
        
    }

    void ReInit()
    {
        
    }
     
    IEnumerator IconDestruction()
    {
        yield return new WaitForSeconds(5);
        for (int i=0; i<zapposIcons.Length; i++)
        {
            
        }
    }
}
