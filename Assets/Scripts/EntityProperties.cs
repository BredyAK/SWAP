using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityProperties : MonoBehaviour
{
    public Collider2D colCollectible;
    public Collider2D colSpike;
    public Collider2D colBlock;
    public Collider2D colEnemy;
    public Collider2D colJumppad;
    public Collider2D colStar;
  
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>(); // 实体动画

        // 先禁用所有种类实体的碰撞箱
        colCollectible.enabled = false;
        colSpike.enabled = false;
        colBlock.enabled = false;
        colEnemy.enabled = false;
        colJumppad.enabled = false;
        colStar.enabled = false;
    }

    void FixedUpdate()
    {
        switch (gameObject.tag)
        {
            case "Entity":
                break;
            case "Collectible":
                colSpike.enabled = false;
                colBlock.enabled = false;
                colEnemy.enabled = false;
                colJumppad.enabled = false;
                colStar.enabled = false;
                anim.SetInteger("entityType", 0);
                colCollectible.enabled = true;
                break;
            case "Spike":
                colCollectible.enabled = false;
                colBlock.enabled = false;
                colEnemy.enabled = false;
                colJumppad.enabled = false;
                colStar.enabled = false;
                anim.SetInteger("entityType", 1);
                colSpike.enabled = true;
                break;
            case "Block":
                colCollectible.enabled = false;
                colSpike.enabled = false;
                colEnemy.enabled = false;
                colJumppad.enabled = false;
                colStar.enabled = false;
                anim.SetInteger("entityType", 2);
                colBlock.enabled = true;
                break;
            case "Enemy":
                colCollectible.enabled = false;
                colSpike.enabled = false;
                colBlock.enabled = false;
                colJumppad.enabled = false;
                colStar.enabled = false;
                anim.SetInteger("entityType", 3);
                colEnemy.enabled = true;
                break;
            case "Jumppad":
                colCollectible.enabled = false;
                colSpike.enabled = false;
                colBlock.enabled = false;
                colEnemy.enabled = false;
                colStar.enabled = false;
                anim.SetInteger("entityType", 4);
                colJumppad.enabled = true;
                break;
            case "Star":
                colCollectible.enabled = false;
                colSpike.enabled = false;
                colBlock.enabled = false;
                colEnemy.enabled = false;
                colJumppad.enabled = false;
                anim.SetInteger("entityType", 5);
                colStar.enabled = true;
                break;
            default:
                break;
        }
    }

    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    public void ResetJumppad()
    {
        anim.SetBool("isActived", false);
    }
}
