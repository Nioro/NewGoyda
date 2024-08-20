using UnityEngine;

public class NoteRay : MonoBehaviour
{
    [SerializeField] Camera cam;
    private RaycastHit hit;
    private RaycastHit previousHit;
    [SerializeField] private GameObject zeroObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawRay(cam.transform.position, cam.transform.forward*25f, Color.red);
        try
        {  
            if (Physics.Raycast(ray, out hit, 3))
            {
                Outline outline = hit.collider.gameObject.GetComponent<Outline>();
                Outline previousOutline = previousHit.collider.gameObject.GetComponent<Outline>();
                if (outline && hit.collider.gameObject.Equals(previousHit.collider.gameObject) && hit.collider.gameObject.tag == "note")
                {
                    outline.OutlineWidth = 2;
                    Hint.SetHint(hit.collider.gameObject.GetComponent<Note>().text);
                    Hint.hint.fontSize = hit.collider.gameObject.GetComponent<Note>().fontSize;
                    if (hit.collider.gameObject.GetComponent<Note>().getOut)
                    {
                        zeroObject.SetActive(true);
                    }
                }
                if (!hit.collider.gameObject.Equals(previousHit.collider.gameObject) || hit.distance > 2.9f)
                {     
                    previousOutline.OutlineWidth = 0;
                    Hint.EraseHint();
                }
                previousHit = hit;
            }        
        }
        catch 
        {
            if (Physics.Raycast(ray, out previousHit, 25))
            {
                return;
            }
        }  
    }
}
