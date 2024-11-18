using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SafeEats
{
    public partial class Login : Form
    {
        private EsqueceuSenha esqueceuSenhaForm;
        private MenuInicial menuInicialForm;

        public static int idUsuario;

        public Login()
        {
            InitializeComponent();
        }


        private void btnForgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Abre o formulário de "EsqueceuSenha"
            if (esqueceuSenhaForm == null || esqueceuSenhaForm.IsDisposed)
            {
                esqueceuSenhaForm = new EsqueceuSenha();
            }

            this.Hide();
            esqueceuSenhaForm.ShowDialog();
            this.Show(); // Só mostra o login novamente quando fechar "EsqueceuSenha"
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Verifica se o email e a senha foram preenchidos
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Por favor, insira o email e a senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verifica se o email possui o domínio @safeeats.com
            if (!txtEmail.Text.EndsWith("@safeeats.com"))
            {
                MessageBox.Show("Credenciais inválidas. Por favor, tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Conecta ao banco de dados e verifica as credenciais
            string connectionString = CodigoBanco.conString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT usuario_id FROM Usuario WHERE email = @Email AND senha = @Senha";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Email", txtEmail.Text);
                        command.Parameters.AddWithValue("@Senha", txtPassword.Text);

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            int currentUserId = Convert.ToInt32(result); // Obtém o idUsuario
                            idUsuario = currentUserId;
                            
                            if (menuInicialForm == null || menuInicialForm.IsDisposed)
                            {
                                menuInicialForm = new MenuInicial();
                            }

                            this.Hide();
                            menuInicialForm.ShowDialog();
                        }
                        else
                        {
                            // Credenciais inválidas
                            MessageBox.Show("Credenciais inválidas. Por favor, tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
