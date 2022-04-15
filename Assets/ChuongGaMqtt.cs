// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using uPLibrary.Networking.M2Mqtt;
// using uPLibrary.Networking.M2Mqtt.Messages;
// using M2MqttUnity;
// using Newtonsoft.Json.Linq;
// using System.Linq;
// using Newtonsoft.Json;

// namespace ChuongGa
// {
//     public class Status_Data
//     {
//         public string temperature { get; set; }
//         public string humidity { get; set; }
        
//     }

//     public class Device{
//         public string device { get; set; }//led-pump
//         public string status { get; set; }//on-off
//     }

//     public class ChuongGaMqtt : M2MqttUnityClient
//     {
//         public List<string> topics = new List<string>();


//         public string msg_received_from_topic_status = "";
//         public string msg_received_from_topic_control = "";


//         private List<string> eventMessages = new List<string>();
//         [SerializeField]
//         public Status_Data _status_data;
//         [SerializeField]
//         public Config_Data _config_data;
//         [SerializeField]
//         public ControlFan_Data _controlFan_data;
        


//         public void PublishDeviceInfo()
//         {
//             // _config_data = new Config_Data();
//             // GetComponent<ChuongGaManager>().Update_Config_Value(_config_data);
//             Device temp = new Device();
//             temp.device = "LED";
//             temp.status = "on";
//             string msg_config = JsonConvert.SerializeObject(temp);
//             client.Publish(topics[1], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
//             Debug.Log("publish config");
//         }

//         public void PublishStatusData()
//         {
//             // _config_data = new Config_Data();
//             // GetComponent<ChuongGaManager>().Update_Config_Value(_config_data);
//             Device temp = new Status_Data();
//             temp.temperature = "34";
//             temp.humidity = "81";
//             string msg_config = JsonConvert.SerializeObject(temp);
//             client.Publish(topics[0], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
//             Debug.Log("publish config");
//         }

//         public void PublishConfig(string name, string sta)
//         {
//             // _config_data = new Config_Data();
//             // GetComponent<ChuongGaManager>().Update_Config_Value(_config_data);
//             Device temp = new Device();
//             temp.device = "LED";
//             temp.status = "on";
//             string msg_config = JsonConvert.SerializeObject(temp);
//             client.Publish(topics[1], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
//             Debug.Log("publish config");
//         }

//         public void PublishFan()
//         {
//             _controlFan_data = GetComponent<ChuongGaManager>().Update_ControlFan_Value(_controlFan_data);
//             string msg_config = JsonConvert.SerializeObject(_controlFan_data);
//             client.Publish(topics[2], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
//             Debug.Log("publish fan");


//         }

//         public void SetEncrypted(bool isEncrypted)
//         {
//             this.isEncrypted = isEncrypted;
//         }

//         protected override void OnConnecting()
//         {
//             base.OnConnecting();
//             //SetUiMessage("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
//         }

//         protected override void OnConnected()
//         {
//             base.OnConnected();

//             SubscribeTopics();
//         }

//         protected override void SubscribeTopics()
//         {

//             foreach (string topic in topics)
//             {
//                 if (topic != "")
//                 {
//                     client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

//                 }
//             }
//         }

//         protected override void UnsubscribeTopics()
//         {
//             foreach (string topic in topics)
//             {
//                 if (topic != "")
//                 {
//                     client.Unsubscribe(new string[] { topic });
//                 }
//             }

//         }

//         protected override void OnConnectionFailed(string errorMessage)
//         {
//             Debug.Log("CONNECTION FAILED! " + errorMessage);
//         }

//         protected override void OnDisconnected()
//         {
//             Debug.Log("Disconnected.");
//         }

//         protected override void OnConnectionLost()
//         {
//             Debug.Log("CONNECTION LOST!");
//         }



//         protected override void Start()
//         {

//             base.Start();
//         }

//         protected override void DecodeMessage(string topic, byte[] message)
//         {
//             string msg = System.Text.Encoding.UTF8.GetString(message);
//             Debug.Log("Received: " + msg);
//             //StoreMessage(msg);
//             if (topic == topics[0])
//                 ProcessMessageStatus(msg);

//             if (topic == topics[1])
//                 ProcessMessageControlLed(msg);

//             if (topic == topics[2])
//                 ProcessMessageControlPump(msg);
//         }

//         private void ProcessMessageStatus(string msg)
//         {
//             _status_data = JsonConvert.DeserializeObject<Status_Data>(msg);
//             msg_received_from_topic_status = msg;
//             // {"abc":ab, "bcd":c} _status_data.abc
//             GetComponent<ChuongGaManager>().Update_Status(_status_data);

//         }

//         private void ProcessMessageControlLed(string msg)
//         {
//             _controlFan_data = JsonConvert.DeserializeObject<ControlFan_Data>(msg);
//             msg_received_from_topic_control = msg;
//             GetComponent<ChuongGaManager>().Update_Control(_controlFan_data);

//         }

//         private void ProcessMessageControlPump(string msg)
//         {
//             _controlFan_data = JsonConvert.DeserializeObject<ControlFan_Data>(msg);
//             msg_received_from_topic_control = msg;
//             GetComponent<ChuongGaManager>().Update_Control(_controlFan_data);

//         }

//         private void OnDestroy()
//         {
//             Disconnect();
//         }

//         private void OnValidate()
//         {
//             //if (autoTest)
//             //{
//             //    autoConnect = true;
//             //}
//         }

//         public void UpdateConfig()
//         {
           
//         }

//         public void UpdateControl()
//         {

//         }
//     }
// }


















using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>
namespace lab2
{

    public class data_status
    {
        public string temp { get; set; }
        public string humi { get; set; }
    }

    public class config_data
    {
        public string device { get; set; }
        public string status { get; set; }
    }
    public class ChuongGaMqtt : M2MqttUnityClient
    {

        public List<string> topics = new List<string>();
        // public string Topic_to_Subcribe = "";
        // public string msg_received_from_topic_status = "";
        // public string msg_received_from_topic_led = "";
        // public string msg_received_from_topic_pump = "";
        private List<string> eventMessages = new List<string>();
        public data_status status;
        public config_data led;
        public config_data pump;


        public void UpdateBeforeConnect()
        {
            this.brokerAddress = GetComponent<ChuongGaManager>().brokerURI_ipF.text;
            this.mqttUserName = GetComponent<ChuongGaManager>().username_ipF.text;
            this.mqttPassword = GetComponent<ChuongGaManager>().password_ipF.text;
            this.brokerPort = 1883;
            this.Connect();
            
        }
        // public void TestPublish()
        // {
        //     client.Publish(Topic_to_Subcribe, System.Text.Encoding.UTF8.GetBytes("Test message"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        //     Debug.Log("Test message published");
        //     // AddUiMessage("Test message published.");
        // }

        public void SetEncrypted(bool isEncrypted)
        {
            this.isEncrypted = isEncrypted;
        }


        protected override void OnConnecting()
        {
            try
            {
                base.OnConnecting();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        protected override void OnConnected()
        {
            // GetComponent<ChuongGaManager>().switch_layer2();
            base.OnConnected();
            SubscribeTopics();
        }

        protected override void SubscribeTopics()
        {

            foreach (string topic in topics)
            {
                if (topic != "")
                {
                    client.Subscribe(new string[] { topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                }
            }
        }

        protected override void UnsubscribeTopics()
        {
            foreach (string topic in topics)
            {
                if (topic != "")
                {
                    client.Unsubscribe(new string[] { topic });
                }
            }

        }

        protected override void OnConnectionFailed(string errorMessage)
        {
            Debug.Log("CONNECTION FAILED! " + errorMessage);
        }

        protected override void OnDisconnected()
        {
            Debug.Log("Disconnected.");
        }

        protected override void OnConnectionLost()
        {
            Debug.Log("CONNECTION LOST!");
        }



        protected override void Start()
        {
            base.Start();
        }

        protected override void DecodeMessage(string topic, byte[] message)
        {
            string msg = System.Text.Encoding.UTF8.GetString(message);
            Debug.Log("Received: " + msg);
            if (topic == topics[0])
            {
                Debug.Log("status");
                ProcessMessageStatus(msg);
                Debug.Log(msg);

            }
            if (topic == topics[1])
            {
                Debug.Log("led");
                ProcessMessageControlLed(msg);
            }
            if (topic == topics[2])
            {
                Debug.Log("pump");
                ProcessMessageControlPump(msg);
            }
        }

        private void ProcessMessageStatus(string msg)
        {
            status = JsonConvert.DeserializeObject<data_status>(msg);
            GetComponent<ChuongGaManager>().updateStatus(status.temp, status.humi);
        }

        private void ProcessMessageControlLed(string msg)
        {
            led = JsonConvert.DeserializeObject<config_data>(msg);
            GetComponent<ChuongGaManager>().updateLed(led.status == "ON" ? 1 : 0);
        }

        private void ProcessMessageControlPump(string msg)
        {
            pump = JsonConvert.DeserializeObject<config_data>(msg);
            GetComponent<ChuongGaManager>().updatePump(pump.status == "ON" ? 1 : 0);
        }

        public void PublishConfigLed(bool on)
        {
            config_data ledData = new config_data();
            ledData.device = "LED";
            ledData.status = on? "ON" : "OFF";
            string msg_config = JsonConvert.SerializeObject(ledData);
            client.Publish(topics[1], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Debug.Log("Publish LED");
        }

        public void PublishConfigPump(bool on)
        {
            config_data pumpData = new config_data();
            pumpData.device = "PUMP";
            pumpData.status = on == true ? "ON" : "OFF";
            string msg_config = JsonConvert.SerializeObject(pumpData);
            client.Publish(topics[2], System.Text.Encoding.UTF8.GetBytes(msg_config), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
            Debug.Log("Publish PUMP");
        }


        private void StoreMessage(string eventMsg)
        {
            eventMessages.Add(eventMsg);
        }

        private void ProcessMessage(string msg)
        {
            // AddUiMessage("Received: " + msg);
        }



        // private void OnDestroy()
        // {
        //     Disconnect();
        //     GetComponent<UIManager>().logout();
        // }

    }


}