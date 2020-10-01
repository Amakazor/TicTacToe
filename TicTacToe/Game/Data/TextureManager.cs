using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;

namespace TicTacToe.Game.Data
{
    public static class TextureType
    {
        public const string Symbol = "symbol";
        public const string Background = "background";
    }

    public class TextureManager
    {
        public Dictionary<string, Dictionary<string, Texture>> TexturesDictionary { get; private set; }

        public TextureManager()
        {
            LoadTextures();
        }

        private void LoadTextures()
        {
            TexturesDictionary = new Dictionary<string, Dictionary<string, Texture>>();
            
            XDocument textureConfig = XDocument.Load("assets/data/textures.xml");

            foreach (FieldInfo fieldInfo in typeof(TextureType).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                string textureType = fieldInfo.GetValue(null).ToString();
                TexturesDictionary[textureType] = (from texture in textureConfig.Descendants("texture")
                                                   where texture.Element("type").Value == textureType
                                                   select new {
                                                       symbol = texture.Element("name").Value,
                                                       location = texture.Element("location").Value
                                                   }).ToDictionary(o => o.symbol, o => new Texture(o.location));
            }

        }

        public string GetNameFromTexture(string textureType, Texture texture)
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