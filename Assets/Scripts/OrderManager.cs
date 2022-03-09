

using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//The purpose of this script is to act as the order system. Randomize orders. See if the player has the right order. 
//If they do, destroy what they're holding. Spawn the next order
public class OrderManager : MonoBehaviour
{

    public GameObject gameOver;
    //Reference to products and to player
    [SerializeField]
    public GameObject[] products;//ALL products being served
    [SerializeField]
    public GameObject[] productImages;//ALL products being served
    public GameObject zombieR;
    private int numberOfOrderItems;//before an order is created, determine the number of items in that order

    public GameObject winPanel;
    public GameObject losePanel;

    //Score
    private static int zombiesFailed = 0;
    private Text zombiesFailedTxt;
    private Text timeTxt;
    
    
    private GameObject[] zombieOrders;



    private float Timer;//a timer to make the order items harder

    

    private System.Random rnd = new System.Random();//new random, for orders


    private int maxOrderNumber = 3;//max number of items in an order

    public bool isOrderRandom;
    private OrderList orderList;
    private int[,] orderItems;
    private int numOfOrders;
    int levelIndex;
    void Start()
    {
        Time.timeScale = 1;
        if (!isOrderRandom)
        {
            //get the non random order from the given game object
             levelIndex = GlobalVariables.Get<int>("currentLevelIndex");
            Debug.Log("Level " + levelIndex);
            orderList = GetComponent<OrderList>();
            orderItems = orderList.getOrder(levelIndex);
            numOfOrders=(orderItems.Length/4)-1;
            currentOrderNum = 0;
        }
        else
        {
            resetValues();
           // timeTxt = GameObject.FindGameObjectWithTag("timerTxt").GetComponent<Text>();
        }

       // zombiesFailedTxt = GameObject.FindGameObjectWithTag("zombiesFailedTxt").GetComponent<Text>();
        
    }


    private void Update()
    {
      //  if (isOrderRandom)
       // {
          //  Timer += Time.deltaTime;
          //  timeTxt.text = "Time played: " + Timer.ToString("F",
           //           CultureInfo.InvariantCulture);
          //  changMaxOrder();
      //  }
           

    }


   /*
    void changMaxOrder()
    {
        if (Timer > 30&&Timer<31)//max number of items gets up to 2
        {
            maxOrderNumber = 2;
        }            
        else if(Timer > 60 && Timer < 61)//second zombie
        {
              zombieR.SetActive(true);
        }
        else if (Timer > 120 && Timer < 121)//max number of item gets up tp 3
        {
            maxOrderNumber = 3;
        }


    }*/

    private int currentOrderNum=0;
    public GameObject[] setOrder()
    {
       
        if (isOrderRandom)
        {
            rnd = new System.Random();
            numberOfOrderItems = rnd.Next(1, maxOrderNumber + 1);//between 1 and 3 items per order  
            zombieOrders = new GameObject[numberOfOrderItems];//temp order

            for (int x = 0; x < numberOfOrderItems; x++)
            {
                int y = rnd.Next(products.Length);//choose any number corresponing to a product
                zombieOrders[x] = productImages[y];
            }


        }
        else//order is not random. We can plan orders ahead.
        {
            //we are given a doc of arrays
            //we extrapolate numbers from the given arrays
            //eg we are given an array as
            /*
             * [1][3][3]
             * [3][2][3]
             * [n][n][1]
             * the coloumn is one order
             * the rows are an order item
             * the numbers correspond to products in the array already created
             * 
             * 
             */

            zombieOrders = null;
            
            //total orders to go through
            //4 items in each order
            //length returns all dimensions
            //so length/4 returns the actual value
            if (currentOrderNum == numOfOrders+2)
            {
                winPanel.SetActive(true);
                PlayerPrefs.SetString(levelIndex.ToString(), "true");

            }
            else if (currentOrderNum!=numOfOrders+1)
            {
                Debug.Log(currentOrderNum + "/" + numOfOrders);
                zombieOrders = new GameObject[3];//temp order
                for (int x = 0; x < 3; x++)
                {
                    if (orderItems[currentOrderNum, x] != -1)
                        zombieOrders[x] = productImages[orderItems[currentOrderNum, x]];
                    else
                        Array.Resize(ref zombieOrders, zombieOrders.Length - 1);
                }
            }
            currentOrderNum++;

        }
            return zombieOrders;
    }

    public void ZombieFailed()//did a zombie's timer run out? (Invoked by animation event)
    {


        zombiesFailed++;//after 3 failed zombies, it's game over
        //int itemNum = 0;
        if (zombiesFailed > 2)
        {
            StartCoroutine(GameOver(false, 5));

        }
        zombiesFailedTxt.text = string.Format("Zombies failed: {0} /3", zombiesFailed.ToString());

    }

    public void orderCompleted(string tag)
    {
                
        zombieBehaviour = GameObject.FindGameObjectWithTag(tag).GetComponent<ZombieBehaviour>();
        zombieBehaviour.changeStates(0);
    }
    
    
    private ZombieBehaviour zombieBehaviour;


    IEnumerator GameOver(bool status, float delayTime)
    {
        zombieBehaviour = GameObject.FindGameObjectWithTag("ZombieR").GetComponent<ZombieBehaviour>();
        zombieBehaviour.changeStates(5);//exit

        zombieBehaviour = GameObject.FindGameObjectWithTag("ZombieL").GetComponent<ZombieBehaviour>();
        zombieBehaviour.changeStates(5);//exit

        if (isOrderRandom)
        {
            float hiScore;
            try
            {
                hiScore = PlayerPrefs.GetFloat("hiScore");
            }
            catch (System.Exception)
            {
                hiScore = 0;
            }

            if (Timer > hiScore)
            {
                PlayerPrefs.SetFloat("hiScore", Timer);
                gameOver.SetActive(true);
                gameOver.GetComponentInChildren<Text>().text = ("YOU WEREN'T FAST ENOUGH!\nZombies ate your brains.\nYou lasted " + Timer.ToString("F", CultureInfo.InvariantCulture) + " seconds\nNew high score!");

            }
            else
            {
                gameOver.SetActive(true);
                gameOver.GetComponentInChildren<Text>().text = ("YOU WEREN'T FAST ENOUGH!\nZombies ate your brains.\nYou lasted " + Timer.ToString("F", CultureInfo.InvariantCulture) + " seconds\nHighScore: " + hiScore.ToString("F", CultureInfo.InvariantCulture) + " seconds");

            }

        }





        yield return new WaitForSeconds(delayTime);
        resetValues();
        if (isOrderRandom)
        {
           
            SceneManager.LoadScene(4);//temporary
        }
        else
        {
            losePanel.SetActive(true);
        }
       

    }

    private void resetValues()
    {
        Time.timeScale = 1;
        zombieR.SetActive(false);

       // gameOver.SetActive(false);
        maxOrderNumber = 2;
        Timer = 0;

        

        zombiesFailed = 0;
        
    }
   

}
