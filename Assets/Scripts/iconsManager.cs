using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class iconsManager : MonoBehaviour
{

    public icons_SO[] zapposIcons;
    public Vector3 scale;
    private TapGesture gesture;
    [SerializeField] private int currentIcon;
    [SerializeField] private GameObject iconsPrefab;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] private int maxIcons;
    [SerializeField] private int maxLife;
      
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
        currentIcon = 0;           
            
        for (int i = 0; i < zapposIcons.Length; i++)
        {
            iconsPrefab.GetComponent<iconsDisplay>().icon = zapposIcons[i];
            Instantiate(iconsPrefab);
            iconsPrefab.transform.localScale = scale;
            StartCoroutine(IconDestruction(currentIcon));
            currentIcon++;
            if(currentIcon >= zapposIcons.Length)
            {
                currentIcon = 0;
            }
            
        }

        foreach(GameObject _iconPrefab in GameObject.FindGameObjectsWithTag("Icon"))
        {
            prefabs.Add(_iconPrefab);
        }
    }

    private void Update()
    {
        
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        
        if (prefabs[currentIcon].active == false && currentIcon == zapposIcons.Length - 1)
        {
            Debug.Log("gimme a second");
            prefabs[currentIcon].SetActive(true);
            StartCoroutine(IconDestruction(currentIcon));
            currentIcon = 0;
            StickyShot();
        }

        if (prefabs[currentIcon].active == true && currentIcon == zapposIcons.Length - 1)
        {
            Debug.Log("we are all here!");
            prefabs[currentIcon].SetActive(false);
            currentIcon = 0;
        }

        if (prefabs[currentIcon].active == false && currentIcon < zapposIcons.Length)
        {
            Debug.Log("click");
            prefabs[currentIcon].SetActive(true);
            StartCoroutine(IconDestruction(currentIcon));
            StickyShot();
            
        }       
    }

    private void StickyShot()
    {
        var ray = Camera.main.ScreenPointToRay(gesture.ScreenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            prefabs[currentIcon].transform.position = hit.point;

            Vector2 direction = new Vector2((float)Random.Range(-10f, 10f), (float)Random.Range(-10f, 10f));

            var rb = prefabs[currentIcon].GetComponent<Rigidbody2D>();

            rb.AddForce(direction, ForceMode2D.Impulse);
            rb.AddTorque(2, ForceMode2D.Impulse);

            currentIcon++;
        }
    }
         
    IEnumerator IconDestruction(int currentIcon)
    {
        yield return new WaitForSeconds(maxLife);
        prefabs[currentIcon].SetActive(false);
        
    }
}
