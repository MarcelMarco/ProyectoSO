#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <ctype.h>
#include <mysql.h>


int main(int argc, char *argv[])
{
	//Conexion a la base de datos
	MYSQL *conn;
	int err;
	// Estructura especial para almacenar resultados de consultas 
	MYSQL_RES *resultado;
	MYSQL_ROW row;	
	//Creamos una conexion al servidor MYSQL 
	conn = mysql_init(NULL);
	if (conn==NULL) {
		printf ("Error al crear la conexi??n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	//inicializar la conexion
	conn = mysql_real_connect (conn, "localhost","root", "mysql", "Juego",0, NULL, 0);
	if (conn==NULL) {
		printf ("Error al inicializar la conexi??n: %u %s\n", 
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	// consulta SQL para obtener una tabla con todos los datos
	// de la base de datos
	err=mysql_query (conn, "SELECT * FROM Jugador,Partida,Relacion");
	if (err!=0) {
		printf ("Error al consultar datos de la base %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		exit (1);
	}	
	resultado = mysql_store_result (conn);
	//Finalizacion de la conexion con la base de datos
	printf("Conexion a la base de datos correcta\n");
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char peticion[512];
	char respuesta[512];
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9030);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int i;
	// Bucle infinito
	for (;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		int terminar =0;
		// Entramos en un bucle para atender todas las peticiones de este cliente
		//hasta que se desconecte
		while (terminar ==0)
		{
			// Ahora recibimos la petici?n
			ret=read(sock_conn,peticion, sizeof(peticion));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			peticion[ret]='\0';
			
			
			printf ("Peticion: %s\n",peticion);
			
			// vamos a ver que quieren
			char *p = strtok( peticion, "/");
			int codigo =  atoi (p);
			// Ya tenemos el c?digo de la petici?n
			char usuario[20];
			char contra[20];
			char nombre[20];
			char consulta[20];
			
			if ((codigo ==1) || (codigo ==2))
			{
				p = strtok( NULL, "/");				
				strcpy (usuario, p);
				p = strtok( NULL, "/");				
				strcpy (contra, p);				
				printf ("Codigo: %d, Usuario: %s, Contraseña: %s\n", codigo, usuario, contra);
			}			
			else if ((codigo ==3) || (codigo ==4) || (codigo ==5))
			{
				p = strtok( NULL, "/");				
				strcpy (consulta, p);
				printf ("Codigo: %d, Consulta: %s\n", codigo, consulta);
			}
			
			
			char sql[100];			
			if (codigo ==0)
				terminar=1;
			else if (codigo ==1) 
			{
				strcpy (sql, "SELECT Jugador.Identificador FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
				strcat (sql, usuario); 
				strcat (sql, "' AND Jugador.Contraseña = '");
				strcat (sql, contra); 
				strcat (sql, "'");
				mysql_query (conn, sql);
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				if (row == NULL)
					strcpy (respuesta,"Incorrecto");
				else
					strcpy (respuesta,"Correcto");
			}
			else if (codigo ==2)
			{
				strcpy (sql, "INSERT INTO Jugador VALUES(4,'");
				strcat (sql, usuario); 
				strcat (sql, "','");
				strcat (sql, contra); 
				strcat (sql, "')");
				mysql_query (conn, sql);
				strcpy (respuesta,"Correcto");
			}
			else if (codigo ==3)
			{
				strcpy (sql, "SELECT Relacion.Puntos FROM (Jugador,Partida,Relacion) WHERE Jugador.Nombre = '");
				strcat (sql, consulta);
				strcat (sql, "' AND Relacion.idJugador = Jugador.Identificador");
				mysql_query (conn, sql);
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				strcpy(respuesta, row[0]);
			}
			else if (codigo ==4)
			{
				strcpy (sql, "SELECT Partida.Fecha FROM (Partida) WHERE Partida.Identificador = ");
				strcat (sql, consulta);
				mysql_query (conn, sql);
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				strcpy(respuesta, row[0]);
			}
			else if (codigo ==5)
			{
				strcpy (sql, "SELECT Jugador.Nombre FROM (Jugador,Partida,Relacion) WHERE Relacion.Puntos = 3 AND Relacion.idJugador = Jugador.Identificador AND Relacion.idPartida = ");
				strcat (sql, consulta);
				strcat (sql, " AND Relacion.idPartida = Partida.Identificador;");
				mysql_query (conn, sql);
				resultado = mysql_store_result (conn); 
				row = mysql_fetch_row (resultado);
				strcpy(respuesta, row[0]);
			}				
			if (codigo !=0)
			{				
				printf ("Respuesta: %s\n", respuesta);
				// Enviamos respuesta
				write (sock_conn,respuesta, strlen(respuesta));
			}
		}
		// Se acabo el servicio para este cliente
		close(sock_conn);
		mysql_close (conn);
	}
}
