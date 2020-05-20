# Using Shaders in Unity

## HDRP Shaders

`// Set HDRP Shader Lit Base Map`
`go.GetComponent<MeshRenderer>().material.SetTexture("_BaseColorMap", myTextures[index]);`

One way to find these is to look at the shader in Debug mode in the inspector and you can find the names, but that isn't always a clear 1-1 since the formatting of the tree changes.  Info found on: https://answers.unity.com/questions/1501883/what-are-the-key-names-of-the-textures-on-the-hd-s.html

