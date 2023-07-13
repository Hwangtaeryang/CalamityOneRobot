using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMove : MonoBehaviour
{
	public float speed = 1f;



	void Update()
	{



		if (Input.GetKey(KeyCode.D))
		{

			transform.Translate(Vector2.right * speed * Time.deltaTime);

		}

		if (Input.GetKey(KeyCode.A))
		{

			transform.Translate(-Vector2.right * speed * Time.deltaTime);

		}

		if (Input.GetKey(KeyCode.W))
		{

			transform.Translate(Vector2.up * speed * Time.deltaTime);

		}

		if (Input.GetKey(KeyCode.S))
		{

			transform.Translate(-Vector2.up * speed * Time.deltaTime);

		}

	}

}
