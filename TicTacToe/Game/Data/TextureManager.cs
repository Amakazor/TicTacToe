using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using TicTacToe.Utility.Exceptions;

namespace TicTacToe.Game.Data
{
    public static class TextureType
    {
        public const string Symbol = "symbol";
        public const string Background = "background";
        public const string Icon = "icon";
        public const string Field = "field";
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

            XDocument textureConfig;

            try
            {
                textureConfig = XDocument.Load("assets/data/textures.xml");
            }
            catch (Exception)
            {
                throw new FileMissingOrCorruptedException("File textures.xml couldn't be loaded");
            }

            foreach (FieldInfo fieldInfo in typeof(TextureType).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                try
                {
                    string textureType = fieldInfo.GetValue(null).ToString();
                    TexturesDictionary[textureType] = (from texture in textureConfig.Descendants("texture")
                                                       where texture.Element("type").Value == textureType
                                                       select new
                                                       {
                                                           symbol = texture.Element("name").Value,
                                                           location = texture.Element("location").Value
                                                       }).ToDictionary(o => o.symbol, o => new Texture(o.location) { Smooth = true });
                }
                catch (Exception)
                {
                    throw new FileMissingOrCorruptedException("Texture file couldn't be loaded");
                }
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

                throw new ArgumentException("Could not find given texture", "texture");
            }
            else throw new ArgumentException("There are no textures of this type", "textureType");
        }
    }
}