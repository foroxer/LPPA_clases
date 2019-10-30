using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Horarios
{
    public class ListItemTemplate : GridViewItem
    {
        private Boolean innerContentDisplayed = false;
        private TextBlock contenido = null;

        public ListItemTemplate(String hora, String texto, String textoInterno)
        {
            StackPanel grid = new StackPanel();
            grid.Width = 350;
            grid.Height = 50;
            grid.Orientation = Orientation.Horizontal;
            grid.Children.Add(this.createTextBlock(hora, 0d));
            grid.Children.Add(this.createTextBlock(texto, 100d));
            contenido = (this.createInnerText(textoInterno));
            grid.Children.Add(contenido);
            grid.Tapped += clickeado;
            this.Content = grid;
        }

        private void clickeado(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            innerContentDisplayed = !innerContentDisplayed;
            (contenido).Visibility = innerContentDisplayed ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            ((StackPanel)this.Content).UpdateLayout();
        }

        private TextBlock createTextBlock(String text, double left)
        {
            TextBlock tb = new TextBlock();
            tb.Text = text;
            tb.Width = 100;
            tb.Height = 100;
            tb.FontSize = 20;
            tb.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
            tb.UseLayoutRounding = true;
            tb.Margin = new Windows.UI.Xaml.Thickness(left, 0d, 0d, 0d);
  
            return tb;
        }

        private TextBlock createInnerText(String innerText)
        {
            TextBlock tb = new TextBlock();
            tb.Text = innerText;
            tb.Width = 100;
            tb.Height = 100;
            tb.FontSize = 20;
            tb.TextAlignment = Windows.UI.Xaml.TextAlignment.Left;
            tb.UseLayoutRounding = true;
            tb.Margin = new Windows.UI.Xaml.Thickness(0d, 50d, 0d, 0d);
            tb.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            return tb;
        }
        
    }
}
