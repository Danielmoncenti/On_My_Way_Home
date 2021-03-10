using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoloController : MonoBehaviour
{
    public GameObject Rock;
    public GameObject Spikey;

    private Rigidbody2D rbMole;

    private float shootTime = 0;
    private bool shooted = false;

    // Start is called before the first frame update
    void Start() {
        rbMole = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //StartCoroutine(SimulateProjectile());
        float delta = Time.deltaTime * 1000;
        shootTime += delta;
        if (shootTime > 5000) {
            shootTime = 0;
            GameObject dani = Instantiate(Rock, transform.position, transform.rotation);
        }
    }
    void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Puas")
        {
            Destroy(gameObject);
        }
    }
    //IEnumerator SimulateProjectile() {

    //    // Delay de cada tiro.
    //    yield return new WaitForSeconds(10);

    //    // Mover la Piedra al Topo.
    //    RockPosition.position = MolePosition.position;

    //    // Guardar distancia hasta Erizo.
    //    float target_Distance = Vector3.Distance(RockPosition.position, TargetPosition.position);

    //    // Calculo de velocidad segun el angulo lanzado.
    //    float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

    //    // Coger la Vx - Vy de la Velocidad total.
    //    float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
    //    float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

    //    // Calcular el tiempo de vuelo
    //    float flightDuration = target_Distance / Vx;

    //    float elapse_time = 0;

    //    while (elapse_time < flightDuration)
    //    {
    //        RockPosition.Translate(Vx * Time.deltaTime, (Vy - (gravity * elapse_time)) * Time.deltaTime, 0);

    //        elapse_time += Time.deltaTime;

    //        yield return null;
    //    }
    //}
}
