using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;
using Modelo;

namespace Controlador
{
    public class ControladorUsuario
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Usuario> usuarios;
        static ControladorUsuario controladorUsuario = null;

        private ControladorUsuario()
        {
            usuarios = new List<Usuario>();
        }

        static public ControladorUsuario Instancia()
        {
            if (controladorUsuario == null)
                controladorUsuario = new ControladorUsuario();
            return controladorUsuario;
        }

        public bool agregarUsuario(Usuario usuario)
        {
            int idPersona = 0, numFilas = 0, numFilas2 = 0;
            OleDbDataReader r = null;
            usuarios.Clear();

            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into persona(nombres,apellidoPaterno,apellidoMaterno,dni,estado) " +
                                                        "values(@nombres,@apellidoPaterno,@apellidoMaterno,@dni,@estado)");

            OleDbCommand comando2 = new OleDbCommand("SELECT TOP 1 * FROM persona order by idPersona DESC");

            OleDbCommand comando3 = new OleDbCommand("insert into usuario(persona_idPersona,username,pass) " +
                                                        "values(@persona_idPersona,@username,@pass)");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres",usuario.persona.nombres),
                new OleDbParameter("@apellidoPaterno",usuario.persona.apellidoPaterno),
                new OleDbParameter("@apellidoMaterno",usuario.persona.apellidoMaterno),
                new OleDbParameter("@dni",usuario.persona.dni),
                new OleDbParameter("@estado",usuario.persona.estado),                
            });

            comando.Connection = conexion;
            comando2.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
                r = comando2.ExecuteReader();

                while (r.Read())
                {
                    idPersona = r.GetInt32(0);
                }

                comando3.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@persona_idPersona",idPersona),
                    new OleDbParameter("@username",usuario.username),
                    new OleDbParameter("@pass",usuario.password), 
                });

                comando3.Connection = conexion;
                numFilas2 = comando3.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return numFilas + numFilas2 == 2;
        }

        public List<Usuario> getListaUsuarios(string username, string nombres, string apellidoPaterno, string apellidoMaterno)
        {
            usuarios.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("select * from persona, usuario where usuario.username like @username and persona.nombres like @nombres and persona.apellidoPaterno like @apellidoPaterno and persona.apellidoMaterno like @apellidoMaterno and persona.estado='activo' and persona.idPersona = usuario.persona_idPersona order by persona.idPersona ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@username","%"+username+"%"),
                new OleDbParameter("@nombres","%"+nombres+"%"),
                new OleDbParameter("@apellidoPaterno","%"+apellidoPaterno+"%"),
                new OleDbParameter("@apellidoMaterno","%"+apellidoMaterno+"%"),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Persona persona = new Persona();
                    persona.idPersona = r.GetInt32(0);
                    persona.nombres = r.GetString(1);
                    persona.apellidoPaterno = r.GetString(2);
                    persona.apellidoMaterno = r.GetString(3);
                    persona.dni = r.GetInt32(4);
                    persona.estado = r.GetString(5);
                    Usuario usuario = new Usuario();
                    usuario.persona = persona;
                    usuario.username = r.GetString(7);
                    usuario.password = r.GetString(8);

                    usuarios.Add(usuario);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return usuarios;
        }

        public bool modificarUsuario(Usuario usuario)
        {
            int numFilas = 0, numFilas2 = 0;
            usuarios.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update persona set nombres=@nombres,apellidoPaterno=@apellidoPaterno,apellidoMaterno=@apellidoMaterno,dni=@dni " +
                                                    "where idPersona=@idPersona");
            OleDbCommand comando2 = new OleDbCommand("update usuario set username=@username,pass=@pass " +
                                                        "where persona_idPersona=@persona_idPersona");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres",usuario.persona.nombres),
                new OleDbParameter("@apellidoPaterno",usuario.persona.apellidoPaterno),
                new OleDbParameter("@apellidoMaterno",usuario.persona.apellidoMaterno),
                new OleDbParameter("@dni",usuario.persona.dni),
                new OleDbParameter("@idPersona",usuario.persona.idPersona),                
            });

            comando2.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@username",usuario.username),
                new OleDbParameter("@pass",usuario.password),                
                new OleDbParameter("@persona_idPersona",usuario.persona.idPersona),                
            });

            comando.Connection = conexion;
            comando2.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
                numFilas2 = comando2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas + numFilas2 == 2;
        }

        public bool eliminarUsuario(Usuario usuario)
        {
            int numFilas = 0;
            usuarios.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update persona set estado=@estado " +
                                                    "where idPersona=@idPersona");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","inactivo"),
                new OleDbParameter("@idPersona",usuario.persona.idPersona),                               
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }
    }

    public class ControladorPaciente
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Paciente> pacientes;
        static ControladorPaciente controladorPaciente = null;

        private ControladorPaciente()
        {
            pacientes = new List<Paciente>();
        }

        static public ControladorPaciente Instancia()
        {
            if (controladorPaciente == null)
                controladorPaciente = new ControladorPaciente();
            return controladorPaciente;
        }

        public Paciente getPaciente(int idPaciente)
        {
            Paciente p = new Paciente();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("select * from persona, paciente, menorEdad, mayorEdad, cita where persona.idPersona = paciente.persona_idPersona and paciente.persona_idPersona = menorEdad.paciente_persona_idPersona and paciente.persona_idPersona = mayorEdad.paciente_persona_idPersona and paciente.persona_idPersona = @idPaciente order by paciente.numeroHistoria ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {                
                new OleDbParameter("@idPaciente",idPaciente),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Persona persona = new Persona();
                    persona.idPersona = r.GetInt32(0);
                    persona.nombres = r.GetString(1);
                    persona.apellidoPaterno = r.GetString(2);
                    persona.apellidoMaterno = r.GetString(3);
                    persona.dni = r.GetInt32(4);
                    persona.estado = r.GetString(5);
                    p.numeroHistoria = r.GetInt32(7);
                    p.fechaNacimiento = r.GetDateTime(8);
                    p.lugarNacimiento = r.GetString(9);
                    p.domicilio = r.GetString(10);
                    p.distrito = r.GetString(11);
                    p.telefonoCasa = r.GetString(12);
                    p.correo = r.GetString(13);
                    p.comoEntero = r.GetString(14);
                    p.observacion = r.GetString(15);
                    MenorEdad menorEdad = new MenorEdad();
                    menorEdad.nombreMadre = r.GetString(17);
                    menorEdad.nombrePadre = r.GetString(18);
                    menorEdad.celularMadre = r.GetString(19);
                    menorEdad.celularPadre = r.GetString(20);
                    menorEdad.escolaridad = r.GetString(21);
                    menorEdad.nombreColegio = r.GetString(22);
                    menorEdad.ubicacionColegio = r.GetString(23);
                    MayorEdad mayorEdad = new MayorEdad();
                    mayorEdad.celular = r.GetString(25);
                    mayorEdad.ocupacion = r.GetString(26);
                    mayorEdad.gradoInstruccion = r.GetString(27);
                    mayorEdad.lugarLaboral = r.GetString(28);
                    p.persona = persona;
                    p.menorEdad = menorEdad;
                    p.mayorEdad = mayorEdad;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return p;
        }

        public List<Paciente> getListaPacientes(string strHistoria, string strDNI, string nombres, string apellidoPaterno, string apellidoMaterno)
        {
            pacientes.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            string hist = "", dn = "";
            int numeroHistoria = 0, dni = 0;

            if (!strHistoria.Equals(""))
            {
                hist = " and paciente.numeroHistoria = @numeroHistoria";
                numeroHistoria = int.Parse(strHistoria);
            }
            if (!strDNI.Equals(""))
            {
                dn = " and persona.dni = @dni";
                dni = int.Parse(strDNI);
            }

            OleDbCommand comando = new OleDbCommand("select * from persona, paciente, menorEdad, mayorEdad where persona.idPersona = paciente.persona_idPersona and paciente.persona_idPersona = menorEdad.paciente_persona_idPersona and paciente.persona_idPersona = mayorEdad.paciente_persona_idPersona and  persona.nombres like @nombres and persona.apellidoPaterno like @apellidoPaterno and persona.apellidoMaterno like @apellidoMaterno and persona.estado like 'activo' " + hist + dn + " order by paciente.numeroHistoria ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {                
                new OleDbParameter("@nombres","%"+nombres+"%"),
                new OleDbParameter("@apellidoPaterno","%"+apellidoPaterno+"%"),
                new OleDbParameter("@apellidoMaterno","%"+apellidoMaterno+"%"),
            });

            if (!strHistoria.Equals(""))
            {
                comando.Parameters.AddWithValue("@numeroHistoria", numeroHistoria);
            }
            if (!strDNI.Equals(""))
            {
                comando.Parameters.AddWithValue("@dni", dni);
            }

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Persona persona = new Persona();
                    persona.idPersona = r.GetInt32(0);
                    persona.nombres = r.GetString(1);
                    persona.apellidoPaterno = r.GetString(2);
                    persona.apellidoMaterno = r.GetString(3);
                    persona.dni = r.GetInt32(4);
                    persona.estado = r.GetString(5);
                    Paciente paciente = new Paciente();
                    paciente.numeroHistoria = r.GetInt32(7);
                    paciente.fechaNacimiento = r.GetDateTime(8);
                    paciente.lugarNacimiento = r.GetString(9);
                    paciente.domicilio = r.GetString(10);
                    paciente.distrito = r.GetString(11);
                    paciente.telefonoCasa = r.GetString(12);
                    paciente.correo = r.GetString(13);
                    paciente.comoEntero = r.GetString(14);
                    paciente.observacion = r.GetString(15);
                    MenorEdad menorEdad = new MenorEdad();
                    menorEdad.nombreMadre = r.GetString(17);
                    menorEdad.nombrePadre = r.GetString(18);
                    menorEdad.celularMadre = r.GetString(19);
                    menorEdad.celularPadre = r.GetString(20);
                    menorEdad.escolaridad = r.GetString(21);
                    menorEdad.nombreColegio = r.GetString(22);
                    menorEdad.ubicacionColegio = r.GetString(23);
                    MayorEdad mayorEdad = new MayorEdad();
                    mayorEdad.celular = r.GetString(25);
                    mayorEdad.ocupacion = r.GetString(26);
                    mayorEdad.gradoInstruccion = r.GetString(27);
                    mayorEdad.lugarLaboral = r.GetString(28);
                    paciente.persona = persona;
                    paciente.menorEdad = menorEdad;
                    paciente.mayorEdad = mayorEdad;

                    pacientes.Add(paciente);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return pacientes;
        }

        public bool agregarPaciente(Paciente paciente)
        {
            int idPersona = 0, numeroHistoria = 0, numFilas1 = 0, numFilas2 = 0, numFilas3 = 0, numFilas4 = 0;
            OleDbDataReader r = null;
            pacientes.Clear();

            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into persona(nombres,apellidoPaterno,apellidoMaterno,dni,estado) " +
                                                        "values(@nombres,@apellidoPaterno,@apellidoMaterno,@dni,@estado)");

            OleDbCommand comando2 = new OleDbCommand("SELECT TOP 1 * FROM persona order by idPersona DESC");
            OleDbCommand comando4 = new OleDbCommand("SELECT TOP 1 * FROM paciente order by persona_idPersona DESC");

            OleDbCommand comando3 = new OleDbCommand("insert into paciente(persona_idPersona,numeroHistoria,fechaNacimiento,lugarNacimiento,domicilio,distrito,telefonoCasa,correo,comoEntero,observacion) " +
                                                        "values(@persona_idPersona,@numeroHistoria,@fechaNacimiento,@lugarNacimiento,@domicilio,@distrito,@telefonoCasa,@correo,@comoEntero,@observacion)");


            OleDbCommand comando5 = new OleDbCommand("insert into menorEdad(paciente_persona_idPersona,nombreMadre,nombrePadre,celularMadre,celularPadre,escolaridad,nombreColegio,ubicacionColegio) " +
                                                        "values(@paciente_persona_idPersona,@nombreMadre,@nombrePadre,@celularMadre,@celularPadre,@escolaridad,@nombreColegio,@ubicacionColegio)");

            OleDbCommand comando6 = new OleDbCommand("insert into mayorEdad(paciente_persona_idPersona,celular,ocupacion,gradoInstruccion,lugarLaboral) " +
                                                        "values(@paciente_persona_idPersona,@celular,@ocupacion,@gradoInstruccion,@lugarLaboral)");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres",paciente.persona.nombres),
                new OleDbParameter("@apellidoPaterno",paciente.persona.apellidoPaterno),
                new OleDbParameter("@apellidoMaterno",paciente.persona.apellidoMaterno),
                new OleDbParameter("@dni",paciente.persona.dni),
                new OleDbParameter("@estado",paciente.persona.estado),                
            });

            comando.Connection = conexion;
            comando2.Connection = conexion;
            comando4.Connection = conexion;
            try
            {
                conexion.Open();
                numFilas1 = comando.ExecuteNonQuery();
                r = comando2.ExecuteReader();

                while (r.Read())
                {
                    idPersona = r.GetInt32(0);
                }

                r = comando4.ExecuteReader();
                while (r.Read())
                {
                    numeroHistoria = r.GetInt32(1) + 1;
                }

                comando3.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@persona_idPersona",idPersona),
                    new OleDbParameter("@numeroHistoria",numeroHistoria),
                    new OleDbParameter("@fechaNacimiento",paciente.fechaNacimiento),
                    new OleDbParameter("@lugarNacimiento",paciente.lugarNacimiento),
                    new OleDbParameter("@domicilio",paciente.domicilio),
                    new OleDbParameter("@distrito",paciente.distrito),
                    new OleDbParameter("@telefonoCasa",paciente.telefonoCasa),                    
                    new OleDbParameter("@correo",paciente.correo), 
                    new OleDbParameter("@comoEntero",paciente.comoEntero),
                    new OleDbParameter("@observacion","-"),
                });

                comando5.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@paciente_persona_idPersona",idPersona),
                    new OleDbParameter("@nombreMadre",paciente.menorEdad.nombreMadre),
                    new OleDbParameter("@nombrePadre",paciente.menorEdad.nombrePadre),
                    new OleDbParameter("@celularMadre",paciente.menorEdad.celularMadre),
                    new OleDbParameter("@celularPadre",paciente.menorEdad.celularPadre),
                    new OleDbParameter("@escolaridad",paciente.menorEdad.escolaridad),
                    new OleDbParameter("@nombreColegio",paciente.menorEdad.nombreColegio),
                    new OleDbParameter("@ubicacionColegio",paciente.menorEdad.ubicacionColegio),
                });

                comando6.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@paciente_persona_idPersona",idPersona),
                    new OleDbParameter("@celular",paciente.mayorEdad.celular),
                    new OleDbParameter("@ocupacion",paciente.mayorEdad.ocupacion),
                    new OleDbParameter("@gradoInstruccion",paciente.mayorEdad.gradoInstruccion),
                    new OleDbParameter("@lugarLaboral",paciente.mayorEdad.lugarLaboral),
                });

                comando3.Connection = conexion;
                comando5.Connection = conexion;
                comando6.Connection = conexion;

                numFilas2 = comando3.ExecuteNonQuery();
                numFilas3 = comando5.ExecuteNonQuery();
                numFilas4 = comando6.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return numFilas1 + numFilas2 + numFilas3 + numFilas4 == 4;
        }

        public bool modificarPaciente(Paciente paciente)
        {
            int numFilas1 = 0, numFilas2 = 0, numFilas3 = 0, numFilas4 = 0;
            pacientes.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando1 = new OleDbCommand("update persona set nombres=@nombres,apellidoPaterno=@apellidoPaterno,apellidoMaterno=@apellidoMaterno,dni=@dni " +
                                                    "where idPersona=@idPersona");
            OleDbCommand comando2 = new OleDbCommand("update paciente set fechaNacimiento=@fechaNacimiento,lugarNacimiento=@lugarNacimiento,domicilio=@domicilio,distrito=@distrito,telefonoCasa=@telefonoCasa,correo=@correo,comoEntero=@comoEntero " +
                                                        "where persona_idPersona=@persona_idPersona");
            OleDbCommand comando3 = new OleDbCommand("update menorEdad set nombreMadre=@nombreMadre,nombrePadre=@nombrePadre,celularMadre=@celularMadre,celularPadre=@celularPadre,escolaridad=@escolaridad,nombreColegio=@nombreColegio,ubicacionColegio=@ubicacionColegio " +
                                                        "where paciente_persona_idPersona=@paciente_persona_idPersona");
            OleDbCommand comando4 = new OleDbCommand("update mayorEdad set celular=@celular,ocupacion=@ocupacion,gradoInstruccion=@gradoInstruccion,lugarLaboral=@lugarLaboral " +
                                                        "where paciente_persona_idPersona=@paciente_persona_idPersona");
            comando1.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres",paciente.persona.nombres),
                new OleDbParameter("@apellidoPaterno",paciente.persona.apellidoPaterno),
                new OleDbParameter("@apellidoMaterno",paciente.persona.apellidoMaterno),
                new OleDbParameter("@dni",paciente.persona.dni),
                new OleDbParameter("@idPersona",paciente.persona.idPersona),                
            });

            comando2.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@fechaNacimiento",paciente.fechaNacimiento),
                new OleDbParameter("@lugarNacimiento",paciente.lugarNacimiento),                
                new OleDbParameter("@domicilio",paciente.domicilio),
                new OleDbParameter("@distrito",paciente.distrito),
                new OleDbParameter("@telefonoCasa",paciente.telefonoCasa),                
                new OleDbParameter("@correo",paciente.correo),
                new OleDbParameter("@comoEntero",paciente.comoEntero),
                new OleDbParameter("@persona_idPersona",paciente.persona.idPersona),
            });

            comando3.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombreMadre",paciente.menorEdad.nombreMadre),
                new OleDbParameter("@nombrePadre",paciente.menorEdad.nombrePadre),                
                new OleDbParameter("@celularMadre",paciente.menorEdad.celularMadre),
                new OleDbParameter("@celularPadre",paciente.menorEdad.celularPadre),
                new OleDbParameter("@escolaridad",paciente.menorEdad.escolaridad),
                new OleDbParameter("@nombreColegio",paciente.menorEdad.nombreColegio),
                new OleDbParameter("@ubicacionColegio",paciente.menorEdad.ubicacionColegio),                
                new OleDbParameter("@paciente_persona_idPersona",paciente.persona.idPersona),
            });

            comando4.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@celular",paciente.mayorEdad.celular),
                new OleDbParameter("@ocupacion",paciente.mayorEdad.ocupacion),                
                new OleDbParameter("@gradoInstruccion",paciente.mayorEdad.gradoInstruccion),
                new OleDbParameter("@lugarLaboral",paciente.mayorEdad.lugarLaboral),                
                new OleDbParameter("@paciente_persona_idPersona",paciente.persona.idPersona),
            });

            comando1.Connection = conexion;
            comando2.Connection = conexion;
            comando3.Connection = conexion;
            comando4.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas1 = comando1.ExecuteNonQuery();
                numFilas2 = comando2.ExecuteNonQuery();
                numFilas3 = comando3.ExecuteNonQuery();
                numFilas4 = comando4.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas1 + numFilas2 + numFilas3 + numFilas4 == 4;
        }

        public bool eliminarPaciente(Paciente paciente)
        {
            int numFilas = 0;
            pacientes.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update persona set estado=@estado " +
                                                    "where idPersona=@idPersona");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","inactivo"),
                new OleDbParameter("@idPersona",paciente.persona.idPersona),                               
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }
    }

    public class ControladorTerapeuta
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Terapeuta> terapeutas;
        static ControladorTerapeuta controladorTerapeuta = null;

        private ControladorTerapeuta()
        {
            terapeutas = new List<Terapeuta>();
        }

        static public ControladorTerapeuta Instancia()
        {
            if (controladorTerapeuta == null)
                controladorTerapeuta = new ControladorTerapeuta();
            return controladorTerapeuta;
        }

        public Terapeuta getTerapeuta(int idTerapeuta)
        {
            Terapeuta t = new Terapeuta();
            OleDbDataReader r = null;
            ControladorServicio controladorServicio = ControladorServicio.Instancia();
            ControladorHorarioTerapeuta controladorHorarioTerapeuta = ControladorHorarioTerapeuta.Instancia();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("select * from persona, terapeuta where persona.idPersona = terapeuta.persona_idPersona and terapeuta.persona_idPersona = @idTerapeuta order by persona.idPersona ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idTerapeuta",idTerapeuta),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Persona persona = new Persona();
                    List<Servicio> serviciosxTerapeuta = new List<Servicio>();
                    List<HorarioTerapeuta> horariosxTerapeuta = new List<HorarioTerapeuta>();
                    persona.idPersona = r.GetInt32(0);
                    serviciosxTerapeuta = controladorServicio.getListaServiciosxTerapeuta(persona.idPersona);
                    horariosxTerapeuta = controladorHorarioTerapeuta.getListaHorariosxTerapeuta(persona.idPersona);
                    persona.nombres = r.GetString(1);
                    persona.apellidoPaterno = r.GetString(2);
                    persona.apellidoMaterno = r.GetString(3);
                    persona.dni = r.GetInt32(4);
                    persona.estado = r.GetString(5);
                    t.persona = persona;
                    t.fechaNacimiento = r.GetDateTime(7);
                    t.telefono = r.GetString(8);
                    t.servicios = serviciosxTerapeuta;
                    t.horarioTerapeuta = horariosxTerapeuta;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return t;
        }

        public List<Terapeuta> getListaTerapeutas(string nombres, string apellidoPaterno, string apellidoMaterno, string strDNI)
        {
            string dn = "";
            int dni = 0;
            terapeutas.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            if (!strDNI.Equals(""))
            {
                dn = " and persona.dni = @dni";
                dni = int.Parse(strDNI);
            }

            OleDbCommand comando = new OleDbCommand("select * from persona, terapeuta where persona.nombres like @nombres and persona.apellidoPaterno like @apellidoPaterno and persona.apellidoMaterno like @apellidoMaterno and persona.estado='activo' and " + dn + " persona.idPersona = terapeuta.persona_idPersona order by persona.idPersona ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres","%"+nombres+"%"),               
                new OleDbParameter("@apellidoPaterno","%"+apellidoPaterno+"%"),
                new OleDbParameter("@apellidoMaterno","%"+apellidoMaterno+"%"),                
            });

            if (!strDNI.Equals(""))
            {
                comando.Parameters.AddWithValue("@dni", dni);
            }

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Persona persona = new Persona();
                    List<Servicio> serviciosxTerapeuta = new List<Servicio>();
                    List<HorarioTerapeuta> horariosxTerapeuta = new List<HorarioTerapeuta>();
                    ControladorServicio controladorServicio = ControladorServicio.Instancia();
                    ControladorHorarioTerapeuta controladorHorarioTerapeuta = ControladorHorarioTerapeuta.Instancia();
                    persona.idPersona = r.GetInt32(0);
                    serviciosxTerapeuta = controladorServicio.getListaServiciosxTerapeuta(persona.idPersona);
                    horariosxTerapeuta = controladorHorarioTerapeuta.getListaHorariosxTerapeuta(persona.idPersona);
                    persona.nombres = r.GetString(1);
                    persona.apellidoPaterno = r.GetString(2);
                    persona.apellidoMaterno = r.GetString(3);
                    persona.dni = r.GetInt32(4);
                    persona.estado = r.GetString(5);
                    Terapeuta terapeuta = new Terapeuta();
                    terapeuta.persona = persona;
                    terapeuta.fechaNacimiento = r.GetDateTime(7);
                    terapeuta.telefono = r.GetString(8);
                    terapeuta.servicios = serviciosxTerapeuta;
                    terapeuta.horarioTerapeuta = horariosxTerapeuta;

                    terapeutas.Add(terapeuta);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return terapeutas;
        }

        public List<Terapeuta> getListaTerapeutasxServicio(int idServicio)
        {
            terapeutas.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("SELECT * from persona, terapeuta, servicioxterapeuta where persona.idPersona = terapeuta.persona_idPersona and terapeuta.persona_idPersona = servicioxterapeuta.persona_idPersona and servicioxterapeuta.idServicio = @idServicio order by persona.idPersona asc");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idServicio",idServicio),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Persona persona = new Persona();
                    List<Servicio> serviciosxTerapeuta = new List<Servicio>();
                    List<HorarioTerapeuta> horariosxTerapeuta = new List<HorarioTerapeuta>();
                    ControladorServicio controladorServicio = ControladorServicio.Instancia();
                    ControladorHorarioTerapeuta controladorHorarioTerapeuta = ControladorHorarioTerapeuta.Instancia();
                    persona.idPersona = r.GetInt32(0);
                    serviciosxTerapeuta = controladorServicio.getListaServiciosxTerapeuta(persona.idPersona);
                    horariosxTerapeuta = controladorHorarioTerapeuta.getListaHorariosxTerapeuta(persona.idPersona);
                    persona.nombres = r.GetString(1);
                    persona.apellidoPaterno = r.GetString(2);
                    persona.apellidoMaterno = r.GetString(3);
                    persona.dni = r.GetInt32(4);
                    persona.estado = r.GetString(5);
                    Terapeuta terapeuta = new Terapeuta();
                    terapeuta.persona = persona;
                    terapeuta.fechaNacimiento = r.GetDateTime(7);
                    terapeuta.telefono = r.GetString(8);
                    terapeuta.servicios = serviciosxTerapeuta;
                    terapeuta.horarioTerapeuta = horariosxTerapeuta;

                    terapeutas.Add(terapeuta);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return terapeutas;
        }

        public bool agregarTerapeuta(Terapeuta terapeuta)
        {
            int idPersona = 0, numFilas = 0, numFilas2 = 0, numFilas4 = 0, numFilas5 = 0;
            OleDbDataReader r = null;
            terapeutas.Clear();

            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into persona(nombres,apellidoPaterno,apellidoMaterno,dni,estado) " +
                                                        "values(@nombres,@apellidoPaterno,@apellidoMaterno,@dni,@estado)");

            OleDbCommand comando2 = new OleDbCommand("SELECT TOP 1 * FROM persona order by idPersona DESC");

            OleDbCommand comando3 = new OleDbCommand("insert into terapeuta(persona_idPersona,fechaNacimiento,telefono) " +
                                                        "values(@persona_idPersona,@fechaNacimiento,@telefono)");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres",terapeuta.persona.nombres),
                new OleDbParameter("@apellidoPaterno",terapeuta.persona.apellidoPaterno),
                new OleDbParameter("@apellidoMaterno",terapeuta.persona.apellidoMaterno),
                new OleDbParameter("@dni",terapeuta.persona.dni),
                new OleDbParameter("@estado",terapeuta.persona.estado),                
            });

            comando.Connection = conexion;
            comando2.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
                r = comando2.ExecuteReader();

                while (r.Read())
                {
                    idPersona = r.GetInt32(0);
                }

                comando3.Parameters.AddRange(new OleDbParameter[]
                {
                    new OleDbParameter("@persona_idPersona",idPersona),
                    new OleDbParameter("@fechaNacimiento",terapeuta.fechaNacimiento),
                    new OleDbParameter("@telefono",terapeuta.telefono), 
                });

                comando3.Connection = conexion;

                numFilas2 = comando3.ExecuteNonQuery();

                foreach (Servicio s in terapeuta.servicios)
                {
                    OleDbCommand comando4 = new OleDbCommand("insert into servicioxterapeuta(idServicio,persona_idPersona) " +
                                                        "values(@idServicio,@persona_idPersona)");

                    comando4.Parameters.AddRange(new OleDbParameter[]
                    {
                        new OleDbParameter("@idServicio",s.idServicio),
                        new OleDbParameter("@persona_idPersona",idPersona),                                                
                    });
                    comando4.Connection = conexion;
                    numFilas4 = numFilas4 + comando4.ExecuteNonQuery();
                }

                foreach (HorarioTerapeuta horarioTerapeuta in terapeuta.horarioTerapeuta)
                {
                    OleDbCommand comando5 = new OleDbCommand("insert into horarioTerapeuta(horaInicio,horaFin,dia,terapeuta_persona_idPersona) " +
                                                        "values(@horaInicio,@horaFin,@dia,@terapeuta_persona_idPersona)");

                    comando5.Parameters.AddRange(new OleDbParameter[]
                    {
                        new OleDbParameter("@horaInicio",horarioTerapeuta.horaInicio),
                        new OleDbParameter("@horaFin",horarioTerapeuta.horaFin),
                        new OleDbParameter("@dia",horarioTerapeuta.dia),
                        new OleDbParameter("@terapeuta_persona_idPersona",idPersona),                                                
                    });

                    comando5.Connection = conexion;
                    numFilas5 = numFilas5 + comando5.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return numFilas + numFilas2 == 2 && terapeuta.servicios.Count == numFilas4 && numFilas5 == 6;
        }

        public bool modificarTerapeuta(Terapeuta terapeuta)
        {
            int numFilas = 0, numFilas2 = 0, numFilas3 = 0, numFilas4 = 0, numFilas5 = 0;
            List<Servicio> serviciosxTerapeuta = new List<Servicio>();
            ControladorServicio controladorServicio = ControladorServicio.Instancia();
            serviciosxTerapeuta = controladorServicio.getListaServiciosxTerapeuta(terapeuta.persona.idPersona);

            terapeutas.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando1 = new OleDbCommand("update persona set nombres=@nombres,apellidoPaterno=@apellidoPaterno,apellidoMaterno=@apellidoMaterno,dni=@dni " +
                                                    "where idPersona=@idPersona");
            OleDbCommand comando2 = new OleDbCommand("update terapeuta set fechaNacimiento=@fechaNacimiento,telefono=@telefono " + "where persona_idPersona=@persona_idPersona");

            OleDbCommand comando3 = new OleDbCommand("delete from servicioxterapeuta " + "where persona_idPersona=@persona_idPersona");


            comando1.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres",terapeuta.persona.nombres),
                new OleDbParameter("@apellidoPaterno",terapeuta.persona.apellidoPaterno),
                new OleDbParameter("@apellidoMaterno",terapeuta.persona.apellidoMaterno),
                new OleDbParameter("@dni",terapeuta.persona.dni),
                new OleDbParameter("@idPersona",terapeuta.persona.idPersona),
            });

            comando2.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@fechaNacimiento",terapeuta.fechaNacimiento),
                new OleDbParameter("@telefono",terapeuta.telefono),
                new OleDbParameter("@persona_idPersona",terapeuta.persona.idPersona),
            });

            comando3.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@persona_idPersona",terapeuta.persona.idPersona),
            });

            comando1.Connection = conexion;
            comando2.Connection = conexion;
            comando3.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando1.ExecuteNonQuery();
                numFilas2 = comando2.ExecuteNonQuery();
                numFilas3 = comando3.ExecuteNonQuery();

                foreach (Servicio s in terapeuta.servicios)
                {
                    OleDbCommand comando4 = new OleDbCommand("insert into servicioxterapeuta(idServicio,persona_idPersona) " + "values(@idServicio,@persona_idPersona)");

                    comando4.Parameters.AddRange(new OleDbParameter[]
                    {
                        new OleDbParameter("@idServicio",s.idServicio),
                        new OleDbParameter("@persona_idPersona",terapeuta.persona.idPersona),                                                
                    });
                    comando4.Connection = conexion;
                    numFilas4 = numFilas4 + comando4.ExecuteNonQuery();
                }

                foreach (HorarioTerapeuta horarioTerapeuta in terapeuta.horarioTerapeuta)
                {
                    OleDbCommand comando5 = new OleDbCommand("update horarioTerapeuta set horaInicio=@horaInicio,horaFin=@horaFin " +
                                                        "where idHorarioTerapeuta=@idHorarioTerapeuta");

                    comando5.Parameters.AddRange(new OleDbParameter[]
                    {
                        new OleDbParameter("@horaInicio",horarioTerapeuta.horaInicio),
                        new OleDbParameter("@horaFin",horarioTerapeuta.horaFin),
                        new OleDbParameter("@idHorarioTerapeuta",horarioTerapeuta.idHorarioTerapeuta),                                                                     
                    });

                    comando5.Connection = conexion;
                    numFilas5 = numFilas5 + comando5.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas + numFilas2 == 2 && numFilas4 == terapeuta.servicios.Count && numFilas3 == serviciosxTerapeuta.Count && numFilas5 == 6;
        }

        public bool eliminarTerapeuta(Terapeuta terapeuta)
        {
            int numFilas = 0;
            terapeutas.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update persona set estado=@estado " +
                                                    "where idPersona=@idPersona");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","inactivo"),
                new OleDbParameter("@idPersona",terapeuta.persona.idPersona),                               
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }
    }

    public class ControladorServicio
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Servicio> servicios;
        static ControladorServicio controladorServicio = null;

        private ControladorServicio()
        {
            servicios = new List<Servicio>();
        }

        static public ControladorServicio Instancia()
        {
            if (controladorServicio == null)
                controladorServicio = new ControladorServicio();
            return controladorServicio;
        }

        public List<Servicio> getListaServiciosxTerapeuta(int idTerapeuta)
        {
            List<Servicio> serviciosxTerapeuta = new List<Servicio>();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("SELECT * from servicio, terapeuta, servicioxterapeuta where servicio.idServicio = servicioxterapeuta.idServicio and  servicioxterapeuta.persona_idPersona = terapeuta.persona_idPersona and terapeuta.persona_idPersona = @idTerapeuta order by servicio.idServicio ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idTerapeuta", idTerapeuta),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Servicio servicio = new Servicio();
                    servicio.idServicio = r.GetInt32(0);
                    servicio.nombreServicio = r.GetString(1);

                    serviciosxTerapeuta.Add(servicio);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return serviciosxTerapeuta;
        }

        public List<Servicio> getListaServicios(string nombreServicio)
        {
            servicios.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("select * from servicio where nombreServicio like @nombreServicio and estado='activo' order by idServicio ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombreServicio","%" + nombreServicio + "%"),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Servicio servicio = new Servicio();
                    servicio.idServicio = r.GetInt32(0);
                    servicio.nombreServicio = r.GetString(1);
                    servicio.intervaloHora = r.GetInt32(2);
                    servicio.costo = r.GetFloat(3);
                    servicio.maximoPacientes = r.GetInt32(4);
                    servicio.estado = r.GetString(5);

                    servicios.Add(servicio);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return servicios;
        }

        public bool agregarServicio(Servicio servicio)
        {
            int numFilas = 0;
            servicios.Clear();

            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into servicio(nombreServicio,intervaloHora,costo,maximoPacientes,estado) " +
                                                        "values(@nombreServicio,@intervaloHora,@costo,@maximoPacientes,@estado)");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombreServicio",servicio.nombreServicio),
                new OleDbParameter("@intervaloHora",servicio.intervaloHora),
                new OleDbParameter("@costo",servicio.costo),
                new OleDbParameter("@maximoPacientes",servicio.maximoPacientes),
                new OleDbParameter("@estado",servicio.estado),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }

        public bool modificarServicio(Servicio servicio)
        {
            int numFilas = 0;
            servicios.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update servicio set nombreServicio=@nombreServicio,intervaloHora=@intervaloHora,costo=@costo,maximoPacientes=@maximoPacientes " +
                                                    "where idServicio=@idServicio");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombreServicio",servicio.nombreServicio),
                new OleDbParameter("@intervaloHora",servicio.intervaloHora),
                new OleDbParameter("@costo",servicio.costo),
                new OleDbParameter("@maximoPacientes",servicio.maximoPacientes),
                new OleDbParameter("@idServicio",servicio.idServicio),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }

        public bool eliminarServicio(Servicio servicio)
        {
            int numFilas = 0;
            servicios.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update servicio set estado=@estado " +
                                                    "where idServicio=@idServicio");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","inactivo"),
                new OleDbParameter("@idServicio",servicio.idServicio),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }

    }

    public class ControladorHorarioTerapeuta
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<HorarioTerapeuta> horarioTerapeutas;
        static ControladorHorarioTerapeuta controladorHorarioTerapeuta = null;

        private ControladorHorarioTerapeuta()
        {
            horarioTerapeutas = new List<HorarioTerapeuta>();
        }

        static public ControladorHorarioTerapeuta Instancia()
        {
            if (controladorHorarioTerapeuta == null)
                controladorHorarioTerapeuta = new ControladorHorarioTerapeuta();
            return controladorHorarioTerapeuta;
        }

        public List<HorarioTerapeuta> getListaHorariosxTerapeuta(int idTerapeuta)
        {
            List<HorarioTerapeuta> horarios = new List<HorarioTerapeuta>();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("SELECT * from horarioTerapeuta where terapeuta_persona_idPersona=@terapeuta_persona_idPersona order by idHorarioTerapeuta ASC");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@terapeuta_persona_idPersona", idTerapeuta),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    HorarioTerapeuta horarioTerapeuta = new HorarioTerapeuta();
                    horarioTerapeuta.idHorarioTerapeuta = r.GetInt32(0);
                    horarioTerapeuta.horaInicio = r.GetDateTime(1);
                    horarioTerapeuta.horaFin = r.GetDateTime(2);
                    horarioTerapeuta.dia = r.GetString(3);
                    horarioTerapeuta.idPersona = r.GetInt32(4);

                    horarios.Add(horarioTerapeuta);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return horarios;
        }
    }

    public class ControladorModalidad
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Modalidad> modalidades;
        static ControladorModalidad controladorModalidad = null;

        private ControladorModalidad()
        {
            modalidades = new List<Modalidad>();
        }

        static public ControladorModalidad Instancia()
        {
            if (controladorModalidad == null)
                controladorModalidad = new ControladorModalidad();
            return controladorModalidad;
        }

        public List<Modalidad> getListaModalidadesxServicio(string nombreModalidad, int idServicio)
        {
            modalidades.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("SELECT * from modalidad, servicio where servicio.idServicio = modalidad.servicio_idServicio and servicio.idServicio = @idServicio and modalidad.nombreModalidad like @nombreModalidad and modalidad.estado = 'activo' order by modalidad.idModalidad asc");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idServicio", idServicio),
                new OleDbParameter("@nombreModalidad","%" + nombreModalidad + "%"),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Modalidad modalidad = new Modalidad();
                    modalidad.idModalidad = r.GetInt32(0);
                    modalidad.nombreModalidad = r.GetString(1);
                    modalidad.idServicio = r.GetInt32(2);
                    modalidad.estado = r.GetString(3);

                    modalidades.Add(modalidad);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return modalidades;
        }

        public bool agregarModalidad(Modalidad modalidad)
        {
            int numFilas = 0;
            modalidades.Clear();

            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into modalidad(nombreModalidad,servicio_idServicio,estado) " +
                                                        "values(@nombreModalidad,@servicio_idServicio,@estado)");

            comando.Parameters.AddRange(new OleDbParameter[]
            {                
                new OleDbParameter("@nombreModalidad",modalidad.nombreModalidad),
                new OleDbParameter("@servicio_idServicio",modalidad.idServicio),
                new OleDbParameter("@estado",modalidad.estado),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }

        public bool modificarModalidad(Modalidad modalidad)
        {
            int numFilas = 0;
            modalidades.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update modalidad set nombreModalidad=@nombreModalidad " +
                                                    "where idModalidad=@idModalidad");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombreModalidad",modalidad.nombreModalidad),
                new OleDbParameter("@idModalidad",modalidad.idModalidad),                      
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }

        public bool eliminarModalidad(Modalidad modalidad)
        {
            int numFilas = 0;
            modalidades.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update modalidad set estado=@estado " +
                                                    "where idModalidad=@idModalidad");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","inactivo"),
                new OleDbParameter("@idModalidad",modalidad.idModalidad),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;
        }
    }

    public class ControladorCita
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Cita> citas;
        static ControladorCita controladorCita = null;

        private ControladorCita()
        {
            citas = new List<Cita>();
        }

        static public ControladorCita Instancia()
        {
            if (controladorCita == null)
                controladorCita = new ControladorCita();
            return controladorCita;
        }

        public List<Cita> getListaCitas(string strNumeroCita, string nombres, string apellidoPaterno, string strIdServicio, DateTime strFecha)
        {
            string cit = "", servi = "";
            int idCita = 0, idServicio = 0, idP = 0, idT = 0;
            ControladorPaciente controladorPaciente = ControladorPaciente.Instancia();
            ControladorTerapeuta controladorTerapeuta = ControladorTerapeuta.Instancia();
            ControladorPago controladorPago = ControladorPago.Instancia();
            citas.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            if (!strNumeroCita.Equals(""))
            {
                cit = " and cita.idCita = @idCita ";
                idCita = int.Parse(strNumeroCita);
            }
            if (!strIdServicio.Equals("0"))
            {
                servi = " and servicio.idServicio = @idServicio ";
                idServicio = int.Parse(strIdServicio);
            }

            OleDbCommand comando = new OleDbCommand("SELECT * from cita, servicio, modalidad, persona where servicio.idServicio = cita.servicio_idServicio and modalidad.idModalidad = cita.modalidad_idModalidad and cita.paciente_persona_idPersona = persona.idPersona and persona.nombres like @nombres and persona.apellidoPaterno like @apellidoPaterno and cita.fechaCita = @fechaCita " + cit + servi + " order by cita.idCita asc");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@nombres","%"+nombres+"%"),               
                new OleDbParameter("@apellidoPaterno","%"+apellidoPaterno+"%"),
                new OleDbParameter("@fechaCita",strFecha.Date),
            });

            if (!strNumeroCita.Equals(""))
            {
                comando.Parameters.AddWithValue("@idCita", idCita);
            }
            if (!strIdServicio.Equals("0"))
            {
                comando.Parameters.AddWithValue("@idServicio", idServicio);
            }

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Cita cita = new Cita();
                    cita.idCita = r.GetInt32(0);
                    idP = r.GetInt32(1);
                    Paciente p = new Paciente();
                    p = controladorPaciente.getPaciente(idP);
                    cita.fechaCita = r.GetDateTime(2);
                    cita.horaCita = r.GetDateTime(3);
                    cita.estado = r.GetString(6);
                    cita.fechaRegistro = r.GetDateTime(7);
                    cita.costo = float.Parse(r.GetValue(8).ToString());
                    cita.descuento = r.GetFloat(9);
                    cita.estadoEvaluacion = r.GetString(10);
                    idT = r.GetInt32(11);
                    Terapeuta t = new Terapeuta();
                    t = controladorTerapeuta.getTerapeuta(idT);
                    Servicio s = new Servicio();
                    s.idServicio = r.GetInt32(12);
                    s.nombreServicio = r.GetString(13);
                    s.intervaloHora = r.GetInt32(14);
                    s.costo = r.GetFloat(15);
                    s.maximoPacientes = r.GetInt32(16);
                    s.estado = r.GetString(17);
                    Modalidad m = new Modalidad();
                    m.idModalidad = r.GetInt32(18);
                    m.nombreModalidad = r.GetString(19);
                    m.idServicio = r.GetInt32(20);
                    m.estado = r.GetString(21);
                    cita.servicio = s;
                    cita.modalidad = m;
                    cita.paciente = p;
                    cita.terapeuta = t;
                    List<Pago> pagos = new List<Pago>();
                    pagos = controladorPago.getListaPagosxCita(cita.idCita);
                    cita.pagos = pagos;

                    citas.Add(cita);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return citas;
        }


        public bool verificaCrucesHorarioPaciente(Paciente paciente, DateTime horaCita, DateTime fechaCita, int intervaloHora)
        {
            bool resultado = false;
            int numFilas = 0;           
            OleDbDataReader r = null; 
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("select * from cita " + "where paciente_persona_idPersona=@idPaciente and fechaCita=@fechaCita order by idCita asc");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idPaciente",paciente.persona.idPersona),                
                new OleDbParameter("@fechaCita",fechaCita.Date),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();

                while (r.Read())
                {
                    Cita c = new Cita();
                    c.idCita = r.GetInt32(0);
                    c.fechaCita = r.GetDateTime(2);
                    c.horaCita = r.GetDateTime(3);
                    Servicio s = new Servicio();
                    s.idServicio = r.GetInt32(12);
                    s.nombreServicio = r.GetString(13);
                    s.intervaloHora = r.GetInt32(14);
                    c.servicio = s;


                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas > 0;            
        }

        public bool reservarCita(Cita cita, Pago pago, out int idCita)
        {
            int idC = 0, numFilas = 0;
            bool resultadoPago=false;
            OleDbDataReader r = null;
            ControladorPago controladorPago = ControladorPago.Instancia();
            citas.Clear();

            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into cita(paciente_persona_idPersona,fechaCita,horaCita,servicio_idServicio,modalidad_idModalidad,estado,fechaRegistro,costo,descuento,estadoEvaluacion,terapeuta_persona_idPersona) " +
                                                        "values(@idPaciente,@fechaCita,@horaCita,@idServicio,@idModalidad,@estado,@fechaRegistro,@costo,@descuento,@estadoEvaluacion,@idPersona)");

            OleDbCommand comando2 = new OleDbCommand("SELECT TOP 1 * FROM cita order by idCita DESC");            

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idPaciente",cita.paciente.persona.idPersona),
                new OleDbParameter("@fechaCita",cita.fechaCita),
                new OleDbParameter("@horaCita",cita.horaCita),
                new OleDbParameter("@idServicio",cita.servicio.idServicio),
                new OleDbParameter("@idModalidad",cita.modalidad.idModalidad),
                new OleDbParameter("@estado",cita.estado),
                new OleDbParameter("@fechaRegistro",cita.fechaRegistro),
                new OleDbParameter("@costo",cita.costo),
                new OleDbParameter("@descuento",cita.descuento),
                new OleDbParameter("@estadoEvaluacion",cita.estadoEvaluacion),
                new OleDbParameter("@idPersona",cita.terapeuta.persona.idPersona),
            });

            comando.Connection = conexion;
            comando2.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
                r = comando2.ExecuteReader();

                while (r.Read())
                {
                    idC = r.GetInt32(0);
                }                
                resultadoPago = controladorPago.procesarPago(pago, idC);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            idCita = idC;
            return numFilas == 1 && resultadoPago;
        }

        public bool confirmarCita(Cita cita, Pago pago)
        {
            int numFilas = 0;
            bool resultadoPago = false;
            ControladorPago controladorPago = ControladorPago.Instancia();
            citas.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update cita set estado=@estado " +
                                                    "where idCita=@idCita");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","Confirmado"),
                new OleDbParameter("@idServicio",cita.idCita),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
                resultadoPago = controladorPago.procesarPago(pago, cita.idCita);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1 && resultadoPago;            
        }

        public bool cancelarCita(Cita cita, Pago pago)
        {
            int numFilas = 0;
            bool resultadoPago = false;
            ControladorPago controladorPago = ControladorPago.Instancia();
            citas.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);
            OleDbCommand comando = new OleDbCommand("update cita set estado=@estado " +
                                                    "where idCita=@idCita");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@estado","Cancelado"),
                new OleDbParameter("@idServicio",cita.idCita),
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
                resultadoPago = controladorPago.procesarPago(pago, cita.idCita);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1 && resultadoPago;     
        }
    }

    public class ControladorPago
    {
        private string cadenaConexion = @"PROVIDER=Microsoft.ACE.OLEDB.12.0;Data Source=./Data/terapiaDB_desarrollo.accdb;Persist Security Info=True";
        private List<Pago> pagos;
        static ControladorPago controladorPago = null;

        private ControladorPago()
        {
            pagos = new List<Pago>();
        }

        static public ControladorPago Instancia()
        {
            if (controladorPago == null)
                controladorPago = new ControladorPago();
            return controladorPago;
        }

        public List<Pago> getListaPagosxCita(int idCita)
        {
            pagos.Clear();
            OleDbDataReader r = null;
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("select * from pago where pago.cita_idCita = @idCita order by pago.idPago asc");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idCita", idCita),                          
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                r = comando.ExecuteReader();
                while (r.Read())
                {
                    Pago pago = new Pago();
                    pago.idPago = r.GetInt32(0);
                    pago.idCita = r.GetInt32(1);
                    pago.monto = r.GetFloat(2);
                    pago.fecha = r.GetDateTime(3);
                    pago.estado = r.GetString(4);

                    pagos.Add(pago);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                r.Close();
                conexion.Close();
            }
            return pagos;
        }

        public bool procesarPago(Pago pago, int idCita)
        {
            int numFilas = 0;
            pagos.Clear();
            OleDbConnection conexion = new OleDbConnection(cadenaConexion);

            OleDbCommand comando = new OleDbCommand("insert into pago(cita_idCita,monto,fecha,estado) " +
                                                        "values(@idCita,@monto,@fecha,@estado)");

            comando.Parameters.AddRange(new OleDbParameter[]
            {
                new OleDbParameter("@idCita",idCita),
                new OleDbParameter("@monto",pago.monto),
                new OleDbParameter("@fecha",pago.fecha),
                new OleDbParameter("@estado",pago.estado),                
            });

            comando.Connection = conexion;

            try
            {
                conexion.Open();
                numFilas = comando.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                conexion.Close();
            }
            return numFilas == 1;            
        }
    }



}