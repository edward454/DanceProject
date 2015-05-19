using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dance : MonoBehaviour {
	
	Color BLUE = new Color(0,0,1.0f,1.0f);
	Color BLACK = new Color(0,0,0,1.0f);
	Color GREEN = new Color (0, 1.0f, 0, 1.0f);
	Color Purple = new Color (0.717f, 0.301f, 0.764f, 1.0f);
	
	/* Connected Sphero Robot */
	Sphero[] m_Spheros;
	
	/* Counter to determine if Sphero should have color or not */
	int m_BlinkCounter;
	
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
	
	/* Update is called once per frame */
	void Update () {
		m_BlinkCounter++;
		if (m_BlinkCounter % 20 == 0) {			
			foreach (Sphero sphero in m_Spheros) {
				// Set the Sphero color to blue 
				if (sphero.RGBLEDColor.Equals (BLACK)) {
					sphero.SetRGBLED (Purple.r, Purple.g, Purple.b);

				} else {
					sphero.SetRGBLED (BLACK.r, BLACK.g, BLACK.b);	
				}
			}
		} else if (m_BlinkCounter % 50 == 0) {
			foreach(Sphero sphero in m_Spheros){
					if(sphero.RGBLEDColor.Equals(BLACK)){
						sphero.SetRGBLED(GREEN.r,GREEN.g,GREEN.b);
					}
			}
		}else if (m_BlinkCounter % 70 == 0) {
			foreach(Sphero sphero in m_Spheros){
				if(sphero.RGBLEDColor.Equals(BLACK)){
					sphero.SetRGBLED(BLUE.r,BLUE.g,BLUE.b);
				}
			}
		}
	}

	//making it jumping 
	void Jumping(){

	}
	
	/*
	 * Callback to receive connection notifications 
	 */
	private void ReceiveNotificationMessage(object sender, SpheroDeviceMessenger.MessengerEventArgs eventArgs)
	{
		SpheroDeviceNotification message = (SpheroDeviceNotification)eventArgs.Message;
		Sphero notifiedSphero = SpheroProvider.GetSharedProvider().GetSphero(message.RobotID);
		if( message.NotificationType == SpheroDeviceNotification.SpheroNotificationType.DISCONNECTED ) {
			notifiedSphero.ConnectionState = Sphero.Connection_State.Disconnected;
			Application.LoadLevel("NoSpheroConnectedScene");
		}
	}
}