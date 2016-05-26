using UnityEngine;
using System.Collections;

public class ScannerBehavior : MonoBehaviour {
   LineRenderer visableScannerLine;
   public float mScannerRange = 4;
   public float mScanningMultiplier = 1;
   public float startWidth = .1f;
   public float endWidth = .25f;
   int scannerMask;
   void Start() {
      visableScannerLine = gameObject.AddComponent<LineRenderer>();
      visableScannerLine.enabled = true;
		Color32 myRed = new Color32 (0, 51, 204, 255);
		visableScannerLine.SetColors(Color.black, myRed);
      visableScannerLine.SetWidth(startWidth, endWidth);
      visableScannerLine.material = new Material(Shader.Find("Particles/Additive"));
      scannerMask = Physics2D.DefaultRaycastLayers & ~(LayerMask.GetMask("Player")); //fancy not symbol !!
   }

   // Update is called once per frame
   void Update() {
      if (Input.GetButton("Fire1")) {

         visableScannerLine.enabled = true;
         //unclear from documentation what exactly the z coordinate should be that ScreenToWorld gets, but this is good enough.
         Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z);
         //Debug.Log("Mouse Position in Screen" + mousePosition);
         mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
         mousePosition.z = transform.position.z;
         //Debug.Log("Mouse Position in World" + mousePosition);
         Vector3 toMouse = mousePosition - transform.position;
         Vector3 impactPoint;
         RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, mousePosition - transform.position, mScannerRange, scannerMask);
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