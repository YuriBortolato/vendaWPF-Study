using System;
using System.Windows.Controls;
using System.Windows.Input;
using umfg.venda.app.Abstracts;
using umfg.venda.app.Commands;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;

namespace umfg.venda.app.ViewModels
{
    internal sealed class ReceberPedidoViewModel : AbstractViewModel
    {
        private PedidoModel _pedido = new();
        private int _tipoCartaoSelecionado = 0;
        private string _numeroCartao = string.Empty;
        private string _cvv = string.Empty;
        private string _dataValidade = string.Empty; 
        private string _nomeCartao = string.Empty;

        public int TipoCartaoSelecionado
        {
            get => _tipoCartaoSelecionado;
            set => SetField(ref _tipoCartaoSelecionado, value);
        }

        public string NumeroCartao
        {
            get => _numeroCartao;
            set => SetField(ref _numeroCartao, value);
        }

        public string CVV
        {
            get => _cvv;
            set => SetField(ref _cvv, value);
        }

        public string DataValidade
        {
            get => _dataValidade;
            set => SetField(ref _dataValidade, value);
        }

        public string NomeCartao
        {
            get => _nomeCartao;
            set => SetField(ref _nomeCartao, value);
        }

        public PedidoModel Pedido
        {
            get => _pedido;
            set => SetField(ref _pedido, value);
        }

        public ICommand FinalizarCommand { get; private set; }

        public ReceberPedidoViewModel(UserControl userControl, IObserver observer, PedidoModel pedido)
            : base("Receber Pedido")
        {
            UserControl = userControl ?? throw new ArgumentNullException(nameof(userControl));
            MainWindow = observer ?? throw new ArgumentNullException(nameof(observer));
            Pedido = pedido ?? throw new ArgumentNullException(nameof(pedido));

            FinalizarCommand = new FinalizarRecebimentoCommand();

            Add(observer);
        }
    }
}