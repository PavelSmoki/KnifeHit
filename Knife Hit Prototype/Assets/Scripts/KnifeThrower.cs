using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KnifeThrower : MonoBehaviour
{
    [SerializeField] private GameObject _knifePrefab;
    [SerializeField] private float _forcePower;
    [SerializeField] private List<GameObject> _knifes;
    [SerializeField] private int _knifesToWin;
    [SerializeField] private Canvas _winCanvas;
    [SerializeField] private Canvas _loseCanvas;
    private GameObject _knife;
    private int _knifesLeft;
    private bool _gameOn = true;

    private void Start()
    {
        CreateKnife();
        Time.timeScale = 1;
        _knifesLeft = _knifes.Count - 1;
    }

    private void Update()
    {
        if (_knifesToWin == 0)
        {
            _winCanvas.gameObject.SetActive(true);
            _gameOn = false;
            Time.timeScale = 0;
        }

        if (_knifesLeft == -1)
        {
            _gameOn = false;
        }
        
        if (Input.GetMouseButtonDown(0) && _gameOn)
        {
            _knife.transform.parent = null;
            _knife.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _forcePower, ForceMode2D.Impulse);
            StartCoroutine(nameof(AddKnife));
            Destroy(_knifes[_knifesLeft]);
            _knifesLeft--;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    
    private void CreateKnife()
    {
        _knife = Instantiate(_knifePrefab, transform.position, Quaternion.identity);
        _knife.GetComponent<Knife>().Setup(OnKnifeHit);
    }
    
    private void OnKnifeHit()
    {
        _knifesToWin--;
        Debug.Log("Left: " + _knifesToWin);
    }

    private IEnumerator AddKnife()
    {
        yield return new WaitForSeconds(0.5f);
        if (_knifesLeft == -1)
        {
            StartCoroutine(nameof(Lose));
        } else CreateKnife();
    }

    private IEnumerator Lose()
    {
        yield return new WaitForSeconds(1);
        _loseCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}