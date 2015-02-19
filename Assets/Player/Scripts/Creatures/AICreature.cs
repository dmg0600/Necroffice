using UnityEngine;
using System.Collections;

public class AICreature : Creature {

    private Controller _movementController;
    public override void OnDead()
    {
        //Muere player 
        GameManager.Instance.CreateParticle("BloodSplat", gameObject.transform.position);
        //AudioSource.PlayClipAtPoint(DwarfDeadAudio, transform.position);

        _Life.life.Regenerate();
        GameManager.Instance.LoadLevel(GameManager.Instance.CurrentlyLoadedLevel);
    }

    public override bool IsPlayer()
    {
        return false;
    }

    protected override void initialize()
    {
        base.initialize();
        _movementController = GetComponent<Controller>();
    }

    public void updateAI()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        bool dead = GameManager.Instance.Player.GetComponent<Life>().life.value == 0;
        //Debug.Log(Vector3.Distance(owner.transform.position, GameManager.Instance.Player.transform.position));
        if (distance > _Weapon.Range && !dead)
        {
            move();
        }
        else if (!dead)
        {
            if (!imAttacking())
            {
                StartCoroutine(_Weapon.attackHandler());
                move();
            }
        }
        else
        {
            idle();
        }
    }


    public virtual void move()
    {

        Vector3 direction = (GameManager.Instance.Player.transform.position - transform.position).normalized;

        int layermask = ~(1 << LayerMask.NameToLayer("Creature") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Weapon"));

        float distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

        if (!Physics.Raycast(transform.position, direction, (_visionRange > distance) ? distance : _visionRange, layermask))
        {
            //send movement commands to the movement controller
            _movementController.OnInputXAxis("Horizontal", direction.x);
            _movementController.OnInputZAxis("Vertical", direction.z);
        }

    }


    protected void idle()
    {
        if (objective != Vector3.zero && Vector3.Distance(transform.position, objective) < 2.0f)
        {
            Vector3 direction = (objective - transform.position).normalized;
            BroadcastMessage("OnInputAxis", direction);
        }
        else
        {
            CancelInvoke();
            idleMovement();
        }
    }

    protected void idleMovement()
    {
        //int layermask = ~(1 << LayerMask.NameToLayer("Creature") | 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Weapon"));

        BroadcastMessage("OnInputAxis", selectCurrentDirection());

        Invoke("idleMovement", 5.0f);
    }

    private Vector3 selectObjective()
    {
        Vector3 direction;
        Vector3 currentDir = Vector3.zero;

        RaycastHit hit;

        direction = Vector3.forward;
        if (!Physics.Raycast(transform.position, direction, out hit))
        {
            return direction;
        }

        direction = Vector3.back;
        if (!Physics.Raycast(transform.position, direction, out hit))
        {
            return direction;
        }

        direction = Vector3.left;
        if (!Physics.Raycast(transform.position, direction, out hit))
        {
            return direction;
        }

        direction = Vector3.right;
        if (!Physics.Raycast(transform.position, direction, out hit))
        {
            return direction;
        }

        return Vector3.zero;
    }

    private Vector3 selectCurrentDirection()
    {
        float maxDistance = 0;
        Vector3 direction;
        Vector3 currentDir = Vector3.zero;

        RaycastHit hit;


        direction = Vector3.forward;
        Physics.Raycast(transform.position, direction, out hit);

        if (hit.distance > maxDistance)
        {
            maxDistance = hit.distance;
            currentDir = direction;
        }

        direction = Vector3.back;
        Physics.Raycast(transform.position, direction, out hit);
        if (hit.distance > maxDistance)
        {
            maxDistance = hit.distance;
            currentDir = direction;
        }

        direction = Vector3.left;
        Physics.Raycast(transform.position, direction, out hit);
        if (hit.distance > maxDistance)
        {
            maxDistance = hit.distance;
            currentDir = direction;
        }

        direction = Vector3.right;
        Physics.Raycast(transform.position, direction, out hit);
        if (hit.distance > maxDistance)
        {
            maxDistance = hit.distance;
            currentDir = direction;
        }

        return currentDir;
    }

    //MONO METHODS
    void FixedUpdate()
    {
        updateAI();
    }

    private Vector3 objective = Vector3.zero;

    [SerializeField]
    private float _visionRange = 5.0f;
}
