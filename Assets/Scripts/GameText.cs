using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameText : MonoBehaviour
{
    GameManager gm;
    AudioSource audioSelect;

    public GameObject selectText;
    public GameObject scoreText;

    public int score;

    int selectTime = 0;
    float antilagTimer = 0;
    string firstTag;
    string secondTag;
    bool isTransformed; // 记录是否有转换发生

    GameObject[] firstSelections;
    GameObject[] secondSelections;
    GameObject[] originSelectionsCollectible;
    GameObject[] originSelectionsSpike;
    GameObject[] originSelectionsBlock;
    GameObject[] originSelectionsEnemy;
    GameObject[] originSelectionsJumppad;
    GameObject[] selectFramesCollectible;
    GameObject[] selectFramesSpike;
    GameObject[] selectFramesBlock;
    GameObject[] selectFramesEnemy;
    GameObject[] selectFramesJumppad;
    GameObject[] transformAnims;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        audioSelect = GameObject.Find("AudioPlayerSelect").GetComponent<AudioSource>();

        originSelectionsCollectible = GameObject.FindGameObjectsWithTag("Collectible");
        originSelectionsSpike = GameObject.FindGameObjectsWithTag("Spike");
        originSelectionsBlock = GameObject.FindGameObjectsWithTag("Block");
        originSelectionsEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        originSelectionsJumppad = GameObject.FindGameObjectsWithTag("Jumppad");
        selectFramesCollectible = GameObject.FindGameObjectsWithTag("SelectFrameCollectible");
        selectFramesSpike = GameObject.FindGameObjectsWithTag("SelectFrameSpike");
        selectFramesBlock = GameObject.FindGameObjectsWithTag("SelectFrameBlock");
        selectFramesEnemy = GameObject.FindGameObjectsWithTag("SelectFrameEnemy");
        selectFramesJumppad = GameObject.FindGameObjectsWithTag("SelectFrameJumppad");
        transformAnims = GameObject.FindGameObjectsWithTag("TransformAnimation");

        score = gm.score;
        selectText.GetComponent<Text>().text = null;

        // 初始化时隐藏所有选择框
        foreach (GameObject selectFrameCollectible in selectFramesCollectible)
        {
            selectFrameCollectible.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject selectFrameSpike in selectFramesSpike)
        {
            selectFrameSpike.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject selectFrameBlock in selectFramesBlock)
        {
            selectFrameBlock.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject selectFrameEnemy in selectFramesEnemy)
        {
            selectFrameEnemy.GetComponent<SpriteRenderer>().enabled = false;
        }
        foreach (GameObject selectFrameJumppad in selectFramesJumppad)
        {
            selectFrameJumppad.GetComponent<SpriteRenderer>().enabled = false;
        }

        // 初始化时隐藏所有转换动画
        foreach (GameObject transformAnim in transformAnims)
        {
            transformAnim.SetActive(false);
        }
    }

    void Update()
    {
        antilagTimer += Time.deltaTime;

        // 按LShift重置所有实体
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isTransformed)
            {
                GameObject.Find("AudioRevert").GetComponent<AudioSource>().Play();
            }

            // 隐藏所有选择框
            foreach (GameObject selectFrameCollectible in selectFramesCollectible)
            {
                selectFrameCollectible.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameSpike in selectFramesSpike)
            {
                selectFrameSpike.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameBlock in selectFramesBlock)
            {
                selectFrameBlock.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameEnemy in selectFramesEnemy)
            {
                selectFrameEnemy.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameJumppad in selectFramesJumppad)
            {
                selectFrameJumppad.GetComponent<SpriteRenderer>().enabled = false;
            }

            // 转换前先播放转换动画
            foreach (GameObject transformAnim in transformAnims)
            {
                if (isTransformed)
                {
                    if (transformAnim.transform.parent.tag == "Collectible" || transformAnim.transform.parent.tag == "Spike" || transformAnim.transform.parent.tag == "Block" || transformAnim.transform.parent.tag == "Enemy" || transformAnim.transform.parent.tag == "Jumppad")
                    {
                        transformAnim.SetActive(true);
                    }
                }
            }

            // 开始转换
            foreach (GameObject originSelectionCollectible in originSelectionsCollectible)
            {
                originSelectionCollectible.tag = "Collectible";
            }
            foreach (GameObject originSelectioSpike in originSelectionsSpike)
            {
                originSelectioSpike.tag = "Spike";
            }
            foreach (GameObject originSelectioBlock in originSelectionsBlock)
            {
                originSelectioBlock.tag = "Block";
            }
            foreach (GameObject originSelectioEnemy in originSelectionsEnemy)
            {
                originSelectioEnemy.tag = "Enemy";
            }
            foreach (GameObject originSelectionJumppad in originSelectionsJumppad)
            {
                originSelectionJumppad.tag = "Jumppad";
            }

            isTransformed = false;
            selectTime = 0;
        }

        // 按X取消选定
        if (Input.GetKeyDown(KeyCode.X))
        {
            selectTime = 0;
            selectText.GetComponent<Text>().text = "选择已重置!";

            // 隐藏所有选择框
            foreach (GameObject selectFrameCollectible in selectFramesCollectible)
            {
                selectFrameCollectible.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameSpike in selectFramesSpike)
            {
                selectFrameSpike.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameBlock in selectFramesBlock)
            {
                selectFrameBlock.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameEnemy in selectFramesEnemy)
            {
                selectFrameEnemy.GetComponent<SpriteRenderer>().enabled = false;
            }
            foreach (GameObject selectFrameJumppad in selectFramesJumppad)
            {
                selectFrameJumppad.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        scoreText.GetComponent<Text>().text = score.ToString();

        GameObject.Find("End").GetComponent<EndTrophy>().SyncScore(score);
    }

    public void MakeSelection(string tag)
    {
        if (antilagTimer > 0.2f)
        {
            if (selectTime == 0)
            {
                firstTag = tag;
                selectTime++;
                selectText.GetComponent<Text>().text = "已选中第一个实体：" + firstTag;
                firstSelections = GameObject.FindGameObjectsWithTag(firstTag);

                audioSelect.Play();

                // 选中第一个实体时出现选择框
                switch (firstTag)
                {
                    case "Collectible":
                        foreach (GameObject selectFrameCollectible in selectFramesCollectible)
                        {
                            if (selectFrameCollectible.transform.parent.tag == "Collectible")
                            {
                                selectFrameCollectible.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }
                        break;
                    case "Spike":
                        foreach (GameObject selectFrameSpike in selectFramesSpike)
                        {
                            if (selectFrameSpike.transform.parent.tag == "Spike")
                            {
                                selectFrameSpike.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }
                        break;
                    case "Block":
                        foreach (GameObject selectFrameBlock in selectFramesBlock)
                        {
                            if (selectFrameBlock.transform.parent.tag == "Block")
                            {
                                selectFrameBlock.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }
                        break;
                    case "Enemy":
                        foreach (GameObject selectFrameEnemy in selectFramesEnemy)
                        {
                            if (selectFrameEnemy.transform.parent.tag == "Enemy")
                            {
                                selectFrameEnemy.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }
                        break;
                    case "Jumppad":
                        foreach (GameObject selectFrameJumppad in selectFramesJumppad)
                        {
                            if (selectFrameJumppad.transform.parent.tag == "Jumppad")
                            {
                                selectFrameJumppad.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                secondTag = tag;
                if (firstTag != secondTag)
                {
                    secondSelections = GameObject.FindGameObjectsWithTag(secondTag);
                    GameObject.Find("AudioSwap").GetComponent<AudioSource>().Play();

                    // 播放转换动画
                    foreach (GameObject transformAnim in transformAnims)
                    {
                        if (transformAnim.transform.parent.tag == firstTag || transformAnim.transform.parent.tag == secondTag)
                        {
                            transformAnim.SetActive(true);
                        }
                    }

                    // 开始转换选中的两类实体
                    foreach (GameObject firstSelection in firstSelections)
                    {
                        firstSelection.tag = secondTag;
                    }
                    foreach (GameObject secondSelection in secondSelections)
                    {
                        secondSelection.tag = firstTag;
                    }

                    // 转换完成后隐藏所有选择框
                    foreach (GameObject selectFrameCollectible in selectFramesCollectible)
                    {
                        selectFrameCollectible.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    foreach (GameObject selectFrameSpike in selectFramesSpike)
                    {
                        selectFrameSpike.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    foreach (GameObject selectFrameBlock in selectFramesBlock)
                    {
                        selectFrameBlock.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    foreach (GameObject selectFrameEnemy in selectFramesEnemy)
                    {
                        selectFrameEnemy.GetComponent<SpriteRenderer>().enabled = false;
                    }
                    foreach (GameObject selectFrameJumppad in selectFramesJumppad)
                    {
                        selectFrameJumppad.GetComponent<SpriteRenderer>().enabled = false;
                    }

                    isTransformed = true;
                    selectText.GetComponent<Text>().text = "已完成转换！";
                    selectTime = 0;
                }
            }
            antilagTimer = 0;
        }
    }

    public void EnemyDestroyed()
    {
        score += 500;
    }

    public void EntityColledted()
    {
        score += 100;
    }

    public void EntityStarColledted()
    {
        score += 10000;
    }

    public void SaveScore()
    {
        gm.score = score;
    }
}
