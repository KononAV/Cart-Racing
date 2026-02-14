using UnityEngine;

public class Finish : MonoBehaviour
{
    private BoxCollider _finishStopper;
    [SerializeField]private BoxCollider finishOpener;
    void Start()
    {
        _finishStopper = gameObject.transform.Find("FinishStopper").GetComponent<BoxCollider>();
        //finishOpener = GameObject.Find("FinishOpener").GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Kart>())
        {
            Debug.Log("Finish collision");
            _finishStopper.enabled = true;
            finishOpener.enabled = true;
        }
    }
}
