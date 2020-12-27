
public class healthSystem
{
    public int healthMax = 100;
    int health;

    public healthSystem(int max){
        this.healthMax = max;
        health = max;
    }
    public void changeMax(int changeHealthMax){
        health += changeHealthMax;
        healthMax += changeHealthMax;
    }
    public int GetHealth(){
        return health;
    }
    public int GetHealthMax(){
        return healthMax;
    }

    public float GetHealthPercent(){
        return (float) health / healthMax;
    }
    
    public void Damage(int damageAmount){
        health -= damageAmount;
        if(health < 0){
            health = 0;
        }
    }

    public void Heal(int healAmount){
        health += healAmount;
        if (health > healthMax){
            health = healthMax;
        }
    }

    public void Reset(){
        health = healthMax;
    }
}
