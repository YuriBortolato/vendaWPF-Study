using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using umfg.venda.app.Abstracts;
using umfg.venda.app.ViewModels;

namespace umfg.venda.app.Commands
{
    internal sealed class FinalizarRecebimentoCommand : AbstractCommand
    {
        public override void Execute(object? parameter)
        {
            var viewModel = parameter as ReceberPedidoViewModel;
            if (viewModel == null)
            {
                MessageBox.Show("Erro de sistema: Não foi possível ler os dados do formulário.");
                return;
            }

            List<string> erros = new List<string>();

            if (viewModel.TipoCartaoSelecionado == 0)
                erros.Add("- Selecione a modalidade do cartão (Crédito ou Débito).");

            if (string.IsNullOrWhiteSpace(viewModel.NomeCartao) || viewModel.NomeCartao.Trim().Length < 3)
                erros.Add("- O Nome no Cartão deve ser preenchido por completo.");

            if (string.IsNullOrWhiteSpace(viewModel.CVV) || !Regex.IsMatch(viewModel.CVV, @"^\d{3}$"))
                erros.Add("- O CVV deve conter exatamente 3 dígitos numéricos.");

            if (string.IsNullOrWhiteSpace(viewModel.DataValidade))
            {
                erros.Add("- A data de validade é obrigatória.");
            }
            else
            {
                if (!Regex.IsMatch(viewModel.DataValidade, @"^(0[1-9]|1[0-2])\/\d{2,4}$"))
                {
                    erros.Add("- Digite a validade no formato correto (ex: 12/26).");
                }
                else
                {
                    DateTime dataValidadeCartao;
                    string formato = viewModel.DataValidade.Length == 5 ? "MM/yy" : "MM/yyyy";

                    if (DateTime.TryParseExact(viewModel.DataValidade, formato, CultureInfo.InvariantCulture, DateTimeStyles.None, out dataValidadeCartao))
                    {
                        var ultimoDiaMesValidade = new DateTime(dataValidadeCartao.Year, dataValidadeCartao.Month, DateTime.DaysInMonth(dataValidadeCartao.Year, dataValidadeCartao.Month));

                        if (ultimoDiaMesValidade.Date < DateTime.Now.Date)
                        {
                            erros.Add("- A data de validade do cartão deve ser superior à data atual.");
                        }
                    }
                    else
                    {
                        erros.Add("- Data de validade inválida.");
                    }
                }
            }

            if (!ValidarCartaoLuhn(viewModel.NumeroCartao))
                erros.Add("- O número do cartão informado é inválido.");

            if (erros.Any())
            {
                MessageBox.Show("Atenção, pagamento recusado! Verifique os problemas:\n\n" + string.Join("\n", erros),
                                "Dados Incorretos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Pagamento aprovado e finalizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.DataContext = new MainWindowViewModel();
            }
        }

        private bool ValidarCartaoLuhn(string numeroDigitado)
        {
            if (string.IsNullOrWhiteSpace(numeroDigitado)) return false;

            string numerosApenas = Regex.Replace(numeroDigitado, "[^0-9]", "");

            if (string.IsNullOrWhiteSpace(numerosApenas) || numerosApenas.Length < 13 || numerosApenas.Length > 19)
                return false;

            int soma = 0;
            bool alternar = false;

            for (int i = numerosApenas.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(numerosApenas[i].ToString());
                if (alternar)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                soma += n;
                alternar = !alternar;
            }

            return (soma % 10 == 0);
        }
    }
}