using System.Collections;
using UnityEngine;

public class StandartRay : MonoBehaviour
{
    internal Camera cam;
    [SerializeField] GameObject zeroNote;
    [SerializeField] GameObject note;
    internal RaycastHit hit;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, 5))
        {
            if (hit.collider.gameObject.tag == "0")
            {
                StartCoroutine(Disappear());
            }
        }
    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.5f);
        hit.collider.gameObject.SetActive(false);
        zeroNote.SetActive(false);
        note.SetActive(true);
    }
}
