using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Stride.UI;
using Stride.UI.Controls;

namespace CSInt_01_UI_Basics
{
   public class UIEditorTut : StartupScript
   {

      public TextBlock nameTextBlock;
      public EditText nameEditText;

      public Button nameButton;

      public override void Start()
      {

         UIPage page = Entity.Get<UIComponent>().Page;

         // How to find the UI Controls other than draging and dropping them into the script
         // Note we cannot drag and drop as expected
         nameEditText = page.RootElement.FindName("NameEditText") as EditText;
         nameTextBlock = page.RootElement.FindVisualChildOfType<TextBlock>("NameTextBlock");

         nameEditText.TextChanged += (sender, args) => {
           nameTextBlock.Text = $"My name is {nameEditText.Text}";   

         };

         Button nameButton = page.RootElement.FindVisualChildOfType<Button>("NameButton");

         nameButton.Click += ButtonClickedEvent;





      }

      private void ButtonClickedEvent(object sender, Stride.UI.Events.RoutedEventArgs e)
      {
         nameEditText.Text = "";
         nameTextBlock.Text = "My name is????????";
      }






   }
}
