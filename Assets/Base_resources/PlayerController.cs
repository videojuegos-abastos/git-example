using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

	[SerializeField]
    Camera cam;

	[SerializeField]
    NavMeshAgent agent;

	[SerializeField]
	GameObject m_Pointer;

	bool _spawned = false;
	bool Spawned
	{
		get { return _spawned; }
		set { _spawned = _spawned || value; }
	}

	bool attachedNavMeshAgent => agent != null;

	void Start ()
	{
		
		if (agent == null)
		{
			agent = GetComponent<NavMeshAgent>();	
		}
		
	}

    // Update is called once per frame
    void Update () 
    {

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		bool collided = Physics.Raycast(ray, out hit);

		if (collided)
		{

			if (!Spawned)
			{
				m_Pointer = Instantiate(m_Pointer, hit.point, Quaternion.identity);
				Spawned = true;
			}

				m_Pointer.SetActive(true);
				m_Pointer.transform.position = hit.point;


			if (attachedNavMeshAgent && Input.GetMouseButtonDown(0))
			{
				agent.SetDestination(hit.point);
			}

		}
		else
		{
			m_Pointer.SetActive(false);
		}

    }
}