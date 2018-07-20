using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour {

    public int tx, ty;
    public int nx, ny;
    public int speed = 70;

    private float lx, lz;

	// Use this for initialization
	void Start ()
    {
        lx = transform.localPosition.x;
        lz = transform.localPosition.z;
    }

    public void SetXY(int x,int y)
    {
        nx = x;
        tx = x;
        ny = y;
        ty = y;

        transform.position = new Vector3(nx * 10 + 5, 5, ny * 10 + 5);
        lx = transform.localPosition.x;
        lz = transform.localPosition.z;
    }
    public void SetTXTY(int x, int y)
    {
        tx = x;
        ty = y;
    }

    // Update is called once per frame
    void Update () {
        if (ny < ty)
        {
            transform.localRotation = new Quaternion(0, 0, -90, 0);
            if((transform.localPosition.z+ speed * Time.deltaTime) - lz < 10) transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
            else transform.position = new Vector3(transform.localPosition.x, 5, ny * 10 + 5+10);
            if (transform.localPosition.z -lz>= 10)
            {
                ny++;
                lz = transform.localPosition.z;
            }
        }
        else if (ny > ty)
        {
            transform.localRotation = new Quaternion(0, 0, 90, 0);
            if (lz - (transform.localPosition.z- speed * Time.deltaTime) < 10) transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
            else transform.position = new Vector3(transform.localPosition.x, 5, ny * 10 + 5 - 10);
            if (lz-transform.localPosition.z >= 10)
            {
                ny--;
                lz = transform.localPosition.z;
            }
        }
        else
        {
            transform.position = new Vector3(transform.localPosition.x, 5, ny * 10 + 5);
        }

        if (nx < tx)
        {
            transform.localRotation = new Quaternion(90, 0, 0, 0);
            if ((transform.localPosition.x+ speed * Time.deltaTime) - lx < 10) transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            else transform.position = new Vector3(nx * 10 + 5+10, 5, transform.localPosition.z);
            if (transform.localPosition.x -lx>= 10)
            {
                nx++;
                lx = transform.localPosition.x;
            }
        }
        else if (nx > tx)
        {
            transform.localRotation = new Quaternion(-90, 0, 0, 0);
            if (lx - (transform.localPosition.x - speed * Time.deltaTime) < 10) transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            else transform.position = new Vector3(nx * 10 + 5 - 10, 5, transform.localPosition.z);
            if (lx-transform.localPosition.x >= 10)
            {
                nx--;
                lx = transform.localPosition.x;
            }
        }
        else
        {
            transform.position = new Vector3(nx * 10 + 5, 5, transform.localPosition.z);
        }
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow)&&ty<29)
        {
            ty++;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)&&ty>0)
        {
            ty--;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)&&tx<29)
        {
            tx++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)&&tx>0)
        {
            tx--;
        }*/
    }
}
