using UnityEngine;
using System.Collections;

public class ScannerBehavior : MonoBehaviour {
   LineRenderer visableScannerLine;
   float mScannerRange = 4;
   float mScanningMultiplier = 1;
   float startWidth = .1f;
   float endWidth = .25f;
	void Start () {
      visableScannerLine = gameObject.AddComponent<LineRenderer>();
      visableScannerLine.enabled = true;
      visableScannerLine.SetColors(Color.green, Color.blue);
      visableScannerLine.SetWidth(startWidth, endWidth);
      visableScannerLine.material = Resources.Load("Scanner Mat") as Material; ;
   }

   // Update is called once per frame
   void Update() {
      if (Input.GetButton("Fire1")) {
         
         visableScannerLine.enabled = true;
         Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         mousePosition.z = 0;
         Vector3 toMouse = mousePosition - transform.position;
         Debug.Log(toMouse);
         Vector3 impactPoint;
         RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, mousePosition - transform.position, mScannerRange);
         if (hitInfo.collider != null) {
            Debug.Log(hitInfo.collider.gameObject.name);
            impactPoint = hitInfo.point;
            hitInfo.collider.gameObject.SendMessage("scanned", mScanningMultiplier, SendMessageOptions.DontRequireReceiver);
         }
         else {
            
            impactPoint = (toMouse.normalized * mScannerRange) + transform.position;
         }
         visableScannerLine.SetPosition(0, transform.position);
         visableScannerLine.SetPosition(1, impactPoint);
      }
      else {
         visableScannerLine.enabled = false;
      }
	}


}
