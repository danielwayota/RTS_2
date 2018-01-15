using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	private NavMeshAgent agent;

	// PROTOTYPE
	public Camera sceneCamera;
	private Plane groundPlane;
	// END PROTOTYPE

	// ================================
	void Start()
	{
		this.agent = GetComponent<NavMeshAgent>();

		this.groundPlane.SetNormalAndPosition(Vector3.up, Vector3.zero);
	}
	
	// ================================
	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			// Calcular posicion de destino
			Ray ray = sceneCamera.ScreenPointToRay(Input.mousePosition);
			float distance;

			groundPlane.Raycast(ray, out distance);
			Vector3 point = ray.GetPoint(distance);

			// Mover el agente
			this.agent.SetDestination(point);
		}
	}
}
