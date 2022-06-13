﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FreshStart.Modelo;
using System.Data.SQLite;
using FreshStart.Cache;
namespace FreshStart.Logica
{
    public class UsuarioLogica
    {
        private static string cadena = ConfigurationManager.ConnectionStrings["Cadena"].ConnectionString;

        private static UsuarioLogica _instancia = null;

        public UsuarioLogica()
        {

        }

        public static UsuarioLogica Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new UsuarioLogica();
                }
                return _instancia;
            }
        }

        public bool Guardar(PrecargaDB obj)
        {
            bool respuesta = true;

            using (SQLiteConnection conexion = new SQLiteConnection(cadena))
            {
                conexion.Open();
                string query = "insert into DATOS(Nombres,APaterno,AMaterno,DNacimiento,MNacimiento,ANacimiento,Contraseña,Correo,Usuario) values (@nombre,@apaterno,@amaterno,@dnacimiento,@mnacimiento,@anacimiento,@contraseña,@correo,@usuario)";
                SQLiteCommand cmd = new SQLiteCommand(query, conexion);
                cmd.Parameters.Add(new SQLiteParameter("@nombre", obj.Nombres));
                cmd.Parameters.Add(new SQLiteParameter("@apaterno", obj.APaterno));
                cmd.Parameters.Add(new SQLiteParameter("@amaterno", obj.AMaterno));
                cmd.Parameters.Add(new SQLiteParameter("@dnacimiento", obj.DNacimiento));
                cmd.Parameters.Add(new SQLiteParameter("@mnacimiento", obj.MNacimiento));
                cmd.Parameters.Add(new SQLiteParameter("@anacimiento", obj.ANacimiento));
                cmd.Parameters.Add(new SQLiteParameter("@contraseña", obj.Contraseña));
                cmd.Parameters.Add(new SQLiteParameter("@correo", obj.Correo));
                cmd.Parameters.Add(new SQLiteParameter("@usuario", obj.Usuario));
                cmd.CommandType = System.Data.CommandType.Text;

                if (cmd.ExecuteNonQuery() < 1)
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public bool Comparacion(string Usuario, string Contraseña)
        {
            bool registrado = false;

            SQLiteConnection conexionn = new SQLiteConnection(cadena);
            conexionn.Open();
            string queryy = "SELECT * from DATOS";
            SQLiteCommand cmd = new SQLiteCommand(queryy, conexionn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string tempUsuario = Convert.ToString(reader["Usuario"]);
                string tempContraseña = Convert.ToString(reader["Contraseña"]);
                if ((tempUsuario == Usuario) && (tempContraseña == Contraseña))
                {
                    UserCache.ID = Convert.ToInt32(reader["ID"]);
                    UserCache.Nombres = Convert.ToString(reader["Nombres"]);
                    UserCache.APaterno = Convert.ToString(reader["APaterno"]);
                    UserCache.AMaterno = Convert.ToString(reader["AMaterno"]);
                    UserCache.DNacimiento = Convert.ToString(reader["DNacimiento"]);
                    UserCache.MNacimiento = Convert.ToString(reader["MNacimiento"]);
                    UserCache.ANacimiento = Convert.ToString(reader["ANacimiento"]);
                    UserCache.Contraseña = Convert.ToString(reader["Contraseña"]);
                    UserCache.Correo = Convert.ToString(reader["Correo"]);
                    UserCache.Usuario = Convert.ToString(reader["Usuario"]);
                    UserCache.Login = true;
                    UserCache.Basica = Convert.ToString(reader["Basica"]);
                    UserCache.Intermedia = Convert.ToString(reader["Intermedia"]);
                    UserCache.Extra = Convert.ToString(reader["Extra"]);
                    registrado = true;
                }


            }
            conexionn.Close();
            return registrado;
        }
        public bool unicos(string usuario, string correo)
        {
            bool resultado = false;
            SQLiteConnection conexionn = new SQLiteConnection(cadena);
            conexionn.Open();
            string queryy = "SELECT * from DATOS";
            SQLiteCommand cmd = new SQLiteCommand(queryy, conexionn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string tempUsuario = Convert.ToString(reader["Usuario"]);
                string tempCorreo = Convert.ToString(reader["Correo"]);
                if ((tempUsuario == usuario) || (tempCorreo == correo))
                {
                    resultado = true;
                }
                
            }
            return resultado;
        }
    }
}
