using UnityEngine;
using System.Collections;

public class BeiSaiEr : MonoBehaviour
{

    private Transform a, b, c, a1, b1,c1;

    private void Start()
    {
        a = transform.GetChild(0);
        b = transform.GetChild(1);
        c = transform.GetChild(2);
        a1 = transform.GetChild(3);
        b1 = transform.GetChild(4);
        c1 = transform.GetChild(5);
    }


    private void FixedUpdate()
    {
        Debug.DrawLine(a.position, b.position);
        Debug.DrawLine(b.position, c.position);
        Debug.DrawLine(c.position, a.position);
        Debug.DrawLine(a1.position, b1.position, Color.red);
    }

    float passTime = 0.00f;
	float bbTime = 0.00f;

    float useTime = 3.00f;

    bool isBool = false;


    void Update()
    {
		 Move();
        if (isBool == true)
        {
            passTime += Time.deltaTime;
		
            float baifenbi = passTime / useTime;
			
            a1.position = Vector3.Lerp(a.position, b.position, baifenbi);
            b1.position = Vector3.Lerp(b.position, c.position, baifenbi);
            Vector3 dis = a1.position - b1.position;
            Vector3 point = new Vector3(dis.x * (1.0f - baifenbi), dis.y * (1.0f - baifenbi), dis.z * (1.0f - baifenbi));
		
			//Vector3 point = new Vector3(dis.x , dis.y , dis.z );
            Vector3 newPoint = point + b1.position;

            c1.position = newPoint;

            if (passTime >= useTime)
            {
                passTime = 0;
                isBool = false;
            }
        }
    }


    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isBool = true;
        }
    }
}