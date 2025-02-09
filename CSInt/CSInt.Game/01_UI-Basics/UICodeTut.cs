using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.Graphics;
using Stride.UI.Controls;
using Stride.UI;
using Stride.UI.Panels;

namespace CSInt_01_UI_Basics
{
   public class UICodeTut : SyncScript
   {

      public SpriteFont font;
      private Button button;
      private TextBlock textBlock;

      public override void Start()
      {

         font = Content.Load<SpriteFont>("UI/OpenSans-font");

         button = CreateNewButton();

         textBlock = CreateNewTextBlock("...", Color.Blue, new Color(255,0,0));

         var myUi = Entity.GetOrCreate<UIComponent>();

         var myUiPage = new UIPage();

         var myUiStackPanel = new StackPanel();
         myUiStackPanel.Width = 600;
         myUiStackPanel.Height = 200;
         myUiStackPanel.Orientation = Orientation.Vertical;
         myUiStackPanel.Margin = new Thickness(0, 0, 10, 10);
         myUiStackPanel.BackgroundColor = Color.LimeGreen;
         myUiStackPanel.Children.Add(button);
         myUiStackPanel.Children.Add(textBlock);
         myUiStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
         myUiStackPanel.VerticalAlignment = VerticalAlignment.Bottom;

         myUiPage.RootElement = myUiStackPanel;

         myUi.Page = myUiPage;



      }

      public override void Update()
      {
      }

      private Button CreateNewButton()
      {
         Button buttonNew = new Button();
         buttonNew.Name = "MyButtonByCode";
         buttonNew.HorizontalAlignment = HorizontalAlignment.Center;
         buttonNew.BackgroundColor = Color.DarkCyan;
         buttonNew.Content = CreateNewTextBlock("Show me the Time", Color.Yellow, Color.Red);

         buttonNew.Click += ShowMeTheTime;


         return buttonNew;
         
      }


      private TextBlock CreateNewTextBlock(string text, Color textColor, Color backgroundColor)
      {
         TextBlock textBlockNew = new TextBlock();

         textBlockNew.Text = text;
         textBlockNew.TextColor = textColor;
         textBlockNew.BackgroundColor = backgroundColor;
         textBlockNew.Font = font;

         textBlockNew.HorizontalAlignment = HorizontalAlignment.Center;

         return textBlockNew;
      }


      private void ShowMeTheTime(object sender, Stride.UI.Events.RoutedEventArgs e)
      {
         textBlock.Text = DateTime.Now.ToString();
      }






   }
}
