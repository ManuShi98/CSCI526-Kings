using UnityEngine;

public class ActivateDialog : MonoBehaviour, IEventHandler<EnemyChangeEvent>
{
    public GameObject dialogBox;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.register<EnemyChangeEvent>(this);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("curDiedMonster: " + Singleton.Instance.curDiedMonster + " reach: " + Singleton.Instance.curReachEndMonster + " curOriginal: " + Singleton.Instance.curOriginalMonster);
        if ((Singleton.Instance.curDiedMonster == Singleton.Instance.curOriginalMonster))
            {
                Singleton.Instance.curDiedMonster = 0;
                Singleton.Instance.curMonsterNum = 0;
                Singleton.Instance.curOriginalMonster = 0;
                Singleton.Instance.curReachEndMonster = 0;
                dialogBox.SetActive(true);
            }
    }

    void OnDestroy()
    {
        EventBus.unregister<EnemyChangeEvent>(this);
    
    }


    public void HandleEvent(EnemyChangeEvent eventData)
    {
        if (eventData.isArrived)
        {
            Singleton.Instance.curReachEndMonster++;
        }

        if (eventData.isDead)
        {
            Singleton.Instance.curDiedMonster++;
        }
    }
}
