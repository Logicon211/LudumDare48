using UnityEngine;
using System.Collections;

public class camera_bear : MonoBehaviour {

    public Transform Bear;
    public Transform Target;
    public float zOffset = 0;
    public float Distance = 4.5f;
    public float ZoomStep = 1.0f;
    public float MoveSpeed = 5f;
    public float Pitch = 30f;
    public float yaw = 45f;
    public float TargetMoveSpeed = 3f;
    public float RotateSpeed = 60f;

    public Animator[] animator;

    private Vector3 TargetPos;
    private Vector3 LookPoint;
    private bool hasGun = false;

    void Start()
    {
        transform.position = GetPosition();
        LookPoint = Target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Animator a in animator)
                a.SetBool("gun_idle", !a.GetBool("gun_idle"));

            hasGun = !hasGun;
        }

        if (Input.GetKeyDown(KeyCode.X)) // WALK
        {
            foreach (Animator a in animator)
            {
                a.SetBool("n_run", false);
                a.SetBool("a_run", false);
                a.SetBool("n_walk", !a.GetBool("n_walk"));
                a.SetBool("a_walk", !a.GetBool("a_walk"));
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // RUN
        {
            foreach (Animator a in animator)
            {
                a.SetBool("n_walk", false);
                a.SetBool("a_walk", false);
                a.SetBool("n_run", !a.GetBool("n_run"));
                a.SetBool("a_run", !a.GetBool("a_run"));
            }
        }

        if (Input.GetKeyDown(KeyCode.Z)) // 
            foreach (Animator a in animator)
                a.gameObject.transform.position = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.G) && !hasGun) // 
            foreach (Animator a in animator)
                a.SetTrigger("growl");

        if (Input.GetKeyDown(KeyCode.B) && !hasGun) // 
            foreach (Animator a in animator)
                a.SetBool("balalayka", !a.GetBool("balalayka"));

        if (Input.GetKeyDown(KeyCode.L) && !hasGun) // Idle 2
            foreach (Animator a in animator)
                a.SetTrigger("lookaround");

        if (Input.GetKeyDown(KeyCode.H)) // HIT
                foreach (Animator a in animator)
                {
                    if (a.GetBool("gun_idle"))
                        if (Input.GetKey(KeyCode.LeftShift)) a.SetTrigger("a_damage");
                        else a.SetTrigger("a_damage_m");
                    else
                        if (Input.GetKey(KeyCode.LeftShift)) a.SetTrigger("n_damage");
                        else a.SetTrigger("n_damage_m");
            }

        if (Input.GetKeyDown("1") && !hasGun) //
            foreach (Animator a in animator)
                a.SetTrigger("n_attack1");

        if (Input.GetKeyDown("2") && !hasGun) // 
            foreach (Animator a in animator)
                a.SetTrigger("n_attack2");

        if (Input.GetKeyDown("3") &&!hasGun) // 
            foreach (Animator a in animator)
                a.SetBool("n_attack3", !a.GetBool("n_attack3"));

        if (Input.GetKeyDown("4") && hasGun) // 
            foreach (Animator a in animator)
                a.SetTrigger("a_shot_1");

        if (Input.GetKeyDown("5") && hasGun) // 
            foreach (Animator a in animator)
                a.SetBool("a_shot_2", !a.GetBool("a_shot_2"));

        if (Input.GetKeyDown(KeyCode.V) && !hasGun) // 
            foreach (Animator a in animator)
                a.SetTrigger("drink");

        if (Input.GetKeyDown("0")) // 
            foreach (Animator a in animator)
                a.SetTrigger("death");

        if (Input.GetKeyDown("9")) // 
            foreach (Animator a in animator)
                a.SetTrigger("undead");

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Bear.RotateAround(Bear.position, -Vector3.up, Time.deltaTime * 94);
            foreach (Animator a in animator)
                if (a.GetBool("gun_idle")) a.SetBool("a_turn_l", true);
                else a.SetBool("n_turn_l", true);
        } else
            foreach (Animator a in animator)
                if (a.GetBool("gun_idle")) a.SetBool("a_turn_l", false);
                else a.SetBool("n_turn_l", false);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Bear.RotateAround(Bear.position, Vector3.up, Time.deltaTime * 94);
            foreach (Animator a in animator)
                if (a.GetBool("gun_idle")) a.SetBool("a_turn_r", true);
                else a.SetBool("n_turn_r", true);
        }
        else
            foreach (Animator a in animator)
                if (a.GetBool("gun_idle")) a.SetBool("a_turn_r", false);
                else a.SetBool("n_turn_r", false);


        if (Input.GetKeyDown(KeyCode.F1)) //  
        {
            Transform tm = Bear.Find("mesh_dejurny");
            tm.gameObject.SetActive(!tm.gameObject.activeSelf);
            return;
        }
        if (Input.GetKeyDown(KeyCode.F2)) //  
        {
            Transform tm = Bear.Find("mesh_kompass");
            tm.gameObject.SetActive(!tm.gameObject.activeSelf);
            return;
        }
        if (Input.GetKeyDown(KeyCode.F3)) //  
        {
            Transform tm = Bear.Find("mesh_rukzak");
            tm.gameObject.SetActive(!tm.gameObject.activeSelf);
            return;
        }
        if (Input.GetKeyDown(KeyCode.F4)) //  
        {
            Transform tm = Bear.Find("mesh_belt");
            bool a = !tm.gameObject.activeSelf;
            tm.gameObject.SetActive(a);
            return;
        }

        // --
        if (Input.GetKey("a"))
        {
            yaw -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("d"))
        {
            yaw += Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("w") && Distance > 2f)
        {
            Distance -= Time.deltaTime * ZoomStep;
        }

        if (Input.GetKey("s") && Distance < 14f)
        {
            Distance += Time.deltaTime * ZoomStep;
        }
        if (Input.GetKey("q") && Pitch > 10f)
        {
            Pitch -= Time.deltaTime * RotateSpeed;
        }
        if (Input.GetKey("e") && Pitch < 75f)
        {
            Pitch += Time.deltaTime * RotateSpeed;
        }

        LookPoint = Vector3.MoveTowards(LookPoint, Target.position + Vector3.up * zOffset, TargetMoveSpeed * Time.deltaTime);

        TargetPos = GetPosition();
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, MoveSpeed * Time.deltaTime);
        transform.LookAt(LookPoint);
    }

    Vector3 GetPosition()
    {
        float y = Distance * Mathf.Sin(Pitch * Mathf.Deg2Rad);
        float r = Distance * Mathf.Cos(Pitch * Mathf.Deg2Rad);
        float x = r * Mathf.Cos(yaw * Mathf.Deg2Rad);
        float z = r * Mathf.Sin(yaw * Mathf.Deg2Rad);

        return (Target.position + new Vector3(x, y, z));
    }

}