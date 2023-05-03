using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDenemeForce : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ýlerleme vectörünün yönünü objeye göre almak istediðinde kullanman gerekenler

        //transform.Translate(transform.forward*Time.deltaTime); //transform.forward bir vectordur (0,0,1)
        //transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        //rb.AddRelativeForce(new Vector3(0, 0, 1));
        //rb.velocity = transform.forward;

        //ilerleme vectörünü yönünü world'e göre almak istediðinde kullanman gerekenler

        //rb.angularVelocity = new Vector3(0, 0, 1);
        //rb.velocity = new Vector3(0, 0, 1); //Burada yön cisimden baðýmsýzdýr. Sahnedeki x,y,z temellerine göre cisme hep ayný doðrultuda kuvvet uygulanýr cismin ne tarafa baktýðý farketmez.
        //rb.AddForce(new Vector3(0, 0, 0.1f));
        //transform.position += new Vector3(0, 0, 1f);
    }
}
