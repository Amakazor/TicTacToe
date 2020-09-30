using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace TicTacToe.Game.Data
{
    public enum TextureType
    {
        Symbol,
        Background,
        Empty
    }

    public class Textures
    {
        public Dictionary<TextureType, Dictionary<string, Texture>> TexturesDictionary { get; private set; }

        public Textures()
        {
            TexturesDictionary = new Dictionary<TextureType, Dictionary<string, Texture>>();

            //Dictionary<string, Texture> symbolDictionary = new Dictionary<string, Texture>();
            XmlDocument textureConfig = new XmlDocument();
            textureConfig.Load("assets/data/textures.xml");

            if (textureConfig.DocumentElement.ChildNodes.Count == 0)
            {
                //TODO: add exception;
                throw new Exception("");
            }
            else
            {
                foreach (XmlNode textureData in textureConfig.DocumentElement.ChildNodes)
                {
                    if (textureData.Attributes.GetNamedItem("type") is null || textureData.Attributes.GetNamedItem("location") is null || textureData.Attributes.GetNamedItem("name") is null)
                    {
                        //TODO: add exception;
                        throw new Exception("");
                    }
                    else
                    {
                        Texture texture = new Texture(textureData.Attributes.GetNamedItem("location").Value);
                        texture.Smooth = true;

                        TextureType textureType;

                        switch (textureData.Attributes.GetNamedItem("type").Value)
                        {
                            case "symbol":
                                textureType = TextureType.Symbol;
                                break;
                            case "background":
                                textureType = TextureType.Background;
                                break;
                            default:
                                textureType = TextureType.Empty;
                                break;
                        }

                        if (textureType != TextureType.Empty)
                        {
                            if (!TexturesDictionary.ContainsKey(textureType))
                            {
                                TexturesDictionary.Add(textureType, new Dictionary<string, Texture>());
                            }
                            TexturesDictionary[textureType].Add(textureData.Attributes.GetNamedItem("name").Value, texture);
                        }
                    }
                }
            }
        }

        public string GetNameFromTexture(TextureType textureType, Texture texture)
        {
            if (TexturesDictionary.ContainsKey(textureType))
            {
                foreach (KeyValuePair<string, Texture> StringTexturePair in TexturesDictionary[textureType])
                {
                    if (StringTexturePair.Value == texture)
                    {
                        return StringTexturePair.Key;
                    }
                }

                throw new Exception();
            }
            else throw new Exception();
        }
    }
}