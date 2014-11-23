using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PowerUp_Mushroom : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        int Cure = 0;
                System.Random ran = new System.Random();
        if (col.name == Defines.Player)
        {
            Creature Player = GameManager.Instance.Player;

            switch (ran.Next(0, 2))
            {
                case 0:
                    {
                        System.Random r = new System.Random();
                        Cure = r.Next(40,60)/100 * Player.GetComponent<Life>().life.value;
                        Player.GetComponent<Life>().OnHeal(Cure);
                        break;
                    }
                case 1:
                    {
                        System.Random r = new System.Random();
                        Cure = r.Next(10, 25) / 100 * Player.GetComponent<Life>().life.value;
                        Player.GetComponent<Life>().OnDamage(Cure);
                        //Todo
                        // en lugar de partículas spawmeamos un enemigo
                        //instanciamos las partículas de explosión
                        ParticleSFX _particleFire = GameManager.Instance.Particles.FirstOrDefault(x => x.name == Defines.ParticleDustExplosion);
                        break;
                    }
                case 2:
                    {
                        //instanciamos las partículas de explosión
                        ParticleSFX _particleFire = GameManager.Instance.Particles.FirstOrDefault(x => x.name == Defines.ParticleDeath);
                        break;
                    }
                default: break;

            }
            

            Destroy(this.gameObject);
        }
    }
}
