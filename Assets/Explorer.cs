using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public Material material;
    public Vector2 position;
    public float scale,angle;
    private Vector2 smoothePosition;
    private float smootheScale,smoothAngle;
    
    void UpdateShader(){
        smoothePosition = Vector2.Lerp(smoothePosition,position,.03f);
        smootheScale = Mathf.Lerp(smootheScale,scale,.03f);
        smoothAngle = Mathf.Lerp(smoothAngle,angle,.03f);

        float aspect = (float)Screen.width / (float)Screen.height;
        
        float scaleX = smootheScale;
        float scaleY = smootheScale;

        if(aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;
        
        material.SetVector("_Area",new Vector4(smoothePosition.x,smoothePosition.y,scaleX,scaleY));
        material.SetFloat("_Angle",smoothAngle);
    }

    void FixedUpdate()
    {
        HandleInput();
        UpdateShader();
    }

    void HandleInput()
    {
        if(Input.GetKey(KeyCode.Space)){
            scale *= 0.99f;
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            scale *= 1.01f;
        }

        if(Input.GetKey(KeyCode.Q)){
            angle += .01f;
        }
        if(Input.GetKey(KeyCode.E)){
            angle -= .01f;
        }

        Vector2 dir = new Vector2(.01f*scale,0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x*c, dir.x*s);

        if(Input.GetKey(KeyCode.A)){
            position -= dir;
        }
        if(Input.GetKey(KeyCode.D)){
            position += dir;
        }
        
        dir = new Vector2(-dir.y,dir.x);

        if(Input.GetKey(KeyCode.W)){
            position += dir;
        }
        if(Input.GetKey(KeyCode.S)){
            position -= dir;
        }
        
    }
}
