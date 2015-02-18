using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleController : MonoBehaviour
{
    public Vector3 vScaleMin;
    public Vector3 vScaleMax;
    public float fTimeLife;

    public TweenScale tween;

    private List<ParticleSystem> lPartSysIni = new List<ParticleSystem>();
    private List<ParticleSystem> lPartSysEnd = new List<ParticleSystem>();
    private float lifeTime = 0;

    private bool bActEmisionEnd = false;

    private float[] basicIniRate;
    private float[] basicEndRate;

    // Use this for initialization
    void Start()
    {

        ParticleSystem[] aPartSys = this.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem pSys in aPartSys)
        {

            if (pSys.tag == "PartSysIni")
            {
                lPartSysIni.Add(pSys);
                pSys.Play();
            }

            if (pSys.tag == "PartSysEnd")
            {
                lPartSysEnd.Add(pSys);
            }
        }

        basicIniRate = new float[lPartSysIni.Count];
        for (int i = 0; i < lPartSysIni.Count; ++i)
        {
            basicIniRate[i] = lPartSysIni[i].emissionRate;
        }
        basicEndRate = new float[lPartSysEnd.Count];
        for (int i = 0; i < lPartSysEnd.Count; ++i)
        {
            basicEndRate[i] = lPartSysEnd[i].emissionRate;
        }

        tween.from = vScaleMin;
        tween.to = vScaleMax;
        tween.duration = fTimeLife;

        tween.enabled = true;
    }

    void Update()
    {
        for (int i = 0; i < lPartSysIni.Count; ++i)
        {
            lPartSysIni[i].emissionRate = basicIniRate[i] * transform.localScale.x;
        }


        for (int i = 0; i < lPartSysEnd.Count; ++i)
        {
            lPartSysEnd[i].emissionRate = basicEndRate[i] * transform.localScale.x;
        }
    }

    public void Ended()
    {
        if (bActEmisionEnd)
        {
            Destroy(gameObject);
            return;
        }

        Invoke("ReLaunchTween", 1f);
    }

    void ReLaunchTween()
    {
        tween.from = vScaleMax;
        tween.to = vScaleMin;
        tween.duration = fTimeLife;

        tween.ResetToBeginning();

        tween.PlayForward();

        foreach (ParticleSystem pSys in lPartSysIni)
            pSys.Stop();

        foreach (ParticleSystem pSys in lPartSysEnd)
            pSys.Play();

        bActEmisionEnd = true;
    }

}
