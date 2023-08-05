using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 origPos, targetPos, position;
    [SerializeField] private float timeToMove;
    private bool isMoving, isEnemyInFront, isAttacking;
    [SerializeField] private Animator animator;    
    [SerializeField] private LayerMask enemyLayer;
    AnimatorStateInfo animatorStateInfo;


    private EnemyController enemy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        if(Input.GetKeyDown(KeyCode.D) && !isMoving && !isAttacking)
        {
            if(CheckEnemy()){
                StartCoroutine(Attack());
            }
            else{
                StartCoroutine(MovePlayer(Vector2.right));
            }
        }     
        if(Input.GetKeyDown(KeyCode.A) && !isMoving && !isAttacking)
        {
            StartCoroutine(MovePlayer(Vector2.left));
        }                

    }

    private IEnumerator MovePlayer (Vector2 direction)
    {
        isMoving = true;
        animator.SetBool("isMoving", isMoving);
        float elapsedTime = 0;

        origPos = position;
        targetPos = origPos + direction;

        while(elapsedTime < timeToMove){
            position = Vector2.Lerp(origPos, targetPos, (elapsedTime/timeToMove));
            transform.position = position;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        position = targetPos;
        transform.position = position;
        
        isMoving = false;
        animator.SetBool("isMoving", isMoving);
    }

    private bool CheckEnemy(){
        origPos = position;
        RaycastHit2D hit = Physics2D.Raycast(origPos, Vector2.right, 0.5f, enemyLayer);

        if(hit.collider != null){
            isEnemyInFront = true;
            enemy = hit.collider.GetComponent<EnemyController>();
            // Verificar si el componente está presente
            if (enemy == null)
            {
                Debug.LogError("Collider golpeado no tiene un componente EnemyController!");
            }
        }
        else
        {
            isEnemyInFront = false;
        }
        return isEnemyInFront;
    }

    private IEnumerator Attack(){
        isAttacking = true;
        Debug.Log(isAttacking);
        animator.Play("Player_Attack");
        // Verificar si el componente está presente antes de llamar a TakeDamage
        if (enemy != null)
        {
            enemy.TakeDamage();
        }
        while (!(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)){
            yield return null;
        }
        isAttacking = false;
        isMoving = false;
        Debug.Log(isAttacking);
    }
}
