using UnityEngine;
using TouchScript.Gestures;

public class icons : MonoBehaviour
{
    [SerializeField] private GameObject iconsGM;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ParticleSystem ps;
    
    private TapGesture gesture;


    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        gesture = GetComponent<TapGesture>();
        gesture.Tapped += tappedHandler;
    }

    private void OnDisable()
    {
        gesture.Tapped -= tappedHandler;
    }


    void Start()
    {
        iconsGM = GameObject.Find("IconsManager");
    }

    
    private void tappedHandler(object sender, System.EventArgs e)
    {
        Vector2 direction = new Vector2((float)Random.Range(-10f, 10f), (float)Random.Range(-10f, 10f));
        rb.AddForce(direction, ForceMode2D.Impulse);
        rb.AddTorque(2, ForceMode2D.Impulse);           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Icon")
        {
            Instantiate(ps, transform.position, transform.rotation);
        }
    }

}
