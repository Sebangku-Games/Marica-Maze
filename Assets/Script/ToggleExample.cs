using UnityEngine;
using UnityEngine.UI;

public class ToggleExample : MonoBehaviour
{
    public GameObject offObject;
    public GameObject onObject;

    private Toggle toggle;
    private GameObject currentObject;

    public AudioManager audios;

    private void Start()
    {
        currentObject = onObject;
        toggle = GetComponent<Toggle>();

        // Set the initial toggle state based on the GameData value
        toggle.isOn = GetToggleValue();

        // Manually trigger the behavior associated with the toggle state
        OnToggleValueChanged(toggle.isOn);

        // Subscribe to the toggle value change event
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void SetToggleState(bool isOn)
    {
        if (currentObject != null)
        {
            currentObject.SetActive(false);
        }

        if (isOn)
        {
            onObject.SetActive(true);
            currentObject = onObject;
        }
        else
        {
            offObject.SetActive(true);
            currentObject = offObject;
        }
    }

    private void OnToggleValueChanged(bool isOn)
    {
        SetToggleState(isOn);

        if (toggle == null || GameData.InstanceData == null)
            return;

        // Update the GameData.instance variable and save the value in PlayerPrefs
        if (gameObject.name == "MusicToggle")
        {
            if (audios != null)
                audios.soundSettings(isOn);
            GameData.InstanceData.onSound = isOn;
            PlayerPrefs.SetInt("onMusic", isOn ? 1 : 0);
        }
        else if (gameObject.name == "SoundToggle")
        {
            GameData.InstanceData.onSound = isOn;
            PlayerPrefs.SetInt("onSound", isOn ? 1 : 0);
        }

    }

    private bool GetToggleValue()
    {
        if (toggle == null || GameData.InstanceData == null)
            return false;

        if (gameObject.name == "MusicToggle")
        {
            print(GameData.InstanceData.onSound);
            return GameData.InstanceData.onSound;
        }
        else if (gameObject.name == "SoundToggle")
        {
            return GameData.InstanceData.onSound;
        }

        return false;
    }

}
