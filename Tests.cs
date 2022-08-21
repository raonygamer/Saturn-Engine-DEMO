using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Xml;
using SimpleSpriteAnimator;
using System.Drawing;
using Saturn.Utils;
using UnityEngine.UI;

public class Tests : MonoBehaviour
{
    
    public SpriteAnimator animator;
    public RawImage img;

    void Start()
    {
        
    }

    public class XMLFrame
    {
        public string name;
        public int x;
        public int y;
        public int width;
        public int height;
        public int frameX;
        public int frameY;
        public int frameWidth;
        public int frameHeight;

        public int dstx1;
        public int dsty1;
        public int dstx2;
        public int dsty2;

        public int srcx1;
        public int srcy1;
        public int srcx2;
        public int srcy2;

        public XMLFrame(XmlNode node)
        {
            try
            {
                name = node.Attributes["name"].Value;
                x = Convert.ToInt32(node.Attributes["x"].Value);
                y = Convert.ToInt32(node.Attributes["y"].Value) - (Convert.ToInt32(node.Attributes["y"].Value) - Convert.ToInt32(node.Attributes["height"].Value));
                width = Convert.ToInt32(node.Attributes["width"].Value);
                height = Convert.ToInt32(node.Attributes["height"].Value);

                try
                {
                    frameX = Convert.ToInt32(node.Attributes["frameX"].Value);
                }
                catch
                {
                    frameX = 0;
                }

                try
                {
                    frameY = Convert.ToInt32(node.Attributes["frameY"].Value);
                }
                catch
                {
                    frameY = 0;
                }

                try
                {
                    frameWidth = Convert.ToInt32(node.Attributes["frameWidth"].Value);
                }
                catch
                {
                    frameWidth = width;
                }

                try
                {
                    frameHeight = Convert.ToInt32(node.Attributes["frameHeight"].Value);
                }
                catch
                {
                    frameHeight = height;
                }


                dstx1 = -frameX;
                dsty1 = -frameY;
                dstx2 = -frameX + width;
                dsty2 = -frameY + height;

                srcx1 = x;
                srcx2 = x + width;
                srcy1 = y;
                srcy2 = y + height;
            }
            catch
            {

            }
        }
    }
}
