using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Prototype code: Note this is a monolithic class.
 * Purpose of this class is for showcase proposed ideas.
 */
public class Player : MonoBehaviour
{
    [SerializeField] private Camera camera;

    [SerializeField] private float speed;
    [SerializeField] private float groundOffset;

    [SerializeField] private GameObject ePillar;

    [Header("Debug Movement")] 
    [SerializeField] private Vector3 moveToward;
    [SerializeField] private Vector3 destination;

    private LineRenderer lineRenderer;
    private LineRenderer lineRendererTwo;

    [Header("Debug Abilities")]
    [SerializeField] private bool connectE;
    [SerializeField] private int eCount;
    [SerializeField] private Vector3 startE;
    [SerializeField] private Vector3 endE;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        
        lineRendererTwo = new GameObject("LineRenderer holder").AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRendererTwo.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            speed += 2;
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            {
                destination = hit.point;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                lineRendererTwo.SetPosition(0, transform.position);
                var offset = hit.point + Vector3.up * groundOffset;
                lineRendererTwo.SetPosition(1, offset);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit))
            { 
                Vector3 offset = Vector3.up * groundOffset/2;
                Instantiate(ePillar, hit.point + offset, Quaternion.identity);

                if (eCount == 0)
                {
                    startE = hit.point;
                }
                else if (eCount == 1)
                {
                    endE = hit.point;
                    connectE = true;
                }

                eCount++;
                eCount %= 2;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 offset = Vector3.up * groundOffset;
        moveToward = Vector3.MoveTowards(transform.position, destination + offset, speed * Time.deltaTime);
        transform.position = moveToward;

        if (connectE)
        {
            // Draw line here from startE to endE
            lineRenderer.SetPosition(0, startE + 0.5f * offset);
            lineRenderer.SetPosition(1, endE + 0.5f * offset);
            
            connectE = false;
        }
    }
}