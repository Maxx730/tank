using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public float RotationSpeed, MovementSpeed, MaxSpeed, TurretSpeed;
    public Text VelocityDebug, TurretRotationDebug, MousePositionDebug;
    public GameObject LoadedAmmo;

    private Rigidbody Body;
    private GameObject Turret, ProjectileSpawn, ShotParticle;

    // Start is called before the first frame update
    void Start()
    {
        Body = transform.GetComponent<Rigidbody>();
        Turret = GameObject.Find("PlayerTurret");
        ProjectileSpawn = GameObject.Find("ProjectileSpawnPoint");
        ShotParticle = GameObject.Find("TurretShot");
    }

    // Update is called once per frame
    void Update()
    {
        VelocityDebug.text = "Velocity: " + Body.velocity.magnitude;
        TurretRotationDebug.text = "Turret Rotation: " + Turret.transform.rotation;
    }

    private void FixedUpdate()
    {
        //Make sure the unit does not go any faster than a set speed.
        if(Body.velocity.magnitude < MaxSpeed)
        {
            //Move forwards and backwards.
            if (Input.GetKey(KeyCode.W))
            {
                Body.AddForce(transform.forward * MovementSpeed);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Body.AddForce(-transform.forward * MovementSpeed);
            }
        }

        //Rotate Right
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
        }
        //Rotate Left
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * RotationSpeed * Time.deltaTime);
        }

        //Rotate the turret to always point at the mouse pointer on the screen.
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mouseHit;

        if (Physics.Raycast(mouseRay, out mouseHit))
        {
            //Now that we have determined where the cursor is relative to the game world,
            //Rotate the turret at the point.
            Vector3 targetDir = mouseHit.point - Turret.transform.position;
            targetDir.y = 0;
            Vector3 newDir = Vector3.RotateTowards(Turret.transform.forward, targetDir, 10 * Time.deltaTime, 0.0f);

            Turret.transform.rotation = Quaternion.LookRotation(newDir);

            //Fire the turret by instantiating the ammo gameobject and adding 
            //a large force to it.
            if (Input.GetMouseButtonDown(0))
            {
                ShotParticle.GetComponent<ParticleSystem>().Play();
                GameObject bullet = Instantiate(LoadedAmmo, ProjectileSpawn.transform.position, Turret.transform.rotation);
                bullet.GetComponent<Rigidbody>().AddForce(Turret.transform.forward * 1320);
            }
        }
    }
}
