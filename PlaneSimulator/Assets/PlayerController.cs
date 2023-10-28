using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject WorldPosition; //Burada Dönüþ deðerlerinden etkilenmemesi için hiyerarþinin tepesinde bir worldPos gameObjesi kullanacaðýz

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

        //Bir method veya kod yardýmýyla wayPointlere doðru uçaðý lerp þekilde yönlendir. Bunu yaparken yukarý aþaðý ve saða sola gitmede couroutine kullan 
        //Spawn pointlere yönlendirirken local pos 
        //Oyuncu A veya D tuþlarýna bastýðýnda iki tuþtaki sürelerin içine bir bool koy ve biri bitmediðinde diðeri baþlamasýn. Yani süre tamamen bitince dönüþ saðlansýn.

        pitch += vertical * pitchSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, -maxPitchAngle, maxPitchAngle);
        transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y + horizontal * rotateSpeed * Time.deltaTime, -yaw * yawSpeed * Time.deltaTime);
        

        roll = Mathf.Lerp(roll, -horizontal * maxRollAngle, Time.deltaTime * rollSpeed);
        transform.rotation *= Quaternion.Euler(0f, 0f, roll);

        if (Input.GetKeyDown(KeyCode.D))
        {
            //counter++;
            StartCoroutine(PushHorizontalDButtonDown()); //A tuþuna basýldýðýnda bu tetiklenece ve horizontal'e yumuþak bir geçiþ saðlanacak
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
            StartCoroutine(PushHorizontalDButtonUp()); //A tuþuna basýldýðýnda bu tetiklenece ve horizontal'e yumuþak bir geçiþ saðlanacak
            //transform.rotation = Quaternion.Euler(pitch, transform.rotation.eulerAngles.y + horizontal * rotateSpeed * Time.deltaTime, -yaw * yawSpeed * Time.deltaTime);
        }
        */
    }


    private IEnumerator PlaneArrived() //Plane hedefe vardýðýnda horizontal yani yataydaki dönüþünü tekrar sýfýra çeken kod 
    {
        if (horizontal > 0f)
        {
            state1 = true; //Eðer state1 true ise horizontalde pozitife doðru bir dönüþ gerçekleþmiþtir.
            while (horizontal > 0)
            {
                horizontal -= Time.deltaTime;
                Debug.Log("Tamamlandi : " + horizontal);
                yield return null;
            }
            horizontal = 0; //Genelde tam sýfýrda durmaz o nedenle iþimizi saðlama alýyoruz.
        }

        if (horizontal < 0) //state1 sýrasýnda uçaðýn pozisyonu tekrar 0'a çekilecek bu nedenler
        {
            while (horizontal > 0)
            {
                horizontal += Time.deltaTime;
                Debug.Log("Tamamlandi : " + horizontal);
                yield return null;
            }
            horizontal = 0; 
        }
        
        
        //Playerýn parmaðý D tuþuna basýlý kaldýðý sürece deðeri 1'de kalacak çünkü deðeri deðiþtiren aksiyon up'da çalýþýyor.
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
        //Playerýn parmaðý D tuþuna basýlý kaldýðý sürece deðeri 1'de kalacak çünkü deðeri deðiþtiren aksiyon up'da çalýþýyor.
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
        
        //horizontal = 0; //Tekrar basýldýðýnda tekrar çalýþmasý için 
        //Player parmaðýný çekmediði sürece deðer 1'den azalmaya baþlamaz ne zaman ki elini çeker o zaman deðer de düþmeye baþlar ta ki 0'a kadar 
    }
}
