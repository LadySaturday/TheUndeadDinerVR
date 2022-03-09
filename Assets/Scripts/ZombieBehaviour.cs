using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour
{
    private Animator anim;
    private OrderManager orderManager;//a reference to the script that produces orders
    private GameObject timer;
    //private Timers timeBehave;
    public StatesEnum State;
    private float itemReceiveReward = 2f;//time to pause the zombie timer for when receiving an item
    public void changeStates(int stateNum)
    {
        switch (stateNum)
        {
            case 0:
                enter();//getOrder and set up the timer
                break;
            case 1:
                waiting();//wait to be handed items
                break;
            case 2:
                receiveItem();//check the item you were handed
                break;
            case 3:
                timerExpired();//called by an animation event
                break;
            case 4:
                exit();
                break;
            case 5:
                gameOver();
                break;
            default:
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        orderManager = GameObject.FindGameObjectWithTag("orderManager").GetComponent<OrderManager>();
        anim = GetComponent<Animator>();
        changeStates(0);
    }

    int delayTime;
    private System.Random rnd = new System.Random();//new random, for orders
    //ZombieEnters
    void enter()
    {
        rnd = new System.Random();
        delayTime = rnd.Next(1, 4);//random time between zombies 


        StartCoroutine(getOrder(delayTime));
    }


    //Zombie receives order
    private GameObject[] orderItems;//all prefabs in the order
    private GameObject[] instantiatedOrderItems;//all instantiated items in the order in the scene
    public GameObject[] orderBackgrounds;//a list of backgrounds for orders (1,2 and 3)
    public Transform orderVisual;//reference to the position it should appear in
    int orderLength;

    GameObject orderBg;//the background corresponding to the order
    IEnumerator getOrder(int delayTime)
    {
        yield return new WaitForSeconds(delayTime);


        //receive order from another  script and spawn it

        orderItems = orderManager.setOrder();


        if (orderItems != null)
        {
            orderLength = orderItems.Length;
           // anim.SetTrigger("Enter");
            Vector3 imageOffset = new Vector3(0, -0.25f);//the offset for the items in the order visual

            orderBg = Instantiate(orderBackgrounds[orderLength - 1], orderVisual.position, Quaternion.identity, orderVisual);//Spawn correct background for number of items
            instantiatedOrderItems = new GameObject[orderLength];
            for (int x = 0; x < orderLength; x++)//spawn the order items
            {

                instantiatedOrderItems[x] = Instantiate(orderItems[x],
                orderVisual.position + imageOffset,
                Quaternion.identity);//child to the background
                instantiatedOrderItems[x].transform.SetParent(orderBg.transform, true);
                imageOffset.y -= 0.5f;//places next order visual lower
            }

            changeStates(1);//wait

        }

    }

    //Zombie waits for player to bring items
    public bool canHaveTimer;
    void waiting()
    {
        //spawn the timer
        if (canHaveTimer)
        {
            //timer = GetComponent<TimerInstantiate>().instantiateTimer();
            //timeBehave = timer.GetComponentInChildren<Timers>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (orderItems != null)//an order exists
        {
            int orderItemNum = -1;
            foreach(GameObject item in orderItems) {
                orderItemNum++;
                if(orderItems[orderItemNum]!=null)
                if (orderItems[orderItemNum].CompareTag(other.gameObject.tag))//checks both hands against every item in the zombie's order
                {
                    Debug.Log(":Destroy Item");
                    Destroy(other.gameObject);
                    orderItems[orderItemNum] = null;
                    Destroy(instantiatedOrderItems[orderItemNum]);
                    orderLength--;
                    if (orderLength < 1)//we completed all orders
                    {
                        changeStates(4);//exit
                        
                    }
                    //else timeBehave.addTime(itemReceiveReward);
                    break;
                }
                else
                {
                    Debug.Log(other.gameObject.tag);
                    Debug.Log(orderItems[orderItemNum].gameObject.tag);
                }
            }
        }
    }
    void receiveItem()
    {

        //animation

    }

    //Zombies timer expired
    void timerExpired()
    {
        //get the order manager and fail, then exit

        orderManager.ZombieFailed();
        changeStates(4);
    }

    //Zombie exits
    void exit()
    {
        if (timer != null)
        {
            Destroy(timer);
        }

      //  anim.SetBool("Exit", true);

        DestroyChildren(orderVisual.transform);
        orderManager.orderCompleted(this.gameObject.tag.ToString());
    }

    void gameOver()
    {
        Destroy(timer);
        DestroyChildren(orderVisual.transform);
    }

    private static void DestroyChildren(Transform transform)
    {
        try
        {
            for (int i = transform.childCount - 1; i >= 0; --i)
            {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }
            transform.DetachChildren();
        }
        catch (System.Exception)
        {
            Debug.Log("Ignoring this exception for now");
        }

    }




}
