using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.UserControls
{
    public partial class ucReceberPedido : UserControl
    {
        private ucReceberPedido(IObserver observer, PedidoModel pedido)
        {
            CultureInfo customCulture = new CultureInfo("pt-BR");
            customCulture.DateTimeFormat.ShortDatePattern = "MM/yyyy";
            Thread.CurrentThread.CurrentCulture = customCulture;
            Thread.CurrentThread.CurrentUICulture = customCulture;

            InitializeComponent();

            Language = XmlLanguage.GetLanguage(customCulture.IetfLanguageTag);

            DataContext = new ReceberPedidoViewModel(this, observer, pedido);
        }

        internal static void Exibir(IObserver observer, PedidoModel pedido)
        {
            (new ucReceberPedido(observer, pedido).DataContext as ReceberPedidoViewModel).Notify();
        }

        private void DatePicker_CalendarOpened(object sender, RoutedEventArgs e)
        {
            var datepicker = sender as DatePicker;
            if (datepicker != null)
            {
                var popup = (Popup)datepicker.Template.FindName("PART_Popup", datepicker);

                if (popup != null && popup.Child is System.Windows.Controls.Calendar calendar)
                {
                    calendar.DisplayModeChanged -= Calendar_DisplayModeChanged;
                    calendar.DisplayModeChanged += Calendar_DisplayModeChanged;

                    calendar.DisplayMode = CalendarMode.Year;
                }
            }
        }

        private void Calendar_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            var calendar = sender as System.Windows.Controls.Calendar;
            if (calendar != null && calendar.DisplayMode == CalendarMode.Month)
            {
                calendar.DisplayModeChanged -= Calendar_DisplayModeChanged;

                calendar.SelectedDate = new DateTime(calendar.DisplayDate.Year, calendar.DisplayDate.Month, 1);

                dpValidade.IsDropDownOpen = false;
                calendar.DisplayMode = CalendarMode.Year;
            }
        }
    }
}