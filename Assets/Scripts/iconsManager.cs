using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class iconsManager : MonoBehaviour
{
    public icons_SO[] zapposIcons;
    public Vector3 scale;
    private TapGesture gesture;
    private PressGesture pressGesture;
    public int currentIcon;
    [SerializeField] private GameObject iconsPrefab;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    public int maxIcons;
    [SerializeField] private int maxLife;
    public float numCurrentIcons;
    public GameObject backgroundManager;
    public backgroundManager bm;
      
    private void OnEnable()
    {
        pressGesture = GetComponent<PressGesture>();
        pressGesture.Pressed += pressedHandler;
    }

    private void OnDisable()
    {
        pressGesture.Pressed -= pressedHandler;
    }

    void Start()
    {
        bm = backgroundManager.GetComponent<backgroundManager>();

        currentIcon = 0;
            
        for (int i = 0; i < zapposIcons.Length; i++)
        {
            iconsPrefab.GetComponent<iconsDisplay>().icon = zapposIcons[i];
            Instantiate(iconsPrefab);
            iconsPrefab.transform.localScale = scale;
            StartCoroutine(IconDestruction(currentIcon));
            currentIcon++;
            
            if (currentIcon >= zapposIcons.Length)
            {
                currentIcon = 0;
                
            }
            
        }

        foreach(GameObject _iconPrefab in GameObject.FindGameObjectsWithTag("Icon"))
        {
            prefabs.Add(_iconPrefab);
        }

        CountActive();

        StartCoroutine(bm.calculateSpeed());
    }

    private void Update()
    {
        
    }

    private void CountActive()
    {
        numCurrentIcons = 0;
        
        for (int i = 0; i < prefabs.Count; i++)
        {
            if(prefabs[i].activeInHierarchy == true)
            {
                numCurrentIcons++;                
            }
        }        

        Debug.Log("number of active objects " + numCurrentIcons);
    }

    private void pressedHandler(object sender, System.EventArgs e)
    {
        CountActive();

        if (numCurrentIcons < maxIcons)
        {
            if (currentIcon == zapposIcons.Length - 1)
            {
                currentIcon = 0;
            }
            prefabs[currentIcon].SetActive(true);
            StartCoroutine(IconDestruction(currentIcon));
            StickyShot();
            SpawnCloud();
        }

        StartCoroutine(bm.calculateSpeed());


    }

    private void StickyShot()
    {
        var ray = Camera.main.ScreenPointToRay(pressGesture.ScreenPosition);
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

    private void SpawnCloud()
    {
        //instantiate cloud on transform.postion
    }
         
    IEnumerator IconDestruction(int currentIcon)
    {
        yield return new WaitForSeconds(maxLife);
        prefabs[currentIcon].SetActive(false);
               
    }

    

}
