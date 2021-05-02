using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures;

public class iconsManager : MonoBehaviour
{

    public icons_SO[] zapposIcons;
    public Vector3 scale;
    private TapGesture gesture;
    public int currentIcon;
    [SerializeField] private GameObject iconsPrefab;
    [SerializeField] private List<GameObject> prefabs = new List<GameObject>();
    public int maxIcons;
    [SerializeField] private int maxLife;
    public int numActiveIcons;
    public GameObject backgroundManager;
    public backgroundManager bm;
      
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
    }

    private void Update()
    {
        
    }

    private void CountActive()
    {
        numActiveIcons = 0;

        for (int i = 0; i < prefabs.Count; i++)
        {
            if(prefabs[i].activeInHierarchy == true)
            {
                numActiveIcons++;
            }
        }
        
        Debug.Log("number of active objects " + numActiveIcons);
    }

    private void tappedHandler(object sender, System.EventArgs e)
    {
        CountActive();

        if (numActiveIcons < maxIcons)
        {
            if (currentIcon == zapposIcons.Length - 1)
            {
                StartCoroutine(bm.ChangeSpeed(0.1f, 0.5f, 60f));
                currentIcon = 0;
            }
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
