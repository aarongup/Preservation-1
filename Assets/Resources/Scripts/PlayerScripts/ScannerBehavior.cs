using UnityEngine;
using System.Collections;

public class ScannerBehavior : MonoBehaviour {
   LineRenderer visableScannerLine;
   public float mScannerRange = 4;
   public float mScanningMultiplier = 1;
   public float startWidth = .1f;
   public float endWidth = .25f;
	void Start () {
      visableScannerLine = gameObject.AddComponent<LineRenderer>();
      visableScannerLine.enabled = true;
      visableScannerLine.SetColors(Color.green, Color.blue);
      visableScannerLine.SetWidth(startWidth, endWidth);
      visableScannerLine.material = new Material(Shader.Find("Particles/Additive"));
   }

   // Update is called once per frame
   void Update() {
      if (Input.GetButton("Fire1")) {
         
         visableScannerLine.enabled = true;
         Debug.Log(Camera.main.nearClipPlane);
         //unclear from documentation what exactly the z coordinate should be that ScreenToWorld gets, but this is good enough.
         Vector3 mousePosition = new Vector3(Input.mousePosition.x,Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
         Debug.Log("Mouse Position in Screen" + mousePosition);
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
         mousePosition.z = 0;
         Debug.Log("Mouse Position in World" + mousePosition);
         Vector3 toMouse = mousePosition - transform.position;
         Vector3 impactPoint;
         RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, mousePosition - transform.position, mScannerRange);
         if (hitInfo.collider != null) {
            //Debug.Log(hitInfo.collider.gameObject.name);
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
