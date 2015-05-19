using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blink : MonoBehaviour {
	private GameObject danceButton;
	Color BLUE = new Color(0,0,1.0f,1.0f);
	Color SmoothBlue = new Color (0.7f, 0.7f, 1.0f, 1.0f);
	Color MidBlue = new Color (0.85f, 0.85f, 1.0f, 1.0f);
	Color BLACK = new Color(0,0,0,1.0f);
	Color Purple = new Color (0.717f, 0.301f, 0.764f, 1.0f);
	Color Yellow = new Color (0.968f,0.949f,0.345f,1.0f);
	Color Green = new Color (0,1.0f,0,1.0f);
	Color SmoothGreen = new Color (0.8f, 1.0f, 0.8f, 1.0f);
	Color MidGreen = new Color (0.4f , 1.0f , 0.4f , 1.0f);

	
	/* Connected Sphero Robot */
	Sphero[] m_Spheros;
	
	/* Counter to determine if Sphero should have color or not */
	int m_BlinkCounter=0,Counting=0;
	
	/* Use this for initialization */
	void ViewSetup() {
		// Get Connected Sphero
		m_Spheros = SpheroProvider.GetSharedProvider().GetConnectedSpheros();
		SpheroDeviceMessenger.SharedInstance.NotificationReceived += ReceiveNotificationMessage;
		if( m_Spheros.Length == 0 ) Application.LoadLevel("SpheroConnectionScene");
	}
	
	void Start () {	
		ViewSetup();
	}
	
	/* This is called when the application returns from or enters background */
	void OnApplicationPause(bool pause) {
		if( pause ) {
			SpheroProvider.GetSharedProvider().DisconnectSpheros();
			// Initialize the device messenger which sets up the callback
			SpheroDeviceMessenger.SharedInstance.NotificationReceived -= ReceiveNotificationMessage;
		}
		else {
			ViewSetup();
		}
	}
	
	/* Update is called once per frame need prime number to make it blinking first*/
	void Update () {

		foreach (Sphero sphero in m_Spheros) {
			MovingForward(sphero);
		}
		m_BlinkCounter++;
		Counting++;
		if (Counting < 1000) {
			if (m_BlinkCounter > 0 && m_BlinkCounter < 50) {			
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (SmoothBlue.r, SmoothBlue.g, SmoothBlue.b);
				
				}
			} else if (m_BlinkCounter > 50 && m_BlinkCounter < 100) {
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (MidBlue.r, MidBlue.g, MidBlue.b);
				}
			} else if (m_BlinkCounter > 100 && m_BlinkCounter < 150) {
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (BLUE.r, BLUE.g, BLUE.b);
				}
			}
			if (m_BlinkCounter > 150) {
				m_BlinkCounter = 0;
			}
		} else if (Counting > 1000 && Counting < 2000) {

			if (m_BlinkCounter > 0 && m_BlinkCounter < 50) {			
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (SmoothGreen.r, SmoothGreen.g, SmoothGreen.b);
					
				}
			} else if (m_BlinkCounter > 50 && m_BlinkCounter < 100) {
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (MidGreen.r, MidGreen.g, MidGreen.b);
				}
			} else if (m_BlinkCounter > 100 && m_BlinkCounter < 150) {
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (Green.r, Green.g, Green.b);
				}
			}
			if (m_BlinkCounter > 150) {
				m_BlinkCounter = 0;
			}
		} else if (Counting > 2000) {
			
			if (m_BlinkCounter > 0 && m_BlinkCounter < 50) {			
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (Purple.r, Purple.g, Purple.b);
					
				}
			} else if (m_BlinkCounter > 50 && m_BlinkCounter < 100) {
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (Yellow.r, Yellow.g, Yellow.b);
				}
			} else if (m_BlinkCounter > 100 && m_BlinkCounter < 150) {
				foreach (Sphero sphero in m_Spheros) {
					// Set the Sphero color to purple 
					sphero.SetRGBLED (BLUE.r, BLUE.g, BLUE.b);
					Counting=0;
				}
			}
			if (m_BlinkCounter > 150) {
				m_BlinkCounter = 0;
			}
		}

	}

	/*
	 * Callback to receive connection notifications 
	 */

	void MovingForward(Sphero sphero){
		sphero.Roll (0 ,0.5f);
	}


	void MovingBackward(Sphero sphero){
		sphero.Roll (180, 0.5f);
	}

	void Stop(Sphero sphero){
		sphero.Roll (0,0);
	}

	void MovingRight(Sphero sphero){
		sphero.Roll (90, 0.5f);
	}

	void MovingLeft(Sphero sphero){
		sphero.Roll (270, 0.5f);
	}



	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = SpheroProvider.GetSharedProvider().GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
			Application.LoadLevel("NoSpheroConnectedScene");
		}
	}


	void OnGUI(){
		if(GUI.Button(new Rect(200, 200 ,100 ,100) , "Dance")){

		}
	}
	
}