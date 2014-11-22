using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleController : MonoBehaviour
{
    public int iRate = 10;
    public bool bActEmisionIni = false;
    public bool bActEmisionEnd = false;
    public Vector3 vScaleMin;
    public Vector3 vScaleMax;
    public float fTimeLife;

    private ParticleSystem partSys;
    private List<ParticleSystem> lPartSysIni = new List<ParticleSystem>();
    private List<ParticleSystem> lPartSysEnd = new List<ParticleSystem>();
    private float lifeTime = 0;


    // Use this for initialization
    void Start()
    {

        ParticleSystem[] aPartSys = this.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pSys in aPartSys)
        {

            if (pSys.tag == "PartSysIni")
            {
                lPartSysIni.Add(pSys);
            }

            if (pSys.tag == "PartSysEnd")
            {
                lPartSysEnd.Add(pSys);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (bActEmisionIni|| bActEmisionEnd)
            lifeTime++;

        //this.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        //this.GetComponentInChildren<ParticleSystem>().emissionRate = 10;
        if (fTimeLife < lifeTime && bActEmisionIni)
        {
            bActEmisionIni = false;
            ControlEmisionAndRate(iRate, bActEmisionIni, lPartSysIni);
            lifeTime = 0;

            bActEmisionEnd = true;
            ControlEmisionAndRate(iRate, bActEmisionEnd, lPartSysEnd);
        }

        if (fTimeLife < lifeTime && bActEmisionEnd)
        {
            lifeTime = 0;
            bActEmisionEnd = false;
            ControlEmisionAndRate(iRate, bActEmisionEnd, lPartSysEnd);
        }

    }

    public void changeRate(GameObject go)
    {
        switch (go.name)
        {
            case "btnIncRate":
                {
                    iRate += 2;
                    break;
                }
            case "btnDecRate":
                {
                    iRate -= 2;
                    break;
                }
            default: break;
        }


        ControlEmisionAndRate(iRate, true, lPartSysIni);
    }

    public void Control2velasnegras()
    {
        bActEmisionIni = !bActEmisionIni;
        ControlEmisionAndRate(iRate, bActEmisionIni, lPartSysIni);

    }

    public void ControlEmisionAndRate(int iRate, bool bActEmision, List<ParticleSystem> aPartSys)
    {

        foreach (ParticleSystem partSys in aPartSys)
        {
            partSys.emissionRate = iRate;
            partSys.enableEmission = bActEmision;


            this.transform.localScale = Vector3.Lerp(vScaleMin, vScaleMax, Time.fixedDeltaTime);


        }

    }



}
