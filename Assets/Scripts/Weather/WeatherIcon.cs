using UnityEngine;
using UnityEngine.UI;

public class WeatherIcon : MonoBehaviour, IEventHandler<UIClickEvent>
{

    public Weather weatherType;
    private GamingDataController gamingDataController;

    private void Start()
    {
        gamingDataController = GamingDataController.GetInstance();
    }

    private void OnEnable()
    {
        EventBus.register<UIClickEvent>(this);
    }

    private void OnDisable()
    {
        EventBus.unregister<UIClickEvent>(this);
    }

    public void HandleEvent(UIClickEvent eventData)
    {
        if ((eventData.obj == gameObject) && gamingDataController.IsFullEnergy())
        {
            WeatherSystem.SetWeather(weatherType);
            // Cost all the energy when change the weather.
            GamingDataController.GetInstance().EmptyEnergy();

            Debug.Log(gameObject);
            Debug.Log(gameObject.name);
            Debug.Log(gameObject.GetComponent<Button>().name);
        }
    }
}
