using UnityEngine;

/// <summary>
/// Disables the parent gameobject when it is clicked.
/// Attach the public method to the OnClick binder of
/// the button or other piece of clickable UI.
/// </summary>
public class DisableOnClick : MonoBehaviour
{
  public void DisableObjectOnClick()
  {
    gameObject.SetActive(false);
  }
}
