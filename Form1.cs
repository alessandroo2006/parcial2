using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1: Form




    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string tipoSeleccionado = comboTipo.SelectedItem.ToString();
            string rarezaSeleccionada = comboRareza.SelectedItem.ToString();

            var filtrados = bloques.Where(b =>
                (tipoSeleccionado == "Todos" || b.Tipo == tipoSeleccionado) &&
                (rarezaSeleccionada == "Todos" || b.Rareza == rarezaSeleccionada)
            ).ToList();

            listBloques.Items.Clear();
            foreach (var bloque in filtrados)
            {
                listBloques.Items.Add($"{bloque.Nombre} ({bloque.Tipo}, {bloque.Rareza})");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtClave.Text))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            Jugador jugador = new Jugador
            {
                Nombre = txtNombre.Text,
                Nivel = 1, // puedes cambiar esto según lógica
                FechaCreacion = DateTime.Now
            };

            _jugadorService.Crear(jugador);
            MessageBox.Show($"Jugador {jugador.Nombre} agregado con ID {jugador.Id}");
            LimpiarCampos();
            CargarJugadores();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtUsuario.Clear();
            txtClave.Clear();
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvJugadores.CurrentRow == null)
            {
                MessageBox.Show("Selecciona un jugador de la tabla.");
                return;
            }

            var jugador = new Jugador
            {
                Id = (int)dgvJugadores.CurrentRow.Cells["Id"].Value,
                Nombre = txtNombre.Text.Trim(),
                Usuario = txtUsuario.Text.Trim(),
                Clave = txtClave.Text.Trim()
            };

            _jugadorService.Modificar(jugador);
            CargarJugadores();
            LimpiarCampos();
        }
        
    }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboTipo.Items.AddRange(new string[] { "Todos", "Natural", "Mineral", "Decorativo" });
            comboTipo.SelectedIndex = 0;

         
        }

        private void Rareza_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboRareza.Items.AddRange(new string[] { "Todos", "Común", "Poco común", "Raro", "Muy raro" });
            comboRareza.SelectedIndex = 0;
        }
    }
}
