# Unity UI Info/Tricks

## PixelsPerUnit
In Orthographic mode you can calculate a position based on the screen resolution and the cameras ortho size.

`pixelsPerUnit = Screen.height / (Camera.main.orthographicSize * 2);`

You can then use this to position things like UI at exact locations on the screen in units:

`GameObject.Find("A1Region").GetComponent<BoxCollider>().size = new Vector3((Screen.width * 0.5F) / pixelsPerUnit, 1, (Screen.height * 0.45F) / pixelsPerUnit);`
`player1Object.transform.position = new Vector3(-(Screen.width * 0.25F) / pixelsPerUnit, 1.0F, 0);`
`randomPosition = new Vector2(Random.Range(-(Screen.width-100)/ pixelsPerUnit/2, (Screen.width - 100) / pixelsPerUnit / 2), Random.Range(50/pixelsPerUnit,((Screen.height*0.9F-50) / pixelsPerUnit)));`


## UI Canvas/Panel sizing
I'm not sure I've really figured this out, but so far have been doing things using percentages of screen size:

`GameObject.Find("Player1ScorePanel").GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height * 0.10F);`
`GameObject.Find("Player1Score").GetComponent<Text>().fontSize = (int)(Screen.height * 0.08F);`

