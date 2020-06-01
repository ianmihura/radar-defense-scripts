using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private string _direction;
    private GameObject _parent;

    public void SetArrow(string direction, GameObject parent) {
        _direction = direction;
        _parent = parent;
    }

    public void OnMouseDown() {
        _parent.GetComponent<Defender>().SetDirection(_direction);
        _parent.GetComponent<Defender>().DestroyArrows();
    }

    public void DestroyMe() {
        Destroy(gameObject);
    }

}
