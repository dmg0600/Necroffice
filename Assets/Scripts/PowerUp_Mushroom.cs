using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PowerUp_Mushroom : MonoBehaviour
{
    public enum Option { Health = 0, Explosion = 1, Fairy = 2 };

    public Option opcion;

    void OnTriggerEnter(Collider col)
    {
        int Cure = 0;

        if (col.name == Defines.Player)
        {
            Creature Player = GameManager.Instance.Player;

            switch (opcion)
            {
                case Option.Health:
                    {
                        System.Random r = new System.Random();
                        Cure = r.Next(40, 60) / 100 * Player.GetComponent<Life>().life.value;
                        Player.GetComponent<Life>().OnHeal(Cure);
                        break;
                    }
                case Option.Explosion:
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
                case Option.Fairy:
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
