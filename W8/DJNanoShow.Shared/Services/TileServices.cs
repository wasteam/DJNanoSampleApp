using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace DJNanoShow.Services
{
    public class TileServices
    {
        public static void CreateFlipTile(string title, string content)
        {
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdater.EnableNotificationQueue(true);
            tileUpdater.Clear();

            var squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText02);
            SetTileImages(squareTileXml, "ms-appx:///Assets/DataImages/FlipSquareTile.png");
            SetTileTexts(squareTileXml, title, content);
            tileUpdater.Update(new TileNotification(squareTileXml));

            var wideTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage01);
            SetTileImages(wideTileXml, "ms-appx:///Assets/DataImages/FlipWideTile.png");
            SetTileTexts(wideTileXml, title, content);
            tileUpdater.Update(new TileNotification(wideTileXml));
        }

        public static void CreateCycleTile(Dictionary<string, string> images)
        {
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdater.EnableNotificationQueue(true);
            tileUpdater.Clear();

            foreach (var image in images)
            {
                var squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
                SetTileImages(squareTileXml, image.Value);
                var wideTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Image);
                SetTileImages(wideTileXml, image.Value);

                var node = wideTileXml.ImportNode(squareTileXml.GetElementsByTagName("binding").Item(0), true);
                wideTileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

                var notif = new TileNotification(wideTileXml);
                notif.Tag = image.Key;
                tileUpdater.Update(notif);
            }
        }

        public static void CreateIconicTile(string content1, string content2, string content3)
        {
            var tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
            tileUpdater.EnableNotificationQueue(true);
            tileUpdater.Clear();

            var squareTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150PeekImageAndText03);
            SetTileImages(squareTileXml, "ms-appx:///Assets/DataImages/IconicSquareTile.png");
            SetTileTexts(squareTileXml, content1, content2, content3);
            tileUpdater.Update(new TileNotification(squareTileXml));

            var wideTileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImageAndText02);
            SetTileImages(wideTileXml, "ms-appx:///Assets/DataImages/IconicWideTile.png");
            SetTileTexts(wideTileXml, content1, content2, content3);
            tileUpdater.Update(new TileNotification(wideTileXml));
        }

        private static void SetTileImages(XmlDocument xmlDocument, params string[] images)
        {
            if (images != null)
            {
                try
                {
                    var imageElements = xmlDocument.GetElementsByTagName("image").ToArray();
                    for (int n = 0; n < images.Length; n++)
                    {
                        var imageElement = imageElements[n] as XmlElement;
                        if (images[n].StartsWith("ms-appx:", StringComparison.OrdinalIgnoreCase) || images[n].StartsWith("ms-appdata:", StringComparison.OrdinalIgnoreCase))
                        {
                            imageElement.SetAttribute("src", images[n]);
                        }
                        else
                        {
                            imageElement.SetAttribute("src", String.Format("ms-appx:///Assets/{0}", images[n]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private static void SetTileTexts(XmlDocument xmlDocument, params string[] texts)
        {
            if (texts != null)
            {
                try
                {
                    var textElements = xmlDocument.GetElementsByTagName("text").ToArray();
                    for (int n = 0; n < texts.Length; n++)
                    {
                        var textElement = textElements[n] as XmlElement;
                        textElement.InnerText = texts[n];
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
