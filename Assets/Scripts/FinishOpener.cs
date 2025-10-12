using UnityEngine;

public class FinishOpener : MonoBehaviour
{
    [SerializeField] private BoxCollider finishStopper;
    private BoxCollider _finishOpener;
    void Start()
    {
        _finishOpener = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Opener enter");
        _finishOpener.enabled = false;
        finishStopper.enabled = false;
    }
}
