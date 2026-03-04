using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using umfg.venda.app.Abstracts;
using umfg.venda.app.Commands;
using umfg.venda.app.Interfaces;
using umfg.venda.app.Models;

namespace umfg.venda.app.ViewModels
{
    internal sealed class ListarProdutosViewModel : AbstractViewModel
    {
        private ProdutoModel _produtoSelecionado = new();
        private ObservableCollection<ProdutoModel> _produtos = [];
        private PedidoModel _pedido = new();

        public ProdutoModel ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set => SetField(ref _produtoSelecionado, value);
        }

        public ObservableCollection<ProdutoModel> Produtos
        {
            get => _produtos;
            set => SetField(ref _produtos, value);
        }

        public PedidoModel Pedido
        {
            get => _pedido;
            set => SetField(ref _pedido, value);
        }

        public AdicionarProdutoPedidoCommand Adicionar { get; private set; } = new();
        public RemoverProdutoPedidoCommand Remover { get; private set; } = new();
        public ReceberPedidoCommand Receber { get; private set; } = new();

        public ListarProdutosViewModel(IObserver observer, UserControl userControl) : base("Produtos")
        {
            UserControl = userControl;
            MainWindow = observer;

            Add(observer);
            CarregarProdutos();
        }

        public void RaiseCanExecuteChanged()
        {
            Adicionar.RaiseCanExecuteChanged();
            Remover.RaiseCanExecuteChanged();
            //Receber.RaiseCanExecuteChanged();
        }

        private void CarregarProdutos()
        {
            Produtos.Clear();

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows7.0\Images\batata.png", UriKind.Relative)),
                Descricao = "Batata Frita 300gr",
                Referencia = "0001",
                Valor = 10.90m,
            });

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows7.0\Images\combo.png", UriKind.Relative)),
                Descricao = "Combo Big Mac + Batata 300gr + Refil 500ML",
                Referencia = "0002",
                Valor = 49.90m,
            });

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows7.0\Images\lanche.png", UriKind.Relative)),
                Descricao = "Big Mac 350gr",
                Referencia = "0003",
                Valor = 25.90m,
            });

            Produtos.Add(new ProdutoModel()
            {
                Imagem = new BitmapImage(
                    new Uri(@"..\net8.0-windows7.0\Images\refrigerante.png", UriKind.Relative)),
                Descricao = "Regrigerante Refil 500ml",
                Referencia = "0004",
                Valor = 10.90m,
            });
        }
    }
}
