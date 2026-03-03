using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using umfg.venda.app.Abstracts;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.Commands
{
    internal sealed class RemoverProdutoPedidoCommand : AbstractCommand
    {
        public override bool CanExecute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            //o método Any() indica que a lista possui ao menos um registro
            return vm is not null && vm.Pedido.Produtos.Any();
        }

        public override void Execute(object? parameter)
        {
            var vm = parameter as ListarProdutosViewModel;

            if (vm is null)
            {
                MessageBox.Show("Parâmetro obrigatório não informado! Verifique.");
                return;
            }

            if (vm.ProdutoSelecionado is null || Guid.Empty.Equals(vm.ProdutoSelecionado.Id))
            {
                MessageBox.Show("Nenhum produto selecionado para remover do carrinho!");
                return;
            }

            //testa se o item selecionado pelo usuário consta na lista de itens do pedido
            //utilizando a passagem instrução lambda no método Any()
            if (!m.Pedido.Produtos.Any(x => x.Id == vm.ProdutoSelecionado.Id))
            {
                MessageBox.Show($"{vm.ProdutoSelecionado.Descricao} não foi encontrado no carrinho!");
                return;
            }

            vm.Pedido.Produtos.Remove(vm.ProdutoSelecionado);
            vm.Pedido.Total = vm.Pedido.Produtos.Sum(x => x.Valor);
        }
    }
}
