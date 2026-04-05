using System;
using System.Windows.Controls;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.UserControls
{
    public partial class ucReceberPedido : UserControl
    {
        private ucReceberPedido(IObserver observer, PedidoModel pedido)
        {
            InitializeComponent();
            DataContext = new ReceberPedidoViewModel(this, observer, pedido);
        }

        internal static void Exibir(IObserver observer, PedidoModel pedido)
        {
            (new ucReceberPedido(observer, pedido).DataContext as ReceberPedidoViewModel).Notify();
        }
    }
}