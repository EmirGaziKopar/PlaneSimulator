using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject WorldPosition; //Burada D�n�� de�erlerinden etkilenmemesi i�in hiyerar�inin tepesinde bir worldPos gameObjesi kullanaca��z

    public float horizontal;
    public float horizontalSpeed;


    public float pitchSpeed;
    public float rollSpeed;
    public float maxPitchAngle;
    public float maxRollAngle;
    public float roll;

    public float vertical;

    public float yawSpeed;
    public float rotateSpeed;
    public float pitch;
    public float yaw;

    public bool state1;

    int counter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Bir method veya kod yard�m�yla wayPointlere do�ru u�a�� lerp �ekilde y�nlendir. Bunu yaparken yukar� a�a�� ve sa�a sola gitmede couroutine kullan 
        //Spawn pointlere y�nlendirirken local pos 
        //Oyuncu A veya D tu�lar�na bast���nda iki tu�taki s�relerin i�ine bir bool koy ve biri bitmedi�inde di�eri ba�lamas�n. Yani s�re tamamen bitince d�n�� sa�lans�n.

        pitch += vertical * pitchSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y + horizontal * rotateSpeed * Time.deltaTime, -yaw * yawSpeed * Time.deltaTime);
        

        roll = Mathf.Lerp(roll, -horizontal * maxRollAngle, Time.deltaTime * rollSpeed);
        transform.rotation *= Quaternion.Euler(0f, 0f, roll);

        if (Input.GetKeyDown(KeyCode.D))
        {
            //counter++;
            StartCoroutine(PushHorizontalDButtonDown()); //A tu�una bas�ld���nda bu tetiklenece ve horizontal'e yumu�ak bir ge�i� sa�lanacak
            //transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y + horizontal * rotateSpeed * Time.deltaTime, -yaw * yawSpeed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(PushHorizontalAButtonDown());
        }
        /*
        if (Input.GetKeyUp(KeyCode.D) && counter>0)
        {

            counter--;
            StartCoroutine(PushHorizontalDButtonUp()); //A tu�una bas�ld���nda bu tetiklenece ve horizontal'e yumu�ak bir ge�i� sa�lanacak
            //transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y + horizontal * rotateSpeed * Time.deltaTime, -yaw * yawSpeed * Time.deltaTime);
        }
        */
    }


    private IEnumerator PlaneArrived() //Plane hedefe vard���nda horizontal yani yataydaki d�n���n� tekrar s�f�ra �eken kod 
    {
        if (horizontal > 0f)
        {
            state1 = true; //E�er state1 true ise horizontalde pozitife do�ru bir d�n�� ger�ekle�mi�tir.
            while (horizontal > 0)
            {
                horizontal -= Time.deltaTime;
                Debug.Log("Tamamlandi : " + horizontal);
                yield return null;
            }
            horizontal = 0; //Genelde tam s�f�rda durmaz o nedenle i�imizi sa�lama al�yoruz.
        }

        if (horizontal < 0) //state1 s�ras�nda u�a��n pozisyonu tekrar 0'a �ekilecek bu nedenler
        {
            while (horizontal > 0)
            {
                horizontal += Time.deltaTime;
                Debug.Log("Tamamlandi : " + horizontal);
                yield return null;
            }
            horizontal = 0; 
        }
        
        
        //Player�n parma�� D tu�una bas�l� kald��� s�rece de�eri 1'de kalacak ��nk� de�eri de�i�tiren aksiyon up'da �al���yor.
    }

    private IEnumerator PushHorizontalDButtonDown()
    {
        while (horizontal<1f)
        {           
            horizontal += horizontalSpeed * Time.deltaTime;       
            yield return null;
        }
        horizontal = 1;
        Debug.Log("Tamamlandi : " + horizontal);
        //Player�n parma�� D tu�una bas�l� kald��� s�rece de�eri 1'de kalacak ��nk� de�eri de�i�tiren aksiyon up'da �al���yor.
    }

    private IEnumerator PushHorizontalAButtonDown()
    {
        while (horizontal > -1f)
        {
            Debug.Log(horizontal);
            horizontal -= horizontalSpeed * Time.deltaTime;
            Debug.Log("Sifir : " + horizontal);
            yield return null;
        }
        horizontal = -1;
        Debug.Log("Tamamlandi : " + horizontal);
        
        //horizontal = 0; //Tekrar bas�ld���nda tekrar �al��mas� i�in 
        //Player parma��n� �ekmedi�i s�rece de�er 1'den azalmaya ba�lamaz ne zaman ki elini �eker o zaman de�er de d��meye ba�lar ta ki 0'a kadar 
    }
}
