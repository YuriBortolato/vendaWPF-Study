using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using umfg.venda.app.Abstracts;
using umfg.venda.app.UserControls;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.Commands
{
    internal sealed class ReceberPedidoCommand : AbstractCommand
    {
        public override bool CanExecute(object? parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            if (vm is null)
            {
                MessageBox.Show("Parâmetro obrigatório não informado! Verifique.");
                return;
            }

            if (vm.Pedido.Produtos == null || vm.Pedido.Produtos.Count == 0)
            {
                MessageBox.Show("Adicione ao menos um item no pedido antes de prosseguir para o pagamento.", "Carrinho Vazio", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ucReceberPedido.Exibir(vm.MainWindow, vm.Pedido);
        }
    }
}