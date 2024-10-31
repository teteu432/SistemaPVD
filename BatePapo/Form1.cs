using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace BatePapo
{
    public partial class Form1 : Form
    {
        private HubConnection connection;


        public Form1()
        {
            InitializeComponent(); // Inicializa os componentes da interface gráfica do Windows Forms

            // Configuração da conexão com o servidor SignalR, especificando a URL
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub") // Define a URL do servidor SignalR
                .Build(); // Constrói a conexão

            // Configuração de um método para escutar mensagens recebidas do servidor
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                // Adiciona a mensagem à ListBox (na interface gráfica) usando o método Invoke para manipulação segura de threads
                Invoke((Action)(() =>
                {
                    listBox1.Items.Add($"{user}: {message}"); // Formata e exibe a mensagem com o nome do usuário
                }));
            });

            // Chama o método para iniciar a conexão com o servidor
            ConnectToServer();
        }

        // Método assíncrono para conectar-se ao servidor
        private async void ConnectToServer()
        {
            try
            {
                // Inicia a conexão com o servidor de forma assíncrona
                await connection.StartAsync();
                listBox1.Items.Add("Conectado ao servidor."); // Exibe uma mensagem de sucesso
            }
            catch (Exception ex)
            {
                // Caso ocorra erro, exibe a mensagem de erro na ListBox
                listBox1.Items.Add($"Erro ao conectar: {ex.Message}");
            }
        }

        // Evento executado ao clicar no botão "Enviar"
        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            string user = "Usuario"; // Nome fixo do usuário; pode ser personalizado para cada cliente
            string message = textBox1.Text; // Obtém o texto digitado pelo usuário

            // Verifica se o campo de mensagem não está vazio antes de enviar
            if (!string.IsNullOrEmpty(message))
            {
                try
                {
                    // Envia a mensagem para o servidor usando o método "SendMessage"
                    await connection.InvokeAsync("SendMessage", user, message);
                    textBox1.Clear(); // Limpa o campo de texto após o envio
                }
                catch (Exception ex)
                {
                    // Em caso de erro ao enviar, exibe a mensagem de erro na ListBox
                    listBox1.Items.Add($"Erro ao enviar mensagem: {ex.Message}");
                }
            }
        }

        // Evento disparado ao selecionar um item na ListBox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Este evento é gerado automaticamente pelo designer e pode ser removido se não for usado
        }

        // Evento disparado ao alterar o texto no TextBox
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Este evento também é gerado automaticamente e pode ser usado para validação, se necessário
        }
    }
}
