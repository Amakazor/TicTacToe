using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Xml;

namespace TicTacToe.Game
{
    public class Textures
    {
        public Dictionary<string, Texture> TexturesDictionary { get; private set; }

        public Textures()
        {
            TexturesDictionary = new Dictionary<string, Texture>();

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
                    if (textureData.Attributes.GetNamedItem("location") is null || textureData.Attributes.GetNamedItem("symbol") is null)
                    {
                        //TODO: add exception;
                        throw new Exception("");
                    }
                    else
                    {
                        Texture texture = new Texture(textureData.Attributes.GetNamedItem("location").Value);
                        texture.Smooth = true;
                        TexturesDictionary.Add(textureData.Attributes.GetNamedItem("symbol").Value, texture);
                    }
                }
            }
        }
    }
}