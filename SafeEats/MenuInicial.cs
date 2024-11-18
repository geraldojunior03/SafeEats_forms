using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SafeEats
{
    public partial class MenuInicial : Form
    {
        private string idUsuario = Login.idUsuario.ToString();
        private string connectionString = CodigoBanco.conString;

        public MenuInicial()
        {
            InitializeComponent();
            Load += MenuInicial_Load; // Adiciona o evento de carregamento do formulário  
        }

        private void ShowEditProductForm(int productId, List<dynamic> suppliers)
        {
            // Limpa o painel e exibe o formulário de editar produto
            productPanel.Controls.Clear();
            AddNavigationButtons(); // Re-adiciona os botões de navegação

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta para obter os dados do produto
                    string query = "SELECT p.produto_id, p.nome, p.descricao, p.preco, p.quantidade, p.imagem, p.fornecedor_id " +
                                   "FROM Produto p WHERE p.produto_id = @idProduto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idProduto", productId);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVII = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVII);

                        // Título da página
                        Label editProductLabel = new Label
                        {
                            Text = "Editar Produto",
                            Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                            AutoSize = true,
                            Location = new Point(200, 20)
                        };
                        productPanel.Controls.Add(editProductLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelI = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelI);

                        // Recupera a imagem do produto
                        byte[] imageBytes = reader["imagem"] as byte[];
                        PictureBox productPictureBox = new PictureBox
                        {
                            Size = new Size(200, 200),
                            Location = new Point(50, 60),
                            SizeMode = PictureBoxSizeMode.Zoom
                        };

                        if (imageBytes != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                productPictureBox.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            productPictureBox.Image = Image.FromFile("C:\\Users\\Pichau\\source\\repos\\SafeEats\\SafeEats\\na.png"); // Imagem padrão
                        }
                        productPanel.Controls.Add(productPictureBox);

                        // Adiciona um espaçador abaixo da imagem
                        Panel spacerPanelII = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, 0),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelII);

                        // Nome do Produto
                        Label nameLabel = new Label
                        {
                            Text = "Nome:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 60)
                        };
                        productPanel.Controls.Add(nameLabel);

                        // Preenche os campos do produto
                        TextBox nameTextBox = new TextBox
                        {
                            Text = reader["nome"].ToString(),
                            Location = new Point(300, 60),
                            Size = new Size(300, 25)
                        };
                        productPanel.Controls.Add(nameTextBox);

                        // Adiciona um espaçador abaixo da quantidade
                        Panel spacerPanelVIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVIII);

                        // ComboBox de Fornecedores
                        Label supplierLabel = new Label
                        {
                            Text = "Fornecedor:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 100)
                        };
                        productPanel.Controls.Add(supplierLabel);

                        // ComboBox de fornecedores
                        ComboBox supplierComboBox = new ComboBox
                        {
                            Location = new Point(300, 130),
                            Size = new Size(300, 25),
                            DataSource = suppliers,
                            DisplayMember = "nome_fantasia",
                            ValueMember = "fornecedor_id",
                            SelectedValue = reader["fornecedor_id"]
                        };
                        productPanel.Controls.Add(supplierComboBox);

                        // Adiciona um espaçador abaixo do fornecedor
                        Panel spacerPanelIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIII);

                        // Descrição do Produto
                        Label descriptionLabel = new Label
                        {
                            Text = "Descrição:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 160)
                        };
                        productPanel.Controls.Add(descriptionLabel);

                        TextBox descriptionTextBox = new TextBox
                        {
                            Text = reader["descricao"].ToString(),
                            Size = new Size(300, 60),
                            Location = new Point(330, 160),
                            Font = new Font("Segoe UI Variable Display", 9),
                            Multiline = false
                        };
                        productPanel.Controls.Add(descriptionTextBox);

                        // Adiciona um espaçador abaixo da descrição
                        Panel spacerPanelIV = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIV);

                        // Preço do Produto
                        Label priceLabel = new Label
                        {
                            Text = "Preço:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 240)
                        };
                        productPanel.Controls.Add(priceLabel);

                        TextBox priceTextBox = new TextBox
                        {
                            Text = reader["preco"].ToString(),
                            Size = new Size(100, 20),
                            Location = new Point(330, 240),
                            Font = new Font("Segoe UI Variable Display", 9)
                        };
                        productPanel.Controls.Add(priceTextBox);

                        // Adiciona um espaçador abaixo do preço
                        Panel spacerPanelV = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelV);

                        // Quantidade do Produto
                        Label quantityLabel = new Label
                        {
                            Text = "Quantidade:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 270)
                        };
                        productPanel.Controls.Add(quantityLabel);

                        TextBox quantityTextBox = new TextBox
                        {
                            Text = reader["quantidade"].ToString(),
                            Size = new Size(100, 20),
                            Location = new Point(330, 270),
                            Font = new Font("Segoe UI Variable Display", 9)
                        };
                        productPanel.Controls.Add(quantityTextBox);

                        // Adiciona um espaçador abaixo da quantidade
                        Panel spacerPanelVI = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVI);

                        // Botão de Salvar
                        Button saveButton = new Button
                        {
                            Text = "Salvar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(330, 330),
                            BackColor = Color.Green,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        saveButton.FlatAppearance.BorderSize = 0;
                        saveButton.Click += (s, e) =>
                        {
                            // Validação e atualização do produto no banco de dados
                            string newName = nameTextBox.Text;
                            string newDescription = descriptionTextBox.Text;
                            decimal newPrice;
                            int newQuantity;
                            int fornecedorId = (int)supplierComboBox.SelectedItem.GetType().GetProperty("fornecedor_id").GetValue(supplierComboBox.SelectedItem);

                            if (decimal.TryParse(priceTextBox.Text, out newPrice) && int.TryParse(quantityTextBox.Text, out newQuantity))
                            {
                                // Passa a imagem corretamente para a função de atualização
                                UpdateProduct(productId, newName, newDescription, newPrice, newQuantity, fornecedorId, imageBytes);

                                MessageBox.Show("O produto foi alterado com sucesso", "Alteração realizada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                MenuInicial_Load(null, EventArgs.Empty);
                            }
                            else
                            {
                                MessageBox.Show("Por favor, insira valores válidos para preço e quantidade.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };
                        productPanel.Controls.Add(saveButton);

                        // Botão de Deletar
                        Button deleteButton = new Button
                        {
                            Text = "Deletar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(450, 330),
                            BackColor = Color.Red,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        deleteButton.FlatAppearance.BorderSize = 0;
                        deleteButton.Click += (s, e) =>
                        {
                            DialogResult dialogResult = MessageBox.Show("Você tem certeza que deseja deletar este produto?", "Confirmar Deleção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (dialogResult == DialogResult.Yes)
                            {
                                DeleteProduct(productId); // Chama o método de deletar o produto pelo ID
                                productPanel.Controls.Clear();
                                AddNavigationButtons();
                                DisplayProductList();
                            }
                        };
                        productPanel.Controls.Add(deleteButton);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao acessar os dados do produto: " + ex.Message);
                }
            }
        }

        private void DeleteProduct(int productId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Produto WHERE produto_id = @idProduto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idProduto", productId);
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Produto deletado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao deletar o produto.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao deletar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void UpdateProduct(int productId, string name, string description, decimal price, int quantity, int supplierId, byte[] image)
        {
            string query = "UPDATE Produto SET nome = @name, descricao = @description, preco = @price, quantidade = @quantity, fornecedor_id = @supplierId WHERE produto_id = @productId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@supplierId", supplierId);
                command.Parameters.AddWithValue("@productId", productId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void MenuInicial_Load(object sender, EventArgs e)
        {
            // Configura a tela inicial para exibir a lista de produtos
            btnGerenciarProdutos_Click(null, EventArgs.Empty);
        }

        private void btnGerenciarProdutos_Click(object sender, EventArgs e)
        {
            // Limpa os controles atuais do painel de produtos
            productPanel.Controls.Clear();

            // Adiciona os botões de navegação
            AddNavigationButtons();

            // Exibe a lista de produtos
            DisplayProductList();
        }

        private void AddNavigationButtons()
        {
            // Adiciona o botão "Mostrar Produtos"
            Button btnShowProducts = new Button
            {
                Text = "Mostrar Produtos",
                Size = new Size(150, 30),
                Location = new Point(10, 10)
            };
            btnShowProducts.Click += btnShowProducts_Click;
            productPanel.Controls.Add(btnShowProducts);

            // Adiciona o botão "Adicionar Produto"
            Button btnAddProduct = new Button
            {
                Text = "Adicionar Produto",
                Size = new Size(150, 30),
                Location = new Point(170, 10)
            };
            btnAddProduct.Click += btnAddProduct_Click;
            productPanel.Controls.Add(btnAddProduct);
        }

        private void btnShowProducts_Click(object sender, EventArgs e)
        {
            MenuInicial_Load(null, EventArgs.Empty);
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Limpa o painel e exibe o formulário de adicionar produto
            productPanel.Controls.Clear();
            AddNavigationButtons(); // Re-adiciona os botões de navegação
            ShowAddProductForm();
        }

        private List<dynamic> GetAllSuppliers()
        {
            List<dynamic> suppliers = new List<dynamic>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta para obter os fornecedores
                    string query = "SELECT fornecedor_id, nome_fantasia FROM fornecedor";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        suppliers.Add(new
                        {
                            fornecedor_id = reader["fornecedor_id"],
                            nome_fantasia = reader["nome_fantasia"]
                        });
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return suppliers;
        }

        private void DisplayProductList()
        {
            // Adiciona um espaçador abaixo do título
            Panel spacerPanelI = new Panel
            {
                Size = new Size(productPanel.Width - 150, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelI);

            // Adiciona o Label "Produtos" ao painel
            Label produtosLabel = new Label
            {
                Text = "Produtos",
                Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black,
                Location = new Point(20, 190) // Ajuste a coordenada Y para posicionar o texto mais para baixo
            };
            productPanel.Controls.Add(produtosLabel);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelII = new Panel
            {
                Size = new Size(productPanel.Width - 150, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelII);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT produto_id, nome, descricao, preco, quantidade, imagem FROM Produto";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Adiciona cada produto ao painel
                    while (reader.Read())
                    {
                        int idProduct = Convert.ToInt32(reader["produto_id"]);
                        int quantity = Convert.ToInt32(reader["quantidade"]);
                        byte[] imageBytes = reader["imagem"] as byte[];

                        Panel productCard = new Panel
                        {
                            Size = new Size(200, 250),
                            BackColor = quantity == 0 ? Color.LightPink : Color.White, // Sinaliza produtos com quantidade zero
                            Margin = new Padding(10),
                            BorderStyle = quantity == 0 ? BorderStyle.FixedSingle : BorderStyle.None // Adiciona borda se quantidade for zero
                        };

                        // Adiciona a imagem do produto ao painel, se disponível
                        PictureBox productImage = new PictureBox
                        {
                            Size = new Size(100, 100),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point((productCard.Width - 100) / 2, 35)
                        };

                        if (imageBytes != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                productImage.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            productImage.Image = Image.FromFile("C:\\Users\\Pichau\\source\\repos\\SafeEats\\SafeEats\\na.png"); // Imagem padrão se não houver imagem no banco de dados
                        }

                        productCard.Controls.Add(productImage);

                        string productNameText = reader["nome"].ToString();
                        Label productName = new Label
                        {
                            Text = productNameText,
                            Font = new Font("Segoe UI Variable Display", 11, FontStyle.Bold),
                            AutoSize = true,
                            MaximumSize = new Size(productCard.Width - 20, 0)
                        };

                        // Recalcula a localização após o tamanho do texto ser definido
                        productName.Location = new Point((productCard.Width - TextRenderer.MeasureText(productNameText, productName.Font).Width) / 2, 10);
                        productCard.Controls.Add(productName);

                        Label productDetails = new Label
                        {
                            Text = $"Descrição: {reader["descricao"]} \nPreço: {reader["preco"]:C2} \nQuantidade: {quantity}",
                            Font = new Font("Segoe UI Variable Display", 8),
                            AutoSize = true,
                            MaximumSize = new Size(productCard.Width - 20, 0),
                            Location = new Point(10, 147)
                        };
                        productCard.Controls.Add(productDetails);

                        // Adiciona um rótulo de "Esgotado" se a quantidade for zero
                        if (quantity == 0)
                        {
                            Label outOfStockLabel = new Label
                            {
                                Text = "Esgotado",
                                Font = new Font("Segoe UI Variable Display", 12, FontStyle.Bold),
                                ForeColor = Color.Red,
                                BackColor = Color.Transparent,
                                AutoSize = true,
                                Location = new Point((productCard.Width - 80) / 2, productCard.Height - 57)
                            };
                            productCard.Controls.Add(outOfStockLabel);
                        }

                        // Adiciona o botão "Editar" para cada produto
                        Button editButton = new Button
                        {
                            Text = "Editar",
                            Font = new Font("Segoe UI Variable Display", 9),
                            Size = new Size(50, 25),
                            Location = new Point((productCard.Width - 50) / 2, productCard.Height - 35)
                        };
                        editButton.Click += (s, args) =>
                        {
                            List<dynamic> suppliers = GetAllSuppliers();
                            ShowEditProductForm(idProduct, suppliers);
                        };
                        productCard.Controls.Add(editButton);

                        productPanel.Controls.Add(productCard);
                    }
                    reader.Close();

                    // Adiciona um espaçador abaixo do título
                    Panel spacerPanelIs = new Panel
                    {
                        Size = new Size(productPanel.Width - 150, 40), // Ajuste a altura conforme necessário
                        BackColor = Color.Transparent
                    };
                    productPanel.Controls.Add(spacerPanelIs);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowAddProductForm()
        {
            // Adiciona um espaçador abaixo do título
            Panel spacerPanelI = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelI);

            // Adiciona o Label "Adicionar Produto" ao painel
            Label addProductLabel = new Label
            {
                Text = "Adicionar Produto",
                Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black
            };
            productPanel.Controls.Add(addProductLabel);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelII = new Panel
            {
                Size = new Size(productPanel.Width - 100, 20),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelII);

            // PictureBox para mostrar a imagem selecionada
            PictureBox productPictureBox = new PictureBox
            {
                Size = new Size(200, 150),
                Location = new Point(10, 60),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            productPanel.Controls.Add(productPictureBox);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelIII = new Panel
            {
                Size = new Size(productPanel.Width - 100, 0),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelIII);

            // Botão para selecionar uma imagem
            Button selectImageButton = new Button
            {
                Text = "Selecionar Imagem",
                Font = new Font("Segoe UI Variable Display", 10),
                Size = new Size(120, 30),
                Location = new Point(10, 45)
            };
            productPanel.Controls.Add(selectImageButton);

            // Evento de clique para selecionar a imagem
            selectImageButton.Click += (s, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Selecione uma imagem",
                    Filter = "Imagens (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    productPictureBox.Image = Image.FromFile(openFileDialog.FileName);
                }
            };

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelIV = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelIV);

            // Campos de entrada de dados para o novo produto
            Label nameLabel = new Label
            {
                Text = "Nome:",
                Font = new Font("Segoe UI Variable Display", 10),
                AutoSize = true,
                Location = new Point(10, 60)
            };
            productPanel.Controls.Add(nameLabel);

            TextBox nameTextBox = new TextBox
            {
                Size = new Size(200, 20),
                Location = new Point(10, 80)
            };
            productPanel.Controls.Add(nameTextBox);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelV = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelV);

            Label descriptionLabel = new Label
            {
                Text = "Descrição:",
                Font = new Font("Segoe UI Variable Display", 10),
                AutoSize = true,
                Location = new Point(10, 110)
            };
            productPanel.Controls.Add(descriptionLabel);

            TextBox descriptionTextBox = new TextBox
            {
                Size = new Size(200, 60),
                Location = new Point(10, 130),
                Multiline = false
            };
            productPanel.Controls.Add(descriptionTextBox);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelVI = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelVI);

            // Adiciona um Label para o ComboBox de Fornecedor
            Label fornecedorLabel = new Label
            {
                Text = "Fornecedor:",
                Font = new Font("Segoe UI Variable Display", 10),
                AutoSize = true,
                Location = new Point(10, 300)
            };
            productPanel.Controls.Add(fornecedorLabel);

            // Cria o ComboBox para selecionar o fornecedor
            ComboBox fornecedorComboBox = new ComboBox
            {
                Size = new Size(200, 20),
                Location = new Point(10, 320),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            productPanel.Controls.Add(fornecedorComboBox);

            // Carregar os fornecedores no ComboBox
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT fornecedor_id, nome_fantasia FROM fornecedor";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Cria um item para "Selecione um fornecedor", mas não permite seleção
                    var defaultItem = new { fornecedor_id = 0, nome_fantasia = "Selecione um fornecedor" };
                    fornecedorComboBox.Items.Add(defaultItem);
                    fornecedorComboBox.SelectedItem = defaultItem;  // Define como item selecionado inicialmente

                    while (reader.Read())
                    {
                        // Adiciona fornecedores reais à lista
                        fornecedorComboBox.Items.Add(new
                        {
                            fornecedor_id = reader["fornecedor_id"],
                            nome_fantasia = reader["nome_fantasia"]
                        });
                    }

                    fornecedorComboBox.DisplayMember = "nome_fantasia";  // Define o que será exibido no ComboBox
                    fornecedorComboBox.ValueMember = "fornecedor_id";    // Define o valor que será acessado (fornecedor_id)

                    // Desabilita o item "Selecione um fornecedor" para não ser clicável
                    fornecedorComboBox.Items[0] = new { fornecedor_id = 0, nome_fantasia = "Selecione um fornecedor" };
                    fornecedorComboBox.DropDownStyle = ComboBoxStyle.DropDownList; // Impede que o item inicial seja modificado
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelIX = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelIX);

            Label priceLabel = new Label
            {
                Text = "Preço:",
                Font = new Font("Segoe UI Variable Display", 10),
                AutoSize = true,
                Location = new Point(10, 200)
            };
            productPanel.Controls.Add(priceLabel);

            TextBox priceTextBox = new TextBox
            {
                Size = new Size(200, 20),
                Location = new Point(10, 220)
            };
            productPanel.Controls.Add(priceTextBox);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelVII = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelVII);

            Label quantityLabel = new Label
            {
                Text = "Quantidade:",
                Font = new Font("Segoe UI Variable Display", 10),
                AutoSize = true,
                Location = new Point(10, 250)
            };
            productPanel.Controls.Add(quantityLabel);

            TextBox quantityTextBox = new TextBox
            {
                Size = new Size(200, 20),
                Location = new Point(10, 270)
            };
            productPanel.Controls.Add(quantityTextBox);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelVIII = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelVIII);

            // Botão de Salvar
            Button saveButton = new Button
            {
                Text = "Salvar",
                Font = new Font("Segoe UI Variable Display", 10),
                Size = new Size(100, 30),
                Location = new Point(330, 260),
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Click += (s, e) =>
            {
                // Código para salvar o novo produto
                string name = nameTextBox.Text;
                string description = descriptionTextBox.Text;
                decimal price;
                int quantity;
                byte[] imageBytes = null;

                if (productPictureBox.Image != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        productPictureBox.Image.Save(ms, productPictureBox.Image.RawFormat);
                        imageBytes = ms.ToArray();
                    }
                }

                if (decimal.TryParse(priceTextBox.Text, out price) && int.TryParse(quantityTextBox.Text, out quantity))
                {
                    // Verifica se um fornecedor foi selecionado
                    var selectedFornecedor = fornecedorComboBox.SelectedItem as dynamic;
                    int fornecedorId = selectedFornecedor?.fornecedor_id ?? 0;

                    // Verifica se o fornecedor foi selecionado
                    if (fornecedorId == 0)
                    {
                        MessageBox.Show("Por favor, selecione um fornecedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "INSERT INTO Produto (fornecedor_id, nome, descricao, preco, quantidade, imagem) VALUES (@fornecedor_id, @nome, @descricao, @preco, @quantidade, @imagem)";
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@fornecedor_id", fornecedorId);
                            command.Parameters.AddWithValue("@nome", name);
                            command.Parameters.AddWithValue("@descricao", description);
                            command.Parameters.AddWithValue("@preco", price);
                            command.Parameters.AddWithValue("@quantidade", quantity);
                            command.Parameters.AddWithValue("@imagem", (object)imageBytes ?? DBNull.Value);
                            command.ExecuteNonQuery();

                            MessageBox.Show("Produto adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Limpa os campos após a inserção
                            nameTextBox.Clear();
                            descriptionTextBox.Clear();
                            priceTextBox.Clear();
                            quantityTextBox.Clear();
                            productPictureBox.Image = null;
                            fornecedorComboBox.SelectedIndex = -1; // Reseta o ComboBox de fornecedor
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao adicionar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, insira valores válidos para preço e quantidade.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            productPanel.Controls.Add(saveButton);
            // Botão de Salvar
            Button voltarButton = new Button
            {
                Text = "Voltar",
                Font = new Font("Segoe UI Variable Display", 10),
                Size = new Size(100, 30),
                Location = new Point(450, 260),
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            voltarButton.FlatAppearance.BorderSize = 0;
            voltarButton.Click += (s, e) =>
            {
                MenuInicial_Load(null, EventArgs.Empty);
            };
            productPanel.Controls.Add(voltarButton);
        }

        private void AddNavigationButtonsSuppliers()
        {
            // Adiciona o botão "Mostrar Fornecedores"
            Button btnShowSuppliers = new Button
            {
                Text = "Mostrar Fornecedores",
                Size = new Size(150, 30),
                Location = new Point(10, 10)
            };
            btnShowSuppliers.Click += btnShowSuppliers_Click;
            productPanel.Controls.Add(btnShowSuppliers);

            // Adiciona o botão "Adicionar Produto"
            Button btnAddSuppliers = new Button
            {
                Text = "Adicionar Fornecedores",
                Size = new Size(150, 30),
                Location = new Point(170, 10)
            };
            btnAddSuppliers.Click += btnAddSuppliers_Click;
            productPanel.Controls.Add(btnAddSuppliers);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelI = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelI);
        }

        private void btnShowSuppliers_Click(object sender, EventArgs e)
        {
            btnGerenciarFornecedores_Click(null, EventArgs.Empty);
        }

        private void btnAddSuppliers_Click(object sender, EventArgs e)
        {
            // Limpa o painel e exibe o formulário de adicionar produto
            productPanel.Controls.Clear();
            AddNavigationButtonsSuppliers(); // Re-adiciona os botões de navegação
            ShowAddFornecedorForm();
        }

        private void btnGerenciarFornecedores_Click(object sender, EventArgs e)
        {
            // Limpa os controles atuais do painel de produtos
            productPanel.Controls.Clear();
            AddNavigationButtonsSuppliers();

            // Adiciona o Label "Produtos" ao painel
            Label fornecedoresLabel = new Label
            {
                Text = "Fornecedores",
                Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black,
                Location = new Point(20, 190) // Ajuste a coordenada Y para posicionar o texto mais para baixo
            };
            productPanel.Controls.Add(fornecedoresLabel);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanel = new Panel
            {
                Size = new Size(productPanel.Width - 20, 20),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanel);

            // Exibe a lista de fornecedores
            DisplayFornecedores();
        }

        private void DisplayFornecedores()
        {
            // Limpa os fornecedores antigos do painel
            Panel fornecedoresPanel = new Panel
            {
                AutoScroll = true,
                Size = new Size(productPanel.Width - 20, productPanel.Height - 100),
                Location = new Point(0, 100)
            };
            productPanel.Controls.Add(fornecedoresPanel);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT fornecedor_id, nome_fantasia, razao_social, tipo_produto, imagem FROM Fornecedor";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Adiciona cada fornecedor ao painel
                    while (reader.Read())
                    {
                        int fornecedorId = Convert.ToInt32(reader["fornecedor_id"]); // Capture o ID do fornecedor aqui

                        Panel fornecedorCard = new Panel
                        {
                            Size = new Size(200, 230),
                            BackColor = Color.White,
                            Margin = new Padding(10)
                        };

                        string fornecedorNameText = reader["nome_fantasia"].ToString();
                        Label fornecedorName = new Label
                        {
                            Text = fornecedorNameText,
                            Font = new Font("Segoe UI Variable Display", 10, FontStyle.Bold),
                            AutoSize = true,
                            MaximumSize = new Size(fornecedorCard.Width - 20, 0)
                        };

                        // Recalcula a localização após o tamanho do texto ser definido
                        fornecedorName.Location = new Point((fornecedorCard.Width - TextRenderer.MeasureText(fornecedorNameText, fornecedorName.Font).Width) / 2, 10);
                        fornecedorCard.Controls.Add(fornecedorName);

                        // Imagem do Fornecedor
                        PictureBox fornecedorPictureBox = new PictureBox
                        {
                            Size = new Size(100, 100),
                            Location = new Point((fornecedorCard.Width - 100) / 2, 40),
                            SizeMode = PictureBoxSizeMode.Zoom
                        };

                        byte[] imageBytes = reader["imagem"] as byte[];
                        if (imageBytes != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                fornecedorPictureBox.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            fornecedorPictureBox.Image = Image.FromFile("C:\\Users\\Pichau\\source\\repos\\SafeEats\\SafeEats\\na.png"); // Substitua pelo caminho da imagem padrão
                        }
                        fornecedorCard.Controls.Add(fornecedorPictureBox);

                        Label fornecedorDetails = new Label
                        {
                            Text = $"Razão Social: {reader["razao_social"].ToString()} \nTipo de Produto: {reader["tipo_produto"].ToString()}",
                            Font = new Font("Segoe UI Variable Display", 8),
                            AutoSize = true,
                            MaximumSize = new Size(fornecedorCard.Width - 20, 0),
                            Location = new Point(10, 150)
                        };
                        fornecedorCard.Controls.Add(fornecedorDetails);

                        // Adiciona o botão "Editar" para cada fornecedor
                        Button editButton = new Button
                        {
                            Text = "Editar",
                            Font = new Font("Segoe UI Variable Display", 8),
                            Size = new Size(50, 25),
                            Location = new Point((fornecedorCard.Width - 50) / 2, fornecedorCard.Height - 35)
                        };
                        editButton.Click += (s, args) =>
                        {
                            ShowEditFornecedorForm(fornecedorId); // Passa o ID para o método de edição
                        };
                        fornecedorCard.Controls.Add(editButton);
                        fornecedoresPanel.Controls.Add(fornecedorCard);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar fornecedores: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowEditFornecedorForm(int fornecedorId)
        {
            // Limpa o painel e exibe o formulário de editar fornecedor
            productPanel.Controls.Clear();
            AddNavigationButtonsSuppliers(); // Re-adiciona os botões de navegação

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT nome_fantasia, razao_social, tipo_produto, imagem FROM Fornecedor WHERE fornecedor_id = @idFornecedor";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idFornecedor", fornecedorId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelI = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelI);

                        // Título da página
                        Label editFornecedorLabel = new Label
                        {
                            Text = "Editar Fornecedor",
                            Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                            AutoSize = true,
                            Location = new Point(200, 20)
                        };
                        productPanel.Controls.Add(editFornecedorLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelII = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelII);

                        // Recupera a imagem do produto do banco de dados
                        byte[] imageBytes = reader["imagem"] as byte[];
                        PictureBox fornecedorPictureBox = new PictureBox
                        {
                            Size = new Size(200, 200),
                            Location = new Point(50, 60),
                            SizeMode = PictureBoxSizeMode.Zoom
                        };

                        if (imageBytes != null)
                        {
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                fornecedorPictureBox.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            fornecedorPictureBox.Image = Image.FromFile("C:\\Users\\Pichau\\source\\repos\\SafeEats\\SafeEats\\na.png"); // Imagem padrão se não houver imagem no banco de dados
                        }
                        productPanel.Controls.Add(fornecedorPictureBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 100, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIII);

                        // Nome Fantasia do Fornecedor
                        Label nameLabel = new Label
                        {
                            Text = "Nome Fantasia:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 60)
                        };
                        productPanel.Controls.Add(nameLabel);

                        TextBox nameTextBox = new TextBox
                        {
                            Text = reader["nome_fantasia"].ToString(),
                            Size = new Size(200, 20),
                            Location = new Point(390, 60)
                        };
                        productPanel.Controls.Add(nameTextBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelIV = new Panel
                        {
                            Size = new Size(productPanel.Width - 100, 0),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIV);


                        // Razão Social do Fornecedor
                        Label razaoSocialLabel = new Label
                        {
                            Text = "Razão Social:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 100)
                        };
                        productPanel.Controls.Add(razaoSocialLabel);

                        TextBox razaoSocialTextBox = new TextBox
                        {
                            Text = reader["razao_social"].ToString(),
                            Size = new Size(300, 20),
                            Location = new Point(390, 100)
                        };
                        productPanel.Controls.Add(razaoSocialTextBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelV = new Panel
                        {
                            Size = new Size(productPanel.Width - 100, 0),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelV);


                        // Tipo de Produto do Fornecedor
                        Label tipoProdutoLabel = new Label
                        {
                            Text = "Tipo de Produto:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(270, 140)
                        };
                        productPanel.Controls.Add(tipoProdutoLabel);

                        TextBox tipoProdutoTextBox = new TextBox
                        {
                            Text = reader["tipo_produto"].ToString(),
                            Size = new Size(300, 20),
                            Location = new Point(390, 140)
                        };
                        productPanel.Controls.Add(tipoProdutoTextBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVI = new Panel
                        {
                            Size = new Size(productPanel.Width - 100, 0),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVI);

                        // Botão de Salvar
                        Button saveButton = new Button
                        {
                            Text = "Salvar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(330, 260),
                            BackColor = Color.Green,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        saveButton.FlatAppearance.BorderSize = 0;
                        saveButton.Click += (s, args) =>
                        {
                            // Valida se os campos estão preenchidos
                            if (string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                                string.IsNullOrWhiteSpace(razaoSocialTextBox.Text) ||
                                string.IsNullOrWhiteSpace(tipoProdutoTextBox.Text))
                            {
                                MessageBox.Show("Todos os campos devem ser preenchidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Reabre a conexão para atualizar os dados
                            using (SqlConnection updateConnection = new SqlConnection(connectionString))
                            {
                                updateConnection.Open();
                                string updateQuery = "UPDATE Fornecedor SET nomeFantasia = @nomeFantasia, razaoSocial = @razaoSocial, tipoProduto = @tipoProduto WHERE idFornecedor = @idFornecedor";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, updateConnection))
                                {
                                    updateCommand.Parameters.AddWithValue("@nomeFantasia", nameTextBox.Text);
                                    updateCommand.Parameters.AddWithValue("@razaoSocial", razaoSocialTextBox.Text);
                                    updateCommand.Parameters.AddWithValue("@tipoProduto", tipoProdutoTextBox.Text);
                                    updateCommand.Parameters.AddWithValue("@idFornecedor", fornecedorId);

                                    int rowsAffected = updateCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Fornecedor atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnGerenciarFornecedores_Click(null, null); // Recarrega a lista de fornecedores
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erro ao atualizar fornecedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        };
                        productPanel.Controls.Add(saveButton);

                        // Botão de Excluir
                        Button deleteButton = new Button
                        {
                            Text = "Excluir",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(390, 200),
                            BackColor = Color.Red,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        deleteButton.FlatAppearance.BorderSize = 0;
                        deleteButton.Click += (s, args) =>
                        {
                            var confirmResult = MessageBox.Show("Tem certeza de que deseja excluir este fornecedor?", "Confirmação de Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (confirmResult == DialogResult.Yes)
                            {
                                // Reabre a conexão para excluir os dados
                                using (SqlConnection deleteConnection = new SqlConnection(connectionString))
                                {
                                    deleteConnection.Open();
                                    string deleteQuery = "DELETE FROM Fornecedor WHERE idFornecedor = @idFornecedor";
                                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, deleteConnection))
                                    {
                                        deleteCommand.Parameters.AddWithValue("@idFornecedor", fornecedorId);

                                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                                        if (rowsAffected > 0)
                                        {
                                            MessageBox.Show("Fornecedor excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            btnGerenciarFornecedores_Click(null, null); // Recarrega a lista de fornecedores
                                        }
                                        else
                                        {
                                            MessageBox.Show("Erro ao excluir fornecedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        };
                        productPanel.Controls.Add(deleteButton);

                        // Botão de Cancelar
                        Button cancelButton = new Button
                        {
                            Text = "Cancelar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(510, 200),
                            BackColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        cancelButton.FlatAppearance.BorderSize = 0;
                        cancelButton.Click += (s, args) =>
                        {
                            btnGerenciarFornecedores_Click(null, null); // Recarrega a lista de fornecedores
                        };
                        productPanel.Controls.Add(cancelButton);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados do fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowAddFornecedorForm()
        {
            // Limpa o painel e exibe o formulário de adicionar fornecedor
            productPanel.Controls.Clear();
            AddNavigationButtonsSuppliers(); // Re-adiciona os botões de navegação

            // Título da página
            Label addFornecedorLabel = new Label
            {
                Text = "Adicionar Fornecedor",
                Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(200, 20)
            };
            productPanel.Controls.Add(addFornecedorLabel);

            // Campos de entrada para o novo fornecedor
            Label nameLabel = new Label
            {
                Text = "Nome Fantasia:",
                Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(270, 60)
            };
            productPanel.Controls.Add(nameLabel);

            TextBox nameTextBox = new TextBox
            {
                Size = new Size(200, 20),
                Location = new Point(390, 60)
            };
            productPanel.Controls.Add(nameTextBox);

            Label razaoSocialLabel = new Label
            {
                Text = "Razão Social:",
                Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(270, 100)
            };
            productPanel.Controls.Add(razaoSocialLabel);

            TextBox razaoSocialTextBox = new TextBox
            {
                Size = new Size(300, 20),
                Location = new Point(390, 100)
            };
            productPanel.Controls.Add(razaoSocialTextBox);

            Label tipoProdutoLabel = new Label
            {
                Text = "Tipo de Produto:",
                Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(270, 140)
            };
            productPanel.Controls.Add(tipoProdutoLabel);

            TextBox tipoProdutoTextBox = new TextBox
            {
                Size = new Size(300, 20),
                Location = new Point(390, 140)
            };
            productPanel.Controls.Add(tipoProdutoTextBox);

            // Botão de Salvar
            Button saveButton = new Button
            {
                Text = "Salvar",
                Font = new Font("Segoe UI Variable Display", 10),
                Size = new Size(100, 30),
                Location = new Point(270, 200)
            };
            saveButton.Click += (s, args) =>
            {
                // Valida se os campos estão preenchidos
                if (string.IsNullOrWhiteSpace(nameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(razaoSocialTextBox.Text) ||
                    string.IsNullOrWhiteSpace(tipoProdutoTextBox.Text))
                {
                    MessageBox.Show("Todos os campos devem ser preenchidos.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string insertQuery = "INSERT INTO Fornecedor (nomeFantasia, razaoSocial, tipoProduto) VALUES (@nomeFantasia, @razaoSocial, @tipoProduto)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@nomeFantasia", nameTextBox.Text);
                            insertCommand.Parameters.AddWithValue("@razaoSocial", razaoSocialTextBox.Text);
                            insertCommand.Parameters.AddWithValue("@tipoProduto", tipoProdutoTextBox.Text);

                            int rowsAffected = insertCommand.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Fornecedor adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnGerenciarFornecedores_Click(null, null); // Recarrega a lista de fornecedores
                            }
                            else
                            {
                                MessageBox.Show("Erro ao adicionar fornecedor.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao adicionar fornecedor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            productPanel.Controls.Add(saveButton);

            // Botão de Cancelar
            Button cancelButton = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI Variable Display", 10),
                Size = new Size(100, 30),
                Location = new Point(390, 200)
            };
            cancelButton.Click += (s, args) =>
            {
                btnGerenciarFornecedores_Click(null, null); // Recarrega a lista de fornecedores
            };
            productPanel.Controls.Add(cancelButton);
        }

        private void AddNavigationButtonsPedidos()
        {
            // Adiciona o botão "Mostrar Pedidos"
            Button btnShowPedidos = new Button
            {
                Text = "Mostrar Pedidos",
                Size = new Size(150, 30),
                Location = new Point(10, 10)
            };
            btnShowPedidos.Click += btnShowPedidos_Click;
            productPanel.Controls.Add(btnShowPedidos);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanelI = new Panel
            {
                Size = new Size(productPanel.Width - 100, 10),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanelI);
        }

        private void btnShowPedidos_Click(object sender, EventArgs e)
        {
            btnGerenciarPedidos_Click(null, EventArgs.Empty);
        }

        private void btnGerenciarPedidos_Click(object sender, EventArgs e)
        {
            // Limpa os controles atuais do painel de produtos
            productPanel.Controls.Clear();

            // Adiciona o Label "Pedidos" ao painel
            Label pedidosLabel = new Label
            {
                Text = "Pedidos",
                Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Black,
                Location = new Point(20, 190) // Ajuste a coordenada Y para posicionar o texto mais para baixo
            };
            productPanel.Controls.Add(pedidosLabel);

            // Adiciona um espaçador abaixo do título
            Panel spacerPanel = new Panel
            {
                Size = new Size(productPanel.Width - 20, 20),
                BackColor = Color.Transparent
            };
            productPanel.Controls.Add(spacerPanel);

            // Exibe a lista de pedidos
            DisplayPedidos();
        }

        private void DisplayPedidos()
        {

            // Variáveis para controlar a posição dos cartões de pedidos
            int xOffset = 10; // Posição inicial horizontal
            int yOffset = 10; // Posição inicial vertical
            int maxWidth = productPanel.Width - 20; // Largura máxima do painel (com margens)

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT pedido_id, data_pedido, status, cpf FROM Pedido INNER JOIN Cliente ON Pedido.cliente_id = Cliente.cliente_id";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    // Adiciona cada pedido ao painel
                    while (reader.Read())
                    {
                        int pedidoId = Convert.ToInt32(reader["pedido_id"]);

                        Panel pedidoCard = new Panel
                        {
                            Size = new Size(200, 230), // Tamanho fixo para o cartão
                            BackColor = Color.White,
                            Margin = new Padding(10),
                            Location = new Point(xOffset, yOffset) // Posição do cartão
                        };

                        Label pedidoName = new Label
                        {
                            Text = $"Pedido #{pedidoId}",
                            Font = new Font("Segoe UI Variable Display", 10, FontStyle.Bold),
                            AutoSize = true,
                            MaximumSize = new Size(pedidoCard.Width - 20, 0)
                        };
                        pedidoName.Location = new Point((pedidoCard.Width - TextRenderer.MeasureText(pedidoId.ToString(), pedidoName.Font).Width) / 3, 10);
                        pedidoCard.Controls.Add(pedidoName);

                        // Adiciona a imagem do produto ao painel, se disponível
                        PictureBox pedidoImage = new PictureBox
                        {
                            Size = new Size(100, 100),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point((pedidoCard.Width - 100) / 2, 30),
                            Image = Image.FromFile("C:\\Users\\Pichau\\source\\repos\\SafeEats\\SafeEats\\na.png")
                        };
                        pedidoCard.Controls.Add(pedidoImage);

                        Label pedidoDetails = new Label
                        {
                            Text = $"Data: {Convert.ToDateTime(reader["data_pedido"]).ToShortDateString()}\nStatus: {reader["status"]}\nCPF Cliente: {reader["cpf"]}",
                            Font = new Font("Segoe UI Variable Display", 8),
                            AutoSize = true,
                            MaximumSize = new Size(pedidoCard.Width - 10, 120)
                        };
                        pedidoDetails.Location = new Point(10, 135);
                        pedidoCard.Controls.Add(pedidoDetails);

                        // Adiciona o botão "Editar" para cada pedido
                        Button editButton = new Button
                        {
                            Text = "Editar",
                            Font = new Font("Segoe UI Variable Display", 8),
                            Size = new Size(50, 25),
                            Location = new Point((pedidoCard.Width - 50) / 2, pedidoCard.Height - 35)
                        };
                        editButton.Click += (s, args) =>
                        {
                            ShowEditPedidoForm(pedidoId);
                        };
                        pedidoCard.Controls.Add(editButton);

                        // Verifica se o próximo cartão ultrapassaria a largura do painel
                        if (xOffset + pedidoCard.Width + 10 > maxWidth)
                        {
                            // Se ultrapassar, reseta a posição x e aumenta o yOffset para a próxima linha
                            xOffset = 10;
                            yOffset += pedidoCard.Height + 10;
                        }

                        // Adiciona o painel do pedido ao painel principal
                        productPanel.Controls.Add(pedidoCard);

                        // Atualiza a posição x para o próximo cartão
                        xOffset += pedidoCard.Width + 10;
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar pedidos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowEditPedidoForm(int pedidoId)
        {
            // Limpa o painel e exibe o formulário de editar pedido
            productPanel.Controls.Clear();
            AddNavigationButtonsPedidos(); // Re-adiciona os botões de navegação

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT pedido_id, data_pedido, status, cpf FROM Pedido INNER JOIN Cliente ON Pedido.cliente_id = Cliente.cliente_id WHERE pedido_id = @codPedido";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@codPedido", pedidoId);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelI = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelI);

                        // Título da página
                        Label editPedidoLabel = new Label
                        {
                            Text = "Editar Pedido",
                            Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                            AutoSize = true,
                            Location = new Point(200, 20)
                        };
                        productPanel.Controls.Add(editPedidoLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelII = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelII);

                        // Adiciona uma imagem fixa (exemplo)
                        PictureBox pedidoImage = new PictureBox
                        {
                            Image = Image.FromFile("C:\\Users\\Pichau\\source\\repos\\SafeEats\\SafeEats\\na.png"), // Certifique-se de alterar para o caminho correto da imagem
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(200, 200),
                            Location = new Point(270, 60)
                        };
                        productPanel.Controls.Add(pedidoImage);

                        // Adiciona um espaçador abaixo da imagem
                        Panel spacerPanelIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 10),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIII);

                        // Campos de exibição de detalhes do pedido
                        Label pedidoDetails = new Label
                        {
                            Text = $"Pedido: {reader["pedido_id"]}\nData: {Convert.ToDateTime(reader["data_pedido"]).ToShortDateString()}\nCliente CPF: {reader["cpf"]}",
                            Font = new Font("Segoe UI Variable Display", 12),
                            AutoSize = true,
                            Location = new Point(380, 60)
                        };
                        productPanel.Controls.Add(pedidoDetails);

                        // Adiciona um espaçador abaixo dos detalhes
                        Panel spacerPanelIV = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 3),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIV);

                        // Checkboxes para editar o status do pedido
                        CheckBox statusEmProgressoCheckBox = new CheckBox
                        {
                            Text = "Em Progresso",
                            Checked = reader["status"].ToString() == "Em Progresso",
                            Location = new Point(270, 180),
                            Font = new Font("Segoe UI Variable Display", 12),
                            AutoSize = true
                        };
                        productPanel.Controls.Add(statusEmProgressoCheckBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelV = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 1),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelV);

                        CheckBox statusConcluidoCheckBox = new CheckBox
                        {
                            Text = "Concluído",
                            Checked = reader["status"].ToString() == "Concluído",
                            Location = new Point(270, 210),
                            Font = new Font("Segoe UI Variable Display", 12)
                        };
                        productPanel.Controls.Add(statusConcluidoCheckBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVI = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 1),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVI);

                        CheckBox statusCanceladoCheckBox = new CheckBox
                        {
                            Text = "Cancelado",
                            Checked = reader["status"].ToString() == "Cancelado",
                            Location = new Point(270, 240),
                            Font = new Font("Segoe UI Variable Display", 12)
                        };
                        productPanel.Controls.Add(statusCanceladoCheckBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVII = new Panel
                        {
                            Size = new Size(productPanel.Width - 120, -1),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVII);

                        // Novo checkbox "Em Rota de Entrega"
                        CheckBox statusEmRotaCheckBox = new CheckBox
                        {
                            Text = "Em Rota de Entrega",
                            Checked = reader["status"].ToString() == "Em Rota de Entrega",
                            Location = new Point(270, 270),
                            Font = new Font("Segoe UI Variable Display", 12)
                        };
                        productPanel.Controls.Add(statusEmRotaCheckBox);


                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 150, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVIII);

                        // Adiciona eventos CheckedChanged para controlar a seleção única
                        statusEmProgressoCheckBox.CheckedChanged += (s, e) =>
                        {
                            if (statusEmProgressoCheckBox.Checked)
                            {
                                statusConcluidoCheckBox.Checked = false;
                                statusCanceladoCheckBox.Checked = false;
                                statusEmRotaCheckBox.Checked = false;
                            }
                        };

                        statusConcluidoCheckBox.CheckedChanged += (s, e) =>
                        {
                            if (statusConcluidoCheckBox.Checked)
                            {
                                statusEmProgressoCheckBox.Checked = false;
                                statusCanceladoCheckBox.Checked = false;
                                statusEmRotaCheckBox.Checked = false;
                            }
                        };

                        statusCanceladoCheckBox.CheckedChanged += (s, e) =>
                        {
                            if (statusCanceladoCheckBox.Checked)
                            {
                                statusEmProgressoCheckBox.Checked = false;
                                statusConcluidoCheckBox.Checked = false;
                                statusEmRotaCheckBox.Checked = false;
                            }
                        };

                        statusEmRotaCheckBox.CheckedChanged += (s, e) =>
                        {
                            if (statusEmRotaCheckBox.Checked)
                            {
                                statusEmProgressoCheckBox.Checked = false;
                                statusConcluidoCheckBox.Checked = false;
                                statusCanceladoCheckBox.Checked = false;
                            }
                        };

                        // Verifica o status do pedido
                        string statusPedido = reader["status"].ToString();

                        // Desabilita os checkboxes se o status for "Concluído"
                        if (statusPedido == "Concluído")
                        {
                            statusEmProgressoCheckBox.Enabled = false;
                            statusConcluidoCheckBox.Enabled = false;
                            statusCanceladoCheckBox.Enabled = false;
                            statusEmRotaCheckBox.Enabled = false;
                        }
                        else
                        {
                            // Habilita os checkboxes para edição
                            statusEmProgressoCheckBox.Enabled = true;
                            statusConcluidoCheckBox.Enabled = true;
                            statusCanceladoCheckBox.Enabled = true;
                            statusEmRotaCheckBox.Enabled = true;
                        }


                        // Botão de Salvar
                        Button saveButton = new Button
                        {
                            Text = "Salvar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(270, 330)
                        };
                        saveButton.Click += (s, args) =>
                        {
                            // Verifica qual CheckBox está selecionado
                            string novoStatus = "";
                            if (statusEmProgressoCheckBox.Checked) novoStatus = "Em Progresso";
                            else if (statusConcluidoCheckBox.Checked) novoStatus = "Concluído";
                            else if (statusCanceladoCheckBox.Checked) novoStatus = "Cancelado";
                            else if (statusEmRotaCheckBox.Checked) novoStatus = "Em Rota de Entrega";

                            // Valida se algum status foi selecionado
                            if (string.IsNullOrWhiteSpace(novoStatus))
                            {
                                MessageBox.Show("Selecione um status para o pedido.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            // Reabre a conexão para atualizar os dados
                            using (SqlConnection updateConnection = new SqlConnection(connectionString))
                            {
                                updateConnection.Open();
                                string updateQuery = "UPDATE Pedido SET status = @statusPedido WHERE pedido_id = @codPedido";
                                using (SqlCommand updateCommand = new SqlCommand(updateQuery, updateConnection))
                                {
                                    updateCommand.Parameters.AddWithValue("@statusPedido", novoStatus);
                                    updateCommand.Parameters.AddWithValue("@codPedido", pedidoId);

                                    int rowsAffected = updateCommand.ExecuteNonQuery();
                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Pedido atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnGerenciarPedidos_Click(null, null); // Recarrega a lista de pedidos
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erro ao atualizar pedido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        };
                        productPanel.Controls.Add(saveButton);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar detalhes do pedido: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private string MascararCPF(string cpf)
        {
            if (cpf.Length == 11)
            {
                return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
            }
            return "CPF Inválido";
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            // Limpa o painel atual antes de carregar o perfil do usuário
            productPanel.Controls.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT u.nome, u.email, a.registro_numero
                    FROM administrador a
                    INNER JOIN usuario u ON u.usuario_id = a.usuario_id
                    WHERE u.usuario_id = @idAdministrador";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idAdministrador", idUsuario);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Configurações gerais
                        int labelSpacing = 10;
                        int initialTop = 20;
                        int leftMargin = 20;

                        // Título da seção
                        Label headerLabel = new Label
                        {
                            Text = "Perfil do Administrador",
                            Font = new Font("Segoe UI Variable Display", 14, FontStyle.Bold),
                            AutoSize = true,
                            ForeColor = Color.DarkSlateGray,
                            Location = new Point(leftMargin, initialTop)
                        };
                        productPanel.Controls.Add(headerLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelI = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelI);

                        // Linha separadora
                        Panel separator = new Panel
                        {
                            Height = 2,
                            Width = productPanel.Width - 40,
                            BackColor = Color.DarkSlateGray,
                            Location = new Point(leftMargin, headerLabel.Bottom + labelSpacing)
                        };
                        productPanel.Controls.Add(separator);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelII = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelII);

                        // Exibe a imagem do usuário
                        PictureBox userPictureBox = new PictureBox
                        {
                            Size = new Size(200, 200), // Tamanho da imagem
                            Location = new Point(leftMargin, spacerPanelII.Bottom + labelSpacing),
                            BorderStyle = BorderStyle.None,
                            SizeMode = PictureBoxSizeMode.Zoom // Ajusta a imagem para caber no PictureBox
                        };

                        // Carrega a imagem fixa do projeto
                        userPictureBox.Image = Properties.Resources.UserAvatar; // Nome da imagem nos recursos do projeto

                        productPanel.Controls.Add(userPictureBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIII);

                        // Exibe o nome do usuário
                        Label nameLabel = new Label
                        {
                            Text = "Nome: " + reader["nome"].ToString(),
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(leftMargin, separator.Bottom + labelSpacing)
                        };
                        productPanel.Controls.Add(nameLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelIV = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIV);

                        // Exibe o email do usuário
                        Label emailLabel = new Label
                        {
                            Text = "Email: " + reader["email"].ToString(),
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(leftMargin, nameLabel.Bottom + labelSpacing)
                        };
                        productPanel.Controls.Add(emailLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelV = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelV);

                        // Exibe o CPF/Registro
                        Label cpfLabel = new Label
                        {
                            Text = "Registro: " + reader["registro_numero"].ToString(),
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(leftMargin, emailLabel.Bottom + labelSpacing)
                        };
                        productPanel.Controls.Add(cpfLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVI = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVI);

                        // Botão de Deletar
                        Button editAdmin = new Button
                        {
                            Text = "Editar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(450, 260),
                            BackColor = Color.CornflowerBlue,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        editAdmin.FlatAppearance.BorderSize = 0;
                        editAdmin.Click += (s, EventArgs) =>
                        {
                            ShowEditAdminForm(Int32.Parse(idUsuario));
                        };
                        productPanel.Controls.Add(editAdmin);

                        // Botão de Cancelar
                        Button cancelButton = new Button
                        {
                            Text = "Cancelar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(510, 200),
                            BackColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        cancelButton.FlatAppearance.BorderSize = 0;
                        cancelButton.Click += (s, args) =>
                        {
                            MenuInicial_Load(null, EventArgs.Empty);
                        };
                        productPanel.Controls.Add(cancelButton);
                    }
                    else
                    {
                        MessageBox.Show("Nenhum dado encontrado para o usuário.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar detalhes do perfil: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowEditAdminForm(int idAdministrador)
        {
            // Limpa o painel e exibe o formulário de editar administrador
            productPanel.Controls.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    SELECT u.nome, u.email, a.cpf, a.registro_numero
                    FROM administrador a
                    INNER JOIN usuario u ON u.usuario_id = a.usuario_id
                    WHERE u.usuario_id = @idAdministrador";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idAdministrador", idAdministrador);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        // Título da página
                        Label editAdminLabel = new Label
                        {
                            Text = "Editar Administrador",
                            Font = new Font("Segoe UI Variable Display", 16, FontStyle.Bold),
                            AutoSize = true,
                            Location = new Point(200, 20)
                        };
                        productPanel.Controls.Add(editAdminLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelI = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelI);

                        // Exibe a imagem do usuário
                        PictureBox userPictureBox = new PictureBox
                        {
                            Size = new Size(150, 150), // Ajuste o tamanho da imagem
                            Location = new Point(20, spacerPanelI.Bottom + 20), // Ajuste a posição da imagem
                            BorderStyle = BorderStyle.None,
                            SizeMode = PictureBoxSizeMode.Zoom // Ajusta a imagem para caber no PictureBox
                        };

                        userPictureBox.Image = Properties.Resources.UserAvatar; // Nome da imagem nos recursos do projeto

                        productPanel.Controls.Add(userPictureBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelII = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelII);

                        // Exibe o Nome do Administrador
                        Label nameLabel = new Label
                        {
                            Text = "Nome:",
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(50, spacerPanelI.Bottom + 180)
                        };
                        productPanel.Controls.Add(nameLabel);


                        TextBox nameTextBox = new TextBox
                        {
                            Text = reader["nome"].ToString(),
                            Size = new Size(200, 20),
                            Location = new Point(150, spacerPanelI.Bottom + 180),
                            Font = new Font("Segoe UI Variable Display", 10)
                        };
                        productPanel.Controls.Add(nameTextBox);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelIII = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIII);

                        // Exibe o Email do Administrador
                        Label emailLabel = new Label
                        {
                            Text = "Email: " + reader["email"].ToString(),
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(50, nameTextBox.Bottom + 10)
                        };
                        productPanel.Controls.Add(emailLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelIV = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelIV);

                        // Exibe o Número de Registro do Administrador
                        Label numRegistroLabel = new Label
                        {
                            Text = "Número de Registro: " + reader["registro_numero"].ToString(),
                            Font = new Font("Segoe UI Variable Display", 12, FontStyle.Regular),
                            AutoSize = true,
                            Location = new Point(50, emailLabel.Bottom + 10)
                        };
                        productPanel.Controls.Add(numRegistroLabel);

                        // Adiciona um espaçador abaixo do título
                        Panel spacerPanelVI = new Panel
                        {
                            Size = new Size(productPanel.Width - 20, 5),
                            BackColor = Color.Transparent
                        };
                        productPanel.Controls.Add(spacerPanelVI);

                        // Botão de Salvar
                        Button saveButton = new Button
                        {
                            Text = "Salvar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(150, numRegistroLabel.Bottom + 20),
                            BackColor = Color.Green,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        saveButton.FlatAppearance.BorderSize = 0;
                        saveButton.Click += (s, e) =>
                        {
                            // Valida e atualiza os dados do administrador
                            string newName = nameTextBox.Text;

                            UpdateAdmin(idAdministrador, newName);
                        };
                        productPanel.Controls.Add(saveButton);

                        // Botão de Cancelar
                        Button cancelButton = new Button
                        {
                            Text = "Cancelar",
                            Font = new Font("Segoe UI Variable Display", 10),
                            Size = new Size(100, 30),
                            Location = new Point(270, numRegistroLabel.Bottom + 20),
                            BackColor = Color.Red,
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat
                        };
                        cancelButton.FlatAppearance.BorderSize = 0;
                        cancelButton.Click += (s, e) =>
                        {
                            btnProfile_Click(null, EventArgs.Empty); // Volta para o perfil do administrador
                        };
                        productPanel.Controls.Add(cancelButton);
                    }
                    else
                    {
                        MessageBox.Show("Administrador não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar detalhes do administrador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateAdmin(int adminId, string newName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                    UPDATE usuario
                    SET nome = @nome
                    WHERE usuario_id = (
                        SELECT usuario_id
                        FROM administrador
                        WHERE usuario_id = @idAdministrador
                    );";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nome", newName);
                    command.Parameters.AddWithValue("@idAdministrador", adminId);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Administrador atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Tente recarregar o painel para atualizar as informações
                    btnProfile_Click(null, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar administrador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
