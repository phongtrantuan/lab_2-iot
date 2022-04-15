

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace lab2
{
    public class ChuongGaManager : MonoBehaviour
    {
        // public static UIManager instance;
        public InputField brokerURI_ipF;
        public InputField username_ipF;
        public InputField password_ipF;

        public Text errorTxt;

        public Text tempTxt;
        public Text humTxt;
        public Slider ledSlider;
        public Slider pumpSlider;

        public GameObject screen1;
        public GameObject screen2;
    
        private float ledStatus = 1;
        private float pumpStatus = 1;

        void Start()
        {
            brokerURI_ipF.text = "mqttserver.tk";
            username_ipF.text = "bkiot";
            password_ipF.text = "12345678";
            // errorTxt.SetActive(false);
            errorTxt.text = "";
        }


        public void updateStatus(string temp, string hum)
        {
            tempTxt.text = temp;
            humTxt.text = hum;
        }

        public void login()
        {
            
            if (username_ipF.text == "bkiot" && password_ipF.text == "12345678" && brokerURI_ipF.text == "mqttserver.tk")
            {
                GetComponent<ChuongGaMqtt>().UpdateBeforeConnect();
                Debug.Log("Login");
                screen1.SetActive(false);
                screen2.SetActive(true);
                tempTxt.text = "30";
                humTxt.text = "50";
            }
            else
            {
                StartCoroutine(WaitForSeconds(3));
                // errorTxt.SetActive(true);
                Debug.Log("Wrong account");
            }
        }

        IEnumerator WaitForSeconds(float time)
        {
            // errorTxt.SetActive(true);
            errorTxt.text = "CONNECTION FAILED";
            yield return new WaitForSeconds(time);
            errorTxt.text = "";
            // errorTxt.SetActive(false);
        }
        // public void checkAccount()
        // {
        //     if (username_ipF.text == "bkiot" && password_ipF.text == "12345678" && brokerURI_ipF.text == "mqttserver.tk")
        //     {
        //         screen1.SetActive(false);
        //         screen2.SetActive(true);
        //     }
        //     else
        //     {
        //         Debug.Log("Wrong account");
        //     }
        // }
        

        public void changeLed()
        {
            if (ledSlider.value != ledStatus)
            {
                try {
                GetComponent<ChuongGaMqtt>().PublishConfigLed(ledSlider.value == 1 ? true : false);
                }
                catch (Exception e){
                    Debug.Log(e.Message);
                }
                ledStatus = ledSlider.value;
            }
        }

        public void changePump()
        {
           if(pumpSlider.value != pumpStatus)
            {
                GetComponent<ChuongGaMqtt>().PublishConfigPump(pumpSlider.value == 1 ? true : false);
                pumpStatus = pumpSlider.value;
            }
        }

        public void updateLed(int status)
        {
            ledSlider.value =  status;
        }

        public void updatePump(int status)
        {
            pumpSlider.value = status;
        }

        public void logout()
        {
            // brokerURI_ipF.text = "";
            // username_ipF.text = "";
            // password_ipF.text = "";
            GetComponent<ChuongGaMqtt>().Disconnect();
            screen1.SetActive(true); 
            screen2.SetActive(false);
        }
    }
}